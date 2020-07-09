using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfazLogging;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Async;

namespace ModuloRastreoOcular
{
    class EyeTrackingLogging : InterfazLogging.ILogging<Logger>
    {
        public Logger testLogger;

        public bool CloseLogTarget(Logger file)
        {
            try
            {
                file.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Logger CreateLogTarget(string directory, string name)
        {
            string fileRouteName = directory + "\\" + name;

            try
            {
                testLogger = new LoggerConfiguration()
                            .WriteTo.Async(a => a.File(fileRouteName, outputTemplate: "{Message}{NewLine}{Exception}"), bufferSize: 1000, blockWhenFull: true)
                            .CreateLogger();
                return testLogger;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool WriteToLog(Logger file, string contents)
        {
            try
            {
                file.Information(contents);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
