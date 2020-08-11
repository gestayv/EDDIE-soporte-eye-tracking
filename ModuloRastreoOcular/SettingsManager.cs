using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ModuloLog;


namespace ModuloRastreoOcular
{
    public class SettingsManager
    {
        StandardLogging logSettings;

        public SettingsManager()
        {
            logSettings = new StandardLogging();
        }

        public FormAttributes LoadSettings(string fileRoute)
        {
            try
            {
                string jsonConfig = File.ReadAllText(fileRoute);
                FormAttributes fAttributes = JsonConvert.DeserializeObject<FormAttributes>(jsonConfig);
                return fAttributes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool SaveSettings(string fileRoute, List<object> controls)
        {
            FormAttributes fAttributes  = new FormAttributes();
            try
            {
                fAttributes.pluginsRoute = (string)controls.ElementAt(0);
                fAttributes.pluginsIndex = (int)controls.ElementAt(1);
                fAttributes.reticlesRoute = (string)controls.ElementAt(2);
                fAttributes.reticlesIndex = (int)controls.ElementAt(3);
                fAttributes.mouseControl = (bool)controls.ElementAt(4);
                fAttributes.clickTime = (int)controls.ElementAt(5);
                fAttributes.saveData = (bool)controls.ElementAt(6);
                fAttributes.fileName = (string)controls.ElementAt(7);
                fAttributes.fileRoute = (string)controls.ElementAt(8);
                // ASSembly conflict
                string jsonConfig = JsonConvert.SerializeObject(fAttributes);
                logSettings.CreateLogTarget("", fileRoute);
                logSettings.WriteToLog(jsonConfig);
                logSettings.CloseLogTarget();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
    }

    public class FormAttributes
    {
        public string pluginsRoute { get; set; }
        public int pluginsIndex { get; set; }
        public string reticlesRoute { get; set; }
        public int reticlesIndex { get; set; }
        public bool mouseControl { get; set; }
        public int clickTime { get; set; }
        public bool saveData { get; set; }
        public string fileName { get; set; }
        public string fileRoute { get; set; }
    }
}
