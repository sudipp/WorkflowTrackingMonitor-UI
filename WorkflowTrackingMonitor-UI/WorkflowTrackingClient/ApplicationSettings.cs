using System;
using System.IO;
using System.Xml.Serialization;

namespace WorkflowTrackingClient
{
    //This class is used to store user settings such as server and database names
    public class ApplicationSettings
    {
        //Set to true if the settings have changed since last saved
        private bool applicationSettingsChanged;

        private string wfTrackingFolderPath;

        private bool autoWFChangeUpadate;
        private bool displayTerminatedWF;

        internal ApplicationSettings()
        {
            applicationSettingsChanged = false;
        }

        //Save app info to the config file
        internal void SaveSettings(string path)
        { 
            if (applicationSettingsChanged)
            {
                StreamWriter writer = null;
                XmlSerializer serializer = null;
                try
                {
                    // Create an XmlSerializer for the 
                    // ApplicationSettings type.
                    serializer = new XmlSerializer(typeof(ApplicationSettings));
                    writer = new StreamWriter(path, false);
                    // Serialize this instance of the ApplicationSettings 
                    // class to the config file.
                    serializer.Serialize(writer, this);
                }
                catch
                {
                }
                finally
                {
                    // If the FileStream is open, close it.
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        //Load app info from the config file
        internal bool LoadAppSettings(string path)
        {
            XmlSerializer serializer = null;
            FileStream fileStream = null;
            bool fileExists = false;

            try
            {
                // Create an XmlSerializer for the ApplicationSettings type.
                serializer = new XmlSerializer(typeof(ApplicationSettings));
                FileInfo info = new FileInfo(path);
                // If the config file exists, open it.
                if (info.Exists)
                {
                    fileStream = info.OpenRead();
                    // Create a new instance of the ApplicationSettings by
                    // deserializing the config file.
                    ApplicationSettings applicationSettings = (ApplicationSettings)serializer.Deserialize(fileStream);

                    applicationSettingsChanged = false;

                    // Assign the property values to this instance of 
                    // the ApplicationSettings class.
                    this.wfTrackingFolderPath = applicationSettings.wfTrackingFolderPath;
                    this.autoWFChangeUpadate = applicationSettings.autoWFChangeUpadate;
                    this.displayTerminatedWF = applicationSettings.displayTerminatedWF;

                    //this.databaseName = applicationSettings.databaseName;

                    fileExists = true;
                }
            }
            catch
            {
            }
            finally
            {
                // If the FileStream is open, close it.
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return fileExists;
        }

        public bool ApplicationSettingsChanged
        {
            get { return applicationSettingsChanged; }
        }

        public string WFTrackingFolderPath
        {
            get { return wfTrackingFolderPath; }
            set
            {
                if (value != wfTrackingFolderPath)
                {
                    wfTrackingFolderPath = value;
                    applicationSettingsChanged = true;
                }
            }
        }

        public bool AutoWFChangeUpadate
        {
            get { return autoWFChangeUpadate; }
            set
            {
                if (value != autoWFChangeUpadate)
                {
                    autoWFChangeUpadate = value;
                    applicationSettingsChanged = true;
                }
            }
        }

        public bool DisplayTerminatedWF
        {
            get { return displayTerminatedWF; }
            set
            {
                if (value != displayTerminatedWF)
                {
                    displayTerminatedWF = value;
                    applicationSettingsChanged = true;
                }
            }
        }
        
        
    }
}
