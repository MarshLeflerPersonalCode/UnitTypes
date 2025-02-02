﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ClassParser;
namespace Library.ClassCreator.Writers
{
    public class ClassWriter
    {

        public static string writeClass(ClassCreatorManager mManager, ClassStructure mClass, ProjectWrapper mProjectWrapper)
        {
            if (mClass == null)
            {
                return "";
            }
            List<string> mVariableInitializer = new List<string>();
            string strClass = Environment.NewLine;
            string strClassExtending = _getCLassInheritingFrom(mClass, mProjectWrapper);
            strClass = strClass + "    public class " + mClass.name + ": " + strClassExtending + Environment.NewLine;


            strClass = strClass + "    {" + Environment.NewLine;
            strClass = strClass + "        public override object _getAs(Type mType)" + Environment.NewLine;
            strClass = strClass + "        {" + Environment.NewLine;
            strClass = strClass + "                System.Windows.Forms.MessageBox.Show(\"calling _getAs. Type looking for is: \" + mType.Name + \" my type is " + mClass.name + "\" );" + Environment.NewLine;
            strClass = strClass + "                if (mType.Name == \"" + mClass.name + "\")" + Environment.NewLine;
            strClass = strClass + "                {" + Environment.NewLine;
            strClass = strClass + "                System.Windows.Forms.MessageBox.Show(\"casting!!!!\" );" + Environment.NewLine;
            strClass = strClass + "                        return (" + mClass.name + ")this;" + Environment.NewLine;
            strClass = strClass + "                }" + Environment.NewLine;
            strClass = strClass + "                return base._getAs(mType);" + Environment.NewLine;
            strClass = strClass + "        }" + Environment.NewLine;
            strClass = strClass + _writeVariables(mManager, mClass, mProjectWrapper, mVariableInitializer);
            strClass = strClass + "public " + mClass.name + "()" + Environment.NewLine;
            strClass = strClass + "        {" + Environment.NewLine;
            foreach( string strVarInitializer in mVariableInitializer)
            {
                strClass = strClass + strVarInitializer + Environment.NewLine;
            }
            strClass = strClass + "        }" + Environment.NewLine;
            strClass = strClass + Environment.NewLine + "    } //end of " + mClass.name + Environment.NewLine;

            return strClass;
        }

        private static string _getCLassInheritingFrom(ClassStructure mClass, ProjectWrapper mProjectWrapper)
        {
            foreach( string strClass in mClass.classStructuresInheritingFrom)
            {
                if( mProjectWrapper.getClassStructByName(strClass) != null )
                {
                    return strClass;
                }
            }

            return "ClassInstance";
        }

        private static string _writeVariables(ClassCreatorManager mManager, ClassStructure mClass, ProjectWrapper mProjectWrapper, List<string> mVariableInitializer)
        {
            string strClass = "";            
            foreach (ClassVariable mVariable in mClass.variables)
            {
                if (mVariable.isPrivateVariable == false &&
                    mVariable.isSerialized == true)
                {
                    string strReplace = _writeVariable(mManager, mProjectWrapper, mClass, mVariable, mVariableInitializer);
                    strReplace = "        " + strReplace.Replace(Environment.NewLine, Environment.NewLine + "        ");
                    strClass = strClass + strReplace;
                }
            }
            return strClass;
        }

        private static string _writeVariable(ClassCreatorManager mManager, ProjectWrapper mProjectWrapper, ClassStructure mClass, ClassVariable mVariable, List<string> mVariableInitializer)
        {
            VariableDefinitionHandler mVariableTypes = mManager.variableDefinitionHandler;


            foreach (VariableDefinition mVariableDef in mVariableTypes.getVariableDefinitions())
            {
                if( mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.LIST)
                {
                    if( mVariable.variableType.StartsWith(mVariableDef.variableName) == false )
                    {
                        continue;
                    }
                }
                else if (mVariable.variableType != mVariableDef.variableName)
                {

                    continue;
                }
                if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.COUNT ||
                    mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.NOT_DEFINED)
                {
                    mManager.log("ERROR - found variable " + mVariable.variableName + " of type " + mVariable.variableType + " in class " + mClass.name + " but the definition for that variable is not defined. Unable to create that variable");
                }
                if (mVariableDef.isPrimitiveType)
                {
                    return _writeVariableInfo(mClass, mVariable) + _writePrimitiveVariable(mProjectWrapper, mVariableDef, mVariable, mClass);
                }
                else if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.CLASS)
                {
                    if (mProjectWrapper.classStructures.ContainsKey(mVariableDef.variableClassName.ToUpper()))
                    {
                        //it's a class structure.
                        return _writeVariableInfo(mClass, mVariable) + _writeClassVariable(mManager, mProjectWrapper, mClass, mVariable, mVariableDef.variableClassName, mVariableInitializer);
                    }
                }
                else if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.ENUM)
                {
                    if (mProjectWrapper.enums.ContainsKey(mVariableDef.variableEnumName.ToUpper()))
                    {
                        //it's an enum.
                        return _writeVariableInfo(mClass, mVariable) + _writeEnumVariable(mManager, mProjectWrapper, mClass, mVariable, mVariableDef.variableEnumName);
                    }
                }
                else if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.LIST)
                {
                    return _writeVariableInfo(mClass, mVariable) + _writeListVariable(mManager, mProjectWrapper, mClass, mVariable);
                }
            }


            if (mProjectWrapper.enums.ContainsKey(mVariable.variableType.ToUpper()))
            {
                //it's an enum.
                return _writeVariableInfo(mClass, mVariable) + _writeEnumVariable(mManager, mProjectWrapper, mClass, mVariable, "");
            }
            if (mProjectWrapper.classStructures.ContainsKey(mVariable.variableType.ToUpper()))
            {
                //it's a class structure.
                return _writeVariableInfo(mClass, mVariable) + _writeClassVariable(mManager, mProjectWrapper, mClass, mVariable, "", mVariableInitializer);
            }



            mManager.log("ERROR - found variable " + mVariable.variableName + " of type " + mVariable.variableType + " in class " + mClass.name + ". No definition was found defining the type.");
            return "";
        }

        private static string _writeVariableInfo(ClassStructure mClass, ClassVariable mClassVariable)
        {

            string strClass = Environment.NewLine;
            strClass = strClass + "/".PadRight(100, '/') + Environment.NewLine;
            strClass = strClass + "//Class File:" + mClass.file + Environment.NewLine;
            strClass = strClass + "//Class Name:" + mClass.name + Environment.NewLine;
            strClass = strClass + "//Variable Name:" + mClassVariable.variableName + Environment.NewLine;
            strClass = strClass + "//Variable Type:" + mClassVariable.variableType + Environment.NewLine;
            strClass = strClass + "//Variable Value:" + mClassVariable.variableValue + Environment.NewLine;
            strClass = strClass + "//Variable Line Number:" + mClassVariable.lineNumber.ToString() + Environment.NewLine;
            if (mClassVariable.variableProperties.Count > 0)
            {
                string strProperties = "";
                foreach (KeyValuePair<string, string> mData in mClassVariable.variableProperties)
                {
                    strProperties = strProperties + mData.Key + " = " + mData.Value + ", ";
                }
                strProperties = strProperties.Substring(0, strProperties.Length - 2);
                strClass = strClass + "//Variable Properties: " + strProperties + Environment.NewLine;
            }
            return strClass;
        }

        private static string _writePrimitiveVariable(ProjectWrapper mProjectWrapper, VariableDefinition mVariableDefinition, ClassVariable mClassVariable, ClassStructure mClass)
        {
            string strClass = "";
            string strDefaultValue = "0";
            switch (mVariableDefinition.eCSharpVariable)
            {
                default:
                {

                    string strValue = mClassVariable.variableValue.Trim().Replace("\"", "");
                    if (strValue.Length > 0)
                    {
                        //bool bFound = false;
                        while (mProjectWrapper.defines.ContainsKey(strValue))
                        {
                            //bFound = true;
                            strValue = mProjectWrapper.defines[strValue];
                        }
                        /*if (bFound == false)
                        {
                            if(strValue.EndsWith("f"))
                            {
                                strValue = strValue.Replace("f", ""); //fixes issue with defining 0.0f
                            }
                            
                        }*/
                    }
                    strDefaultValue = ((strValue.Length != 0) ? strValue : "0");
                    strClass = strClass + "private " + EVARIABLE_CSHARP_TYPES_NAMES.g_Names[(int)mVariableDefinition.eCSharpVariable] + " _" + mClassVariable.variableName + " = " + strDefaultValue + ";" + Environment.NewLine;
                }
                break;
                case EVARIABLE_CSHARP_TYPES.STRING:
                {
                    string strValue = mClassVariable.variableValue.Trim();
                    while (mProjectWrapper.defines.ContainsKey(strValue))
                    {
                        strValue = mProjectWrapper.defines[strValue];
                    }
                    if (strValue.StartsWith("\""))
                    {
                        strValue = strValue.Substring(1, strValue.Length - 1);
                    }
                    if (strValue.EndsWith("\""))
                    {
                        strValue = strValue.Substring(0, strValue.Length - 1);
                    }
                    strDefaultValue = "\"" + ((strValue.Length != 0) ? strValue : "") + "\"";
                    strClass = strClass + "private string _" + mClassVariable.variableName + " = " + strDefaultValue + ";" + Environment.NewLine;
                }
                break;
            }
            string strFuncVariableName = "_" + mClassVariable.variableName;
            strClass = strClass + _writeVaraibleComponentModelDetails(mClassVariable);
            strClass = strClass + "public " + EVARIABLE_CSHARP_TYPES_NAMES.g_Names[(int)mVariableDefinition.eCSharpVariable] + " " + mClassVariable.variableName + Environment.NewLine;
            strClass = strClass + "{" + Environment.NewLine;
            strClass = strClass + "    get" + Environment.NewLine;
            strClass = strClass + "    {" + Environment.NewLine;
            strClass = strClass + "        if(m_OwningClass != null && " + strFuncVariableName + " == " + strDefaultValue + ")" + Environment.NewLine;
            strClass = strClass + "        {" + Environment.NewLine;
            strClass = strClass + "            " + mClass.name + " mParent = m_OwningClass as " + mClass.name + ";" + Environment.NewLine;
            strClass = strClass + "            if(mParent != null){ return mParent." + mClassVariable.variableName + "; }" + Environment.NewLine;
            strClass = strClass + "        }" + Environment.NewLine;
            strClass = strClass + "        return " + strFuncVariableName + ";" + Environment.NewLine;
            strClass = strClass + "    }" + Environment.NewLine;
            if (mClassVariable.variableProperties.ContainsKey("ClampMin") ||
               mClassVariable.variableProperties.ContainsKey("ClampMax"))
            {
                strClass = strClass + "    set" + Environment.NewLine;
                strClass = strClass + "    {" + Environment.NewLine;
                if (mClassVariable.variableProperties.ContainsKey("ClampMin"))
                {
                    strClass = strClass + "        if(value < " + mClassVariable.variableProperties["ClampMin"] + "){value = " + mClassVariable.variableProperties["ClampMin"] + ";}" + Environment.NewLine;
                }
                if (mClassVariable.variableProperties.ContainsKey("ClampMax"))
                {
                    strClass = strClass + "        if(value > " + mClassVariable.variableProperties["ClampMax"] + "){value = " + mClassVariable.variableProperties["ClampMax"] + ";}" + Environment.NewLine;
                }
                strClass = strClass + "        " + strFuncVariableName + " = value;" + Environment.NewLine;
                strClass = strClass + "        _notifyOfPropertyChanged(\"" + mClassVariable.variableName + "\");" + Environment.NewLine;
                strClass = strClass + "    }" + Environment.NewLine;
            }
            else
            {
                strClass = strClass + "    set{ " + strFuncVariableName + " = value; _notifyOfPropertyChanged(\"" + mClassVariable.variableName + "\");}" + Environment.NewLine;
            }
            strClass = strClass + "}" + Environment.NewLine;

            string strDefaultValueFunc = "public static " + EVARIABLE_CSHARP_TYPES_NAMES.g_Names[(int)mVariableDefinition.eCSharpVariable] + " " + mClassVariable.variableName + "_Default{ get { return " + strDefaultValue + "; } }" + Environment.NewLine;
            return strDefaultValueFunc + strClass;
        }

        private static string _writeVaraibleComponentModelDetails(ClassVariable mClassVariable)
        {
            if (mClassVariable.variableComment == "" && mClassVariable.variableProperties.Count == 0)
            {
                return "";
            }
            string strDetails = "";

            if (mClassVariable.variableProperties.ContainsKey("DISPLAYNAME"))
            {
                strDetails = strDetails + "DisplayName(\"" + mClassVariable.variableProperties["DISPLAYNAME"] + "\"), ";
            }
            if (mClassVariable.variableProperties.ContainsKey("CATEGORY"))
            {
                strDetails = strDetails + "Category(\"" + mClassVariable.variableProperties["CATEGORY"] + "\"), ";
            }
            if (mClassVariable.variableProperties.ContainsKey("READONLY"))
            {
                strDetails = strDetails + "ReadOnly(true), ";
            }
            if (mClassVariable.variableProperties.ContainsKey("HIDDEN"))
            {
                strDetails = strDetails + "Browsable(false), ";
            }
            else if (mClassVariable.variableProperties.ContainsKey("BROWSABLE"))
            {
                string strValue = mClassVariable.variableProperties["BROWSABLE"];
                if( strValue.ToUpper().Contains("FALSE"))
                {
                    strDetails = strDetails + "Browsable(false), ";
                }
            }

            string strDescription = "";
            if (mClassVariable.variableProperties.ContainsKey("TOOLTIP"))
            {
                strDescription = mClassVariable.variableProperties["TOOLTIP"];
            }
            else if (mClassVariable.variableProperties.ContainsKey("DESCRIPTION"))
            {
                strDescription = mClassVariable.variableProperties["DESCRIPTION"];
            }
            else if (mClassVariable.variableComment != "")
            {
                strDescription = mClassVariable.variableComment;
            }

            if (strDescription.Length != 0)
            {
                strDescription = strDescription.Replace("\r", "");
                strDescription = strDescription.Replace("\n", "");
                strDescription = strDescription.Replace("\"", "'");
                strDescription = strDescription.Replace("//", "");                
                strDetails = strDetails + "Description(\"" + strDescription + "\"), ";
            }
            if (strDetails.Length == 0)
            {
                return "";
            }
            strDetails = strDetails.Substring(0, strDetails.Length - 2); //remove ", "
            string strReturnString = "[" + strDetails + "]" + Environment.NewLine;
            if(mClassVariable.typeConverter != "")
            {
                return strReturnString + mClassVariable.typeConverter + Environment.NewLine;
            }
            return strReturnString;
        }


        public static string _writeEnumVariable(ClassCreatorManager mManager, ProjectWrapper mProjectWrapper, ClassStructure mClass, ClassVariable mVariable, string strTypeOverride)
        {
            string strEnumLine = "";
            string strValue = mVariable.variableValue;
            string strGetCheck = mVariable.variableValue;
            string strType = (strTypeOverride == "") ? mVariable.variableType : strTypeOverride;
            if (strValue != "")
            {
                strValue = " = " + strValue.Replace("::", ".") + ";";
                strGetCheck = strGetCheck.Replace("::", ".");
            }
            else
            {
                strGetCheck = "(" + strType + ")0";
                strValue = ";";
            }
            
            strEnumLine = strEnumLine + "private " + strType + " _" + mVariable.variableName + strValue + Environment.NewLine;
            strEnumLine = strEnumLine + _writeVaraibleComponentModelDetails(mVariable);
            strEnumLine = strEnumLine + "public " + strType + " " + mVariable.variableName + Environment.NewLine;
            strEnumLine = strEnumLine + "{" + Environment.NewLine;                        
            strEnumLine = strEnumLine + "    get" + Environment.NewLine;
            strEnumLine = strEnumLine + "    {" + Environment.NewLine;
            strEnumLine = strEnumLine + "        if(m_OwningClass != null && _" + mVariable.variableName + " == " + strGetCheck + ")" + Environment.NewLine;
            strEnumLine = strEnumLine + "        {" + Environment.NewLine;
            strEnumLine = strEnumLine + "            " + mClass.name + " mParent = m_OwningClass as " + mClass.name + ";" + Environment.NewLine;
            strEnumLine = strEnumLine + "            if(mParent != null){ return mParent." + mVariable.variableName + "; }" + Environment.NewLine;
            strEnumLine = strEnumLine + "        }" + Environment.NewLine;
            strEnumLine = strEnumLine + "        return _" + mVariable.variableName + ";" + Environment.NewLine;
            strEnumLine = strEnumLine + "    }" + Environment.NewLine;
            strEnumLine = strEnumLine + "    set{ _" + mVariable.variableName + " = value; _notifyOfPropertyChanged(\"" + mVariable.variableName + "\");}" + Environment.NewLine;
            strEnumLine = strEnumLine + "}" + Environment.NewLine;
            string strDefaultValueFunc = "public static " + strType + " " + mVariable.variableName + "_Default{ get { return " + strGetCheck + "; } }" + Environment.NewLine;
            return strDefaultValueFunc + strEnumLine;
        }
        public static string _writeClassVariable(ClassCreatorManager mManager, ProjectWrapper mProjectWrapper, ClassStructure mClass, ClassVariable mVariable, string strTypeOverride, List<string> mVariableInitializer)
        {
            string strClassLine = "";
            string strType = (strTypeOverride == "") ? mVariable.variableType : strTypeOverride;

            //mVariableInitializer.Add("                _" + mVariable.variableName + ".m_OwningClass = this;");
            strClassLine = strClassLine + "private " + strType + " _" + mVariable.variableName + " = new " + strType + "();" + Environment.NewLine;            
            strClassLine = strClassLine + _writeVaraibleComponentModelDetails(mVariable);
            strClassLine = strClassLine + "public " + strType + " " + mVariable.variableName + Environment.NewLine;
            strClassLine = strClassLine + "{" + Environment.NewLine;
            strClassLine = strClassLine + "    get{ return _" + mVariable.variableName + "; }" + Environment.NewLine;
            strClassLine = strClassLine + "    set{ _" + mVariable.variableName + " = value; _notifyOfPropertyChanged(\"" + mVariable.variableName + "\"); }" + Environment.NewLine;
            strClassLine = strClassLine + "}" + Environment.NewLine;
            return strClassLine;
        }

        public static string _writeListVariable(ClassCreatorManager mManager, ProjectWrapper mProjectWrapper, ClassStructure mClass, ClassVariable mVariable)
        {
            string strVariableType = "";
            int iIndexLessThan = mVariable.variableType.IndexOf("<");
            int iIndexGreaterThan = mVariable.variableType.IndexOf(">");
            if( iIndexGreaterThan < 0 ||
                iIndexLessThan < 0)
            {                
                mManager.log("ERROR - LIST VARIABLE TYPE IS FORMATED WRONG: " + mVariable.variableType);
                return "ERROR - CHECK LOG" + Environment.NewLine;
            }
            iIndexLessThan++;//removes the <
            VariableDefinitionHandler mVariableTypes = mManager.variableDefinitionHandler;
            string strTempVariableTypeName = mVariable.variableType.Substring(iIndexLessThan, iIndexGreaterThan - iIndexLessThan).Trim();
            strTempVariableTypeName = strTempVariableTypeName.Replace("*", "").Trim();
            bool bVariableTypeFound = false;
            foreach (VariableDefinition mVariableDef in mVariableTypes.getVariableDefinitions())
            {
                if (strTempVariableTypeName != mVariableDef.variableName)
                {

                    continue;
                }
                if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.COUNT ||
                    mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.NOT_DEFINED)
                {
                    mManager.log("ERROR - found variable " + strTempVariableTypeName + " inside list variable " + mVariable.variableType + " in class " + mClass.name + " but the definition for that variable is not defined. Unable to create that variable");
                    return "ERROR - CHECK LOG" + Environment.NewLine;
                }
                if (mVariableDef.isPrimitiveType)
                {
                    strVariableType = EVARIABLE_CSHARP_TYPES_NAMES.g_Names[(int)mVariableDef.eCSharpVariable];

                }
                else if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.CLASS)
                {
                    strVariableType = mVariableDef.variableClassName;
                }
                else if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.ENUM)
                {
                    strVariableType = mVariableDef.variableEnumName;
                }
                else if (mVariableDef.eCSharpVariable == EVARIABLE_CSHARP_TYPES.LIST)
                {

                    mManager.log("ERROR - List variable: " + mVariable.variableType + " appears to have a list inside it. This should be possible but I don't want to handle it right now - Marsh out.");
                    return "ERROR - CHECK LOG" + Environment.NewLine;
                }
                bVariableTypeFound = true;
                break;
            }
            if (bVariableTypeFound == false)
            {
                if (mProjectWrapper.enums.ContainsKey(strTempVariableTypeName.ToUpper()))
                {
                    //it's an enum.
                    strVariableType = strTempVariableTypeName;
                }
                if (mProjectWrapper.classStructures.ContainsKey(strTempVariableTypeName.ToUpper()))
                {
                    //it's a class structure.
                    strVariableType = strTempVariableTypeName;
                }
            }


            if (strVariableType == "")
            {
                mManager.log("ERROR - UNKNOWN VARIABLE TYPE FOR LIST: " + mVariable.variableType);
                return "ERROR - CHECK LOG" + Environment.NewLine;
            }



            string strClassLine = "";
            string strType = "List<" + strVariableType + ">";

            strClassLine = strClassLine + "private " + strType + " _" + mVariable.variableName + " = new " + strType + "();" + Environment.NewLine;
            strClassLine = strClassLine + _writeVaraibleComponentModelDetails(mVariable);
            strClassLine = strClassLine + "public " + strType + " " + mVariable.variableName + Environment.NewLine;
            strClassLine = strClassLine + "{" + Environment.NewLine;
            strClassLine = strClassLine + "    get{ return _" + mVariable.variableName + "; }" + Environment.NewLine;
            strClassLine = strClassLine + "    set{ _" + mVariable.variableName + " = value; _notifyOfPropertyChanged(\"" + mVariable.variableName + "\"); }" + Environment.NewLine;
            strClassLine = strClassLine + "}" + Environment.NewLine;
            return strClassLine;

        }

    }//end class

} //end namespace
