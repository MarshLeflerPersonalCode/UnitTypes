﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library.ClassCreator;
using Library.IO;
using System.Reflection;
using System.Runtime.Serialization;


namespace Library.Database
{
    public interface IDatabaseCallbacks
    {
        void notifyOfNewEntry(Database mDatabase, ClassInstance mNewEntry);
        void notifyOfPreSave(Database mDatabase);
        void notifyOfPostSave(Database mDatabase);
        
    }
    public enum EERROR_ADDING
	{
		NO_ERRORS,
		OBJECT_WAS_NULL,
		NAME_ALREADY_TAKEN,
		FILE_NAME_ALREADY_TAKEN,
		GUID_ALREADY_TAKEN,
        NO_GUIDS_AVAILABLE,
        EXCEPTION_THROWN,
        CLASS_TYPE_NOT_FOUND
	};
    public class Database : IClassInstanceCallbacks
    {
        bool m_bIgnorePropertySetCallback = false;
        DatabaseManager m_DatabaseManager = null;
        DatabaseConfig m_DatabaseConfig = new DatabaseConfig();
        private Type m_ClassType = null;
        List<ClassInstance> m_Instances = new List<ClassInstance>();
		Dictionary<int, ClassInstance> m_InstancesByGuid = new Dictionary<int, ClassInstance>();
		Dictionary<string, ClassInstance> m_InstancesByName = new Dictionary<string, ClassInstance>();
		Dictionary<string, ClassInstance> m_InstancesByFileName = new Dictionary<string, ClassInstance>();
        List<string> m_ListOfNames = new List<string>();
		public Database(DatabaseManager mDatabaseManager)
		{
            m_DatabaseManager = mDatabaseManager;
            isLoaded = false;
            databaseCallbacks = new List<IDatabaseCallbacks>();

        }
       
        public List<IDatabaseCallbacks> databaseCallbacks { get; set; }

        public DatabaseManager getDatabaseManager() { return m_DatabaseManager; }
        public ICollection getListOfNames()
        {
            if( m_ListOfNames.Count == 0 )
            {
                m_ListOfNames = new List<string>(m_InstancesByName.Keys);
                m_ListOfNames.Insert(0, "");
            }
            return m_ListOfNames;
        }
        public bool isLoaded { get; set; }

        public DatabaseConfig getConfig() { return m_DatabaseConfig; }
        //the class representing this database
        public string databaseEntryClass { get { return m_DatabaseConfig.databaseEntryClass; } set { m_DatabaseConfig.databaseEntryClass = value; } }
        //the database name.
        public string databaseName { get { return m_DatabaseConfig.databaseName; } set { m_DatabaseConfig.databaseName = value; } }
        //Each Database saves a guid. That guid will have this mask applied to it. For instance 0x‭8000000‬ is the 27th bit as a 1(0x0000‭1000000000000000000000000000‬). This allows for each guid to know what database they are in and can make them unique against other databases entries with the same mask.
        public int uniqueMask { get { return m_DatabaseConfig.uniqueMask; } set { m_DatabaseConfig.uniqueMask = value; } }
        //This is the mask in which the guid is actually generated in. Should be a number that represents something like 0xFFFF(65535)
        public int guidMask { get { return m_DatabaseConfig.guidMask; } set { m_DatabaseConfig.guidMask = value; } }
        //returns the C# type defining object
        public Type getDatabaseEntryClassType()
        {
            if(m_ClassType != null )
            {
                return m_ClassType;
            }
            m_ClassType = m_DatabaseManager.getDatabaseEntryType(databaseName);
            return m_ClassType;
        }

        public void _notifyOfPropertySetOnClassInstance(ClassInstance mInstance, string strProperty)
        {
            if(m_bIgnorePropertySetCallback )
            {
                return;
            }
            
            switch (strProperty)
            {
                case "m_DatabaseGuid":
                {
                    foreach(KeyValuePair<int, ClassInstance> mData in m_InstancesByGuid)
                    {
                        if( mData.Value == mInstance)
                        {
                            m_InstancesByGuid.Remove(mData.Key);
                            m_InstancesByGuid[getEntryGuid(mInstance)] = mInstance;
                            break;
                        }
                    }
                }
                    break;
                case "m_strName":
                {
                    
                    foreach (KeyValuePair<string, ClassInstance> mData in m_InstancesByName)
                    {
                        if (mData.Value == mInstance)
                        {
                            m_ListOfNames.Clear();
                            m_InstancesByName.Remove(mData.Key);
                            m_InstancesByName[getEntryName(mInstance).ToUpper()] = mInstance;
                            break;
                        }
                    }
                }
                break;
                case "m_strFileName":
                {
                    foreach (KeyValuePair<string, ClassInstance> mData in m_InstancesByFileName)
                    {
                        if (mData.Value == mInstance)
                        {
                            m_InstancesByFileName.Remove(mData.Key);
                            m_InstancesByFileName[getEntryFileName(mInstance).ToUpper()] = mInstance;
                            break;
                        }
                    }
                }
                break;
            }

            if(strProperty == m_DatabaseConfig.parentVariableName)
            {
                ClassInstance mParentInstance = getEntryByName(mInstance.getPropertyValueString(strProperty, ""));
                if (mParentInstance != mInstance)
                {
                    mInstance.setOwningClass(mParentInstance);
                }
            }
            
        }


        public void removeEntry(ClassInstance mEntry)
        {
            if( mEntry == null)
            {
                return;
            }
            m_Instances.Remove(mEntry);
            m_InstancesByFileName.Remove(getEntryFileName(mEntry).ToUpper());
            m_InstancesByName.Remove(getEntryName(mEntry).ToUpper());
            m_ListOfNames.Clear();
            m_InstancesByGuid.Remove(getEntryGuid(mEntry));            
            mEntry.m_CallBacks.Remove(this);
        }

        public string getEntryFileName(ClassInstance mEntry)
        {

            string strFileName = mEntry.getPropertyValueString("m_strFileName", "");
            if (strFileName != "")
            {
                return strFileName;
            }
            strFileName = getEntryName(mEntry);
            if( strFileName == "")
            {
                return strFileName;
            }
            return strFileName.Replace(" ", "_") + ".dat";
        }
        private void _setEntryFileName(ClassInstance mEntry, string strFileName)
        {
            strFileName = strFileName.Replace(" ", "_");
            if ( strFileName.EndsWith(".dat") == false )
            {
                strFileName = strFileName + ".dat";
            }
            m_bIgnorePropertySetCallback = true;
            mEntry.setProperty("m_strFileName", strFileName);
            m_bIgnorePropertySetCallback = false;
        }

        //returns if the name is valid for this database
        public bool isValidName(string strName)
        {
            return (getEntryByName(strName) == null) ? true : false;
        }

        public bool renameEntry(ClassInstance mEntry, string strNewName )
        {
            if(mEntry != null &&
                isValidName(strNewName))
            {
                mEntry.setProperty("m_strName", strNewName);                
                mEntry.m_bIsDirty = true;
                return true;
            }
            return false;
        }
        public string getEntryName(ClassInstance mEntry)
        {
            return mEntry.getPropertyValueString("m_strName", "");
        }


        private void _setEntryName(ClassInstance mEntry, string strName)
        {
            m_bIgnorePropertySetCallback = true;
            mEntry.setProperty("m_strName", strName);
            m_bIgnorePropertySetCallback = false;
        }
        public int getEntryGuid(ClassInstance mEntry)
        {
            return mEntry.getPropertyValueInt("m_DatabaseGuid", -1);
        }
        private void _setEntryGuid(ClassInstance mEntry, int iGuid)
        {
            m_bIgnorePropertySetCallback = true;
            iGuid = iGuid & m_DatabaseConfig.guidMask;  //we always put the guid mask and the unique mask on.
            iGuid = iGuid | m_DatabaseConfig.uniqueMask; //we always put the guid mask and the unique mask on.
            
            mEntry.setProperty("m_DatabaseGuid", iGuid);
            m_bIgnorePropertySetCallback = false;
        }


        private bool _createUniqueGuidForEntry(ClassInstance mEntry)
        {
            int iTries = 1000;
            
            Random mRandom = new Random();
            while(iTries > 0 )
            {
                int iGuid = mRandom.Next() & m_DatabaseConfig.guidMask;                
                iGuid = iGuid | m_DatabaseConfig.uniqueMask;
                ClassInstance mEntryWithGuid = getEntryByGuid(iGuid);
                if( mEntryWithGuid == null )
                {
                    _setEntryGuid(mEntry, iGuid);
                    return true;
                }
            };
            log("WARNING - guid creation took more than 1000 random attempts to find a unique guid.");
            int iStartIndex = mRandom.Next() & m_DatabaseConfig.uniqueMask;
            for ( int iGuidAttempt = iStartIndex; iGuidAttempt < m_DatabaseConfig.uniqueMask; iGuidAttempt++)
            {
                ClassInstance mEntryWithGuid = getEntryByGuid(iGuidAttempt);
                if (mEntryWithGuid == null)
                {
                    _setEntryGuid(mEntry, iGuidAttempt);
                    return true;
                }
            }
            for (int iGuidAttempt = 0; iGuidAttempt < iStartIndex; iGuidAttempt++)
            {
                ClassInstance mEntryWithGuid = getEntryByGuid(iGuidAttempt);
                if (mEntryWithGuid == null)
                {
                    _setEntryGuid(mEntry, iGuidAttempt);
                    return true;
                }
            }
            log("ERROR - unable to find any new guids. Every guid appears to be taken!");
            return false;
        }

        public string getDatabaseConfigPathAndFile() { return m_DatabaseConfig.getDatabaseConfigPathAndFile(); }

        public void cleanDatabase()
        {
            m_Instances.Clear();
            m_InstancesByGuid.Clear();
            m_InstancesByName.Clear();
            m_ListOfNames.Clear();            
            m_InstancesByFileName.Clear();            
        }

        //logs a message
        public bool log(string strLogMessage)
        {
            return m_DatabaseManager.log(strLogMessage);
        }

        public List<ClassInstance> getEntries()
		{
			return m_Instances;
		}

		//returns the instance by name
		public ClassInstance getEntryByName(string strName)
		{
            if (m_InstancesByName.ContainsKey(strName.ToUpper()))
            {
                return m_InstancesByName[strName.ToUpper()];
            }
            return null;
		}

		public ClassInstance getEntryByGuid(int iGuid)
		{
            iGuid = iGuid & (m_DatabaseConfig.guidMask | m_DatabaseConfig.uniqueMask);            
            if( m_InstancesByGuid.ContainsKey(iGuid))
            {
                return m_InstancesByGuid[iGuid];
            }
            return null;
		}
		public ClassInstance getEntryByFileName(string strFileName)
		{
            if (m_InstancesByFileName.ContainsKey(strFileName.ToUpper()))
            {
                return m_InstancesByFileName[strFileName.ToUpper()];
            }
            return null;
		}

        public string strCreateUniqueName(string strUniqueName)
        {
            int iCount = 0;
            while(true)
            {
                if(getEntryByName(databaseName + "_entry" + iCount.ToString()) == null)
                {
                    return databaseName + "_entry" + iCount.ToString();
                }
                iCount++;
            }
        }

        public EERROR_ADDING createEntry(string strUniqueName)
        {
            Type mType = getDatabaseEntryClassType();
            if (mType == null)
            {
                log("Unable to create classes(" + databaseEntryClass + ") for database " + databaseName + ". Class type not found.");                
                return EERROR_ADDING.CLASS_TYPE_NOT_FOUND;
            }
            if (getEntryByName(strUniqueName) != null)
            {
                return EERROR_ADDING.NAME_ALREADY_TAKEN;
            }
            try
            {
             
                object mObjectCreated = Activator.CreateInstance(mType);
                ClassInstance mInstance = mObjectCreated as ClassInstance;
                _setEntryName(mInstance, strUniqueName);
                _setEntryFileName(mInstance, getEntryFileName(mInstance));
                if(_createUniqueGuidForEntry(mInstance) == false )
                {
                    return EERROR_ADDING.NO_GUIDS_AVAILABLE;
                }
                EERROR_ADDING eError = addEntry(mInstance);
                if (eError == EERROR_ADDING.NO_ERRORS)
                {
                    mInstance.m_bIsDirty = true;
                }

                return eError;

            }
            catch (Exception e)
            {
                log("Error in creating class: " + mType.Name + ". Exception thrown was: " + e.Message);
            }
            return EERROR_ADDING.EXCEPTION_THROWN;
        }

		public EERROR_ADDING addEntry(ClassInstance mNewEntry)
		{          
			if(mNewEntry == null)
			{
                log("ERROR - unable to add entry. Entry was null. Database: " + databaseName);
				return EERROR_ADDING.OBJECT_WAS_NULL;
			}
            if( getEntryByName(getEntryName(mNewEntry)) != null )
            {
                log("ERROR - unable to add entry. Entry name is not unique. Name specified: " + getEntryName(mNewEntry) + ".  Database: " + databaseName);
                return EERROR_ADDING.NAME_ALREADY_TAKEN;
            }
            int iGuid = getEntryGuid(mNewEntry);
            if (iGuid == -1 || iGuid == 0 || getEntryByGuid(iGuid) != null)
            {
                log("ERROR - unable to add entry. Entry guid is not unique or valid. Guid specified: " + iGuid.ToString() + ".  Database: " + databaseName);
                return EERROR_ADDING.GUID_ALREADY_TAKEN;
            }            
            mNewEntry.m_LogFile = m_DatabaseManager.logFile;
            mNewEntry.m_CallBacks.Add(this);            
            m_Instances.Add(mNewEntry);            
            m_InstancesByGuid[getEntryGuid(mNewEntry)] = mNewEntry;
            m_InstancesByName[getEntryName(mNewEntry).ToUpper()] = mNewEntry;
            m_ListOfNames.Clear();
            m_InstancesByFileName[getEntryFileName(mNewEntry).ToUpper()] = mNewEntry;
            foreach(IDatabaseCallbacks mCallback in databaseCallbacks)
            {
                mCallback.notifyOfNewEntry(this, mNewEntry);
            }
            return EERROR_ADDING.NO_ERRORS;
		}

        public bool deleteEntry(ClassInstance mNewEntry)
        {
            if(mNewEntry == null )
            {
                return false;
            }
            string strEntryname = getEntryName(mNewEntry);
            string strEntryFileName = getEntryFileName(mNewEntry);
            try
            {

                m_Instances.Remove(mNewEntry);
                m_InstancesByGuid.Remove(getEntryGuid(mNewEntry));
                m_InstancesByName.Remove(strEntryname);
                m_ListOfNames.Clear();
                m_InstancesByFileName.Remove(strEntryFileName);                
                string strPathToDatabaseFolder = Path.Combine(m_DatabaseManager.getDatabaseDirectory(), databaseName.Replace(" ", "_") + "\\");
                File.Delete(Path.Combine(strPathToDatabaseFolder, strEntryFileName));
            }
            catch(Exception e)
            {
                log("Error - in deleting entry " + strEntryname + " in database " + databaseName + Environment.NewLine + e.Message);
                return false;
            }
            return true;
        }

        public bool loadDatabase(string strPathToFile)
        {
            isLoaded = false;
            DatabaseConfig mNewConfig = DatabaseConfig.createDatabaseConfigFromJson(this, strPathToFile);
            if(mNewConfig != null)
            {
                m_DatabaseConfig = mNewConfig;
                return _loadData();

            }
            else
            {
                isLoaded = true;
            }
            return false;
        }

        public int getEntrysDirtyCount()
        {
            int iCount = 0;
            foreach (ClassInstance mInstance in m_Instances)
            {
                if (mInstance.m_bIsDirty)
                {
                    iCount++;
                }
            }
            return iCount;
        }

        public bool saveDatabase( bool bForce )
        {
            string strPathToDatabaseFolder = Path.Combine(m_DatabaseManager.getDatabaseDirectory(), databaseName.Replace(" ", "_") + "\\");
            string strDatabaseFile = "database.json";
            foreach (IDatabaseCallbacks mCallback in databaseCallbacks)
            {
                mCallback.notifyOfPreSave(this);
            }
            if ( m_DatabaseConfig.saveDatabaseConfigToJson(this, Path.Combine(strPathToDatabaseFolder, strDatabaseFile)))
            {

                foreach( ClassInstance mInstance in m_Instances)
                {
                    if( mInstance.m_bIsDirty ||
                        bForce)
                    {
                        try
                        {


                            string strInstanceSerialized = DataGroupConvert.serializeObjectIntoString(mInstance, m_DatabaseManager.logFile);
                            string strFileName = getEntryFileName(mInstance);
                            if (strFileName != null &&
                                strFileName != "")
                            {
                                string strFullPath = Path.Combine(strPathToDatabaseFolder, strFileName);
                                File.WriteAllText(strFullPath, strInstanceSerialized, Encoding.ASCII);
                                if(File.Exists(strFullPath))
                                {
                                    mInstance.m_bIsDirty = false;
                                }
                                else
                                {
                                    log("Error - in saving instance: " + getEntryName(mInstance));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            log("Error - in saving instance: " + getEntryName(mInstance) + ". Error was " + e.Message);
                        }
                    }
                }
            }
            foreach (IDatabaseCallbacks mCallback in databaseCallbacks)
            {
                mCallback.notifyOfPostSave(this);
            }
            return true;
        }



        private bool _loadData()
        {
            cleanDatabase();
            Type mType = m_DatabaseManager.getDatabaseEntryType(databaseName);
            if (mType == null )
            {
                log("Unable to create classes(" + databaseEntryClass + ") for database " + databaseName + ". Class type not found.");
                isLoaded = true;
                return false;
            }

            string strPathToSearch = Path.GetFullPath(getDatabaseConfigPathAndFile());
            strPathToSearch = strPathToSearch.Substring(0, strPathToSearch.Length - Path.GetFileName(strPathToSearch).Length);
            string[] mFiles = Directory.GetFiles(strPathToSearch, "*.dat", SearchOption.AllDirectories);
            
            foreach (string strFile in mFiles)
            {
                try
                {
                    
                    ClassInstance mEntry = DataGroupConvert.deserializeObjectFromFile(strFile, mType, m_DatabaseManager.logFile, m_DatabaseManager) as ClassInstance;
                    if (mEntry == null)
                    {
                        log("ERROR - Unable to deserialize file: " + strFile);
                    }
                    addEntry(mEntry);
                    _setEntryFileName(mEntry, Path.GetFileName(strFile));
                    mEntry.m_bIsDirty = false;
                }
                catch (Exception e)
                {
                    log("ERROR - Unable to deserialize Database config. File attempting to deserialize is: " + strFile + Environment.NewLine + "Error message was: " + e.Message);
                    isLoaded = true;
                    return false;

                }

            }
            bool bNoErrors = true;
            if (m_DatabaseConfig.parentVariableName != null &&
                m_DatabaseConfig.parentVariableName != "")
            {
                foreach (ClassInstance mInstance in m_Instances)
                {
                    string strParent = mInstance.getPropertyValueString(m_DatabaseConfig.parentVariableName, "");
                    try
                    {

                        
                        if(strParent == "" )
                        {
                            continue;
                        }
                        ClassInstance mParent = getEntryByName(strParent);
                        if( mParent != null)
                        {
                            mInstance.setOwningClass(mParent);
                        }
                    }
                    catch (Exception e)
                    {
                        log("ERROR - Loaded objects but failed to set parent: " + strParent + " for object named " + getEntryName(mInstance) + Environment.NewLine + "Error message was: " + e.Message);
                        bNoErrors = false;
                    }
                }
            }
            isLoaded = true;
            return bNoErrors;
        }

    } //end class
}//end namespace
