﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library.CommandLine;
using Library.IO;
using Library.DataGroup;
namespace CommandLineSerializer
{
	public class SerializerController
	{
		private LogFile m_LogFile = null;
		private List<HeaderFile> m_HeaderFiles = new List<HeaderFile>();
		private List<ProcessHeaderFileList> m_Processes = new List<ProcessHeaderFileList>();
		private SerializerConfigFile m_ConfigFile = new SerializerConfigFile();
		public SerializerController(string[] args)
		{
			
			_createCommandLineArguments(args);
			_createLogFile();
			if(_createIntermediateDirectory() == false) 
			{
				log("Closing down");
				Environment.Exit(0);
				return;
			}
			_loadConfigFile();
			if( _findHeaders() == false )
			{
				log("Closing down");
				Environment.Exit(0);
				return;
			}
			_startProcessingHeaders();
			_saveConfigFile();
		}
		//creates the command line arguments
		private void _createCommandLineArguments(string[] args)
		{
			commandLineArguments = new CommandLineArguments();
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-Help", "-?" }, "Prints out all the commands"));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-Debug", "-d" }, "Shows all the values of the commands processed"));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-DisplayOff", "-DO" }, "Will make it so that logging info won't be printed to the screen."));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-LogFile", "-LF" }, "The log file. Must include the file name.", ""));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-ForceRecompile", "-FR" }, "Forces all headers to recompile."));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-SourceDir", "-SD" }, "the directory where the header files will be parsed", ""));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-IntermediateDir", "-ID" }, "the intermediate directory where the source files will be generated. The project must point there.", ""));
			commandLineArguments.addCommandLineOption(new CommandLineOption(new string[] { "-MaxThreads", "-MT" }, "Max threads allowed to process headers. Uses Max - 1", Environment.ProcessorCount - 1));
			commandLineArguments.parseArguments(args);
			if (commandLineArguments.getCommandValueAsString("-LogFile") == "")
			{
				commandLineArguments.setCommand("-LogFile", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CommandLineSerializer.log"));
			}
			
			if (commandLineArguments.getCommandValueAsBool("-?"))
			{
				Console.WriteLine(commandLineArguments.getCommandsAsAstring());
			}
			if (commandLineArguments.getCommandValueAsBool("-Debug"))
			{
				Console.WriteLine(commandLineArguments.getCommandsAsDisplayString());
			}
		}

		private bool _createIntermediateDirectory()
		{
			string strPathToIntermediateDir = commandLineArguments.getCommandValueAsString("-IntermediateDir");
			if (Directory.Exists(strPathToIntermediateDir) == false)
			{
				Directory.CreateDirectory(strPathToIntermediateDir);
				if (Directory.Exists(strPathToIntermediateDir) == false)
				{
					log("Unable to create directory: " + strPathToIntermediateDir);
					return false;
				}
			}
			return true;
		}
		private void _loadConfigFile()
		{
			
			string strFullPath = Path.Combine(commandLineArguments.getCommandValueAsString("-IntermediateDir"), "CommandLineSerializer.cfg");
			
			string strError = "";
			DataGroup mDataGroupTest = DataGroup.createFromFile(strFullPath, ref strError);
			m_ConfigFile = DataGroup.deserializeObjectFromFile(strFullPath, ref strError) as SerializerConfigFile;
			if( strError != "" )
			{
				log("ERROR - " + strError);
				m_ConfigFile = new SerializerConfigFile();
			}
			m_ConfigFile.initialize( this, "CommandLineSerializer.cfg");
			
		}

		private void _saveConfigFile()
		{
			m_ConfigFile.clearHeaderFiles();
			foreach(HeaderFile mFile in m_HeaderFiles)
			{
				m_ConfigFile.addHeaderFile(mFile);
			}
			m_ConfigFile.save(commandLineArguments.getCommandValueAsString("-IntermediateDir"));
		}

		private void _startProcessingHeaders()
		{
			int iThreads = Math.Max(1, commandLineArguments.getCommandValueAsInt("-MaxThreads"));
			
			for( int iProcessIndex = 0; iProcessIndex < iThreads; iProcessIndex++)
			{
				m_Processes.Add(new ProcessHeaderFileList());
			}
			int iCurrentProcessBucket = 0;
			bool bForce = commandLineArguments.getCommandValueAsBool("-ForceRecompile");
			foreach (HeaderFile mHeaderFile in m_HeaderFiles)
			{
				if(bForce == false &&
					m_ConfigFile.getHeaderFileNeedsToRecompile(mHeaderFile) == false )
				{
					log(mHeaderFile.headerFile + " is up to date.");
				}
				else
				{
					log(mHeaderFile.headerFile + " recompiling header.");
				}
				
				m_Processes[iCurrentProcessBucket].addHeaderFileToProcess(mHeaderFile);
				iCurrentProcessBucket++;
				if( iCurrentProcessBucket >= iThreads)
				{
					iCurrentProcessBucket = 0;
				}
			}
			log("Processing headers on " + iThreads.ToString() + " processors.");
			for (int iProcessIndex = 0; iProcessIndex < m_Processes.Count; iProcessIndex++)
			{
				log("Thread_" + iProcessIndex.ToString() + ") Processing " + m_Processes[iProcessIndex].getNumberOfHeadersRemainingToProcess().ToString() + " headers.");
			}
			
		}

		
		//returns the command line arguments
		public CommandLineArguments commandLineArguments { get; set; }

		//returns if it should be showing output to the screen
		public bool showDebugInfo { get; set;}

		//returns if it should be logging output
		public bool logOutput { get; set; }

		private void _createLogFile()
		{
			////attempt to log and say if the console will print or not
			m_LogFile = new LogFile(commandLineArguments.getCommandValueAsString("-LogFile"), 100, !commandLineArguments.getCommandValueAsBool("-DisplayOff"));
			if (m_LogFile.getValidLogFile())
			{
				log("Log file created at: " + commandLineArguments.getCommandValueAsString("-LogFile"));
			}
			else
			{
				log("Unable to create log file at: " + commandLineArguments.getCommandValueAsString("-LogFile"));
			}
			log("Console output is turned : " + ((!commandLineArguments.getCommandValueAsBool("-DisplayOff"))?"On":"Off"));
		}

		//logs the line and prints to the debug window
		public void log(string strLine) { m_LogFile.log(strLine); }

		public bool _findHeaders()
		{
			string strPathToIntermediateDir = commandLineArguments.getCommandValueAsString("-IntermediateDir");

			string[] mFiles = Directory.GetFiles(commandLineArguments.getCommandValueAsString("-SourceDir"), "*.h", SearchOption.AllDirectories);
			foreach( string strFile in mFiles)
			{
				HeaderFile mHeaderFile = new HeaderFile();
				string strFileName = Path.GetFileName(strFile);

				string strExportedHeaderFile = Path.Combine(strPathToIntermediateDir, Path.GetFileNameWithoutExtension(strFile) + "serialize.h");
				mHeaderFile.initialize(strFile, strExportedHeaderFile);
				m_HeaderFiles.Add(mHeaderFile);
			}
			if(m_HeaderFiles.Count == 0 )
			{
				log("No header files found in directory: " + commandLineArguments.getCommandValueAsString("-SourceDir"));
				return false;
			}
			log( m_HeaderFiles.Count.ToString() + " header files found in directory: " + commandLineArguments.getCommandValueAsString("-SourceDir"));
			return true;
		}


	}
}
