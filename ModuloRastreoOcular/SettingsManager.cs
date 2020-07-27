using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;



// mover guardado de datos oculares al módulo de logging, tratar de generalizar el guardado de datos en una sola clase?

namespace ModuloRastreoOcular
{
    public class SettingsManager : InterfazLogging.ILogging<FileStream>
    {
        FileStream configFile;

        public FormAttributes LoadSettings(string fileRoute)
        {
            string jsonConfig = File.ReadAllText(fileRoute);
            FormAttributes fAttributes = JsonConvert.DeserializeObject<FormAttributes>(jsonConfig);
            return fAttributes;
        }

        public void SaveSettings(string fileRoute, List<object> controls)
        {
            FormAttributes fAttributes  = new FormAttributes();
            fAttributes.pluginsRoute    = (string) controls.ElementAt(0);
            fAttributes.pluginsIndex    = (int) controls.ElementAt(1);
            fAttributes.reticlesRoute   = (string)controls.ElementAt(2);
            fAttributes.reticlesIndex   = (int)controls.ElementAt(3);
            fAttributes.mouseControl    = (bool)controls.ElementAt(4);
            fAttributes.clickTime       = (int)controls.ElementAt(5);
            fAttributes.saveData        = (bool)controls.ElementAt(6);
            fAttributes.fileName        = (string)controls.ElementAt(7);
            fAttributes.fileRoute       = (string)controls.ElementAt(8);
            // ASSembly conflict
            string jsonConfig = JsonConvert.SerializeObject(fAttributes);
            configFile = CreateLogTarget("", fileRoute);
            WriteToLog(configFile, jsonConfig);
            CloseLogTarget(configFile);
        }

        public FileStream CreateLogTarget(string name, string directory)
        {
            try
            {
                return File.Create(directory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public bool WriteToLog(FileStream file, string contents)
        {
            try
            {
                byte[] info = new UTF8Encoding(true).GetBytes(contents);
                file.Write(info, 0, info.Length);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public bool CloseLogTarget(FileStream file)
        {
            try
            {
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
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
