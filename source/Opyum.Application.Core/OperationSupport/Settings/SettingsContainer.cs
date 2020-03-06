using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Opyum.Core.OperationSupport.Settings
{
    public class SettingsContainer
    {
        /// <summary>
        /// Contains all settings in current document in XML format.
        /// </summary>
        public XmlDocument Settings { get; protected internal set; }

        public int SetSettings(XmlDocument sett)
        {
            return -1;
        }

        /// <summary>
        /// Gets the settings from a file.
        /// </summary>
        /// <param name="path">The path of the settings file</param>
        /// <exception cref="NullReferenceException"/>
        public static void GetSettingsFromFile(string path)
        {
            if (!System.IO.File.Exists(path)) throw new NullReferenceException(message: "Settings file doesn't exits.");
            try
            {
                //Check if the settings file exists and if not, thorw an exception;
                
                

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
