using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ModuloRastreoOcular
{
    /// <summary>
    /// Singleton class used for managing functionalities related to eye tracking.
    /// </summary>
    public class IntermediateClass
    {
        //  Attributes for implementing the class as a thread safe singleton
        private static IntermediateClass _Instance  =   null;
        private static readonly object padlock      =   new object();

        //  Attributes for reflection
        public Assembly pluginAssembly;
        public Type assemblyType;
        public object assemblyInstance;
            
        //  Attributes for handling new eye tracking data
        private Dictionary<string, string> _Data = new Dictionary<string, string>();
        public event PropertyChangedEventHandler PropertyChanged;

        //  Attributes for controling mouse movement, logging data, drawing the reticle, and generate clicks.
        private MouseControl controller;
        private EyeTrackingLogging logging;
        private ReticleDrawing drawingClass;
        private ClickCountdown generateClicks;
        private bool mouseControl;
        private bool saveData;

        //  Dictionary where the eye tracking data is kept
        public Dictionary<string, string> Data 
        {
            get { return _Data; }
            set 
            { 
                _Data = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }

        public static IntermediateClass GetInstance() 
        {
            if (_Instance == null) 
            {
                lock (padlock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new IntermediateClass();
                    }
                    return _Instance;
                }
            }
            return _Instance;
        }

        /// <summary>
        /// Method for initializating IntermediateClasss
        /// </summary>
        /// <param name="pluginAssembly">Assembly corresponding to the eye tracking plugin selected</param>
        /// <param name="mouseControl">Bool value that indicates if the user will control the mouse with its gaze</param>
        /// <param name="saveData">Bool value that indicates if the eye tracking data will be saved to a file</param>
        /// <param name="reticle">Image containing the reticle selected, can be null.</param>
        public void initializeClass(string pluginName, bool mouseControl, bool saveData, Image reticle)
        {
            this.pluginAssembly = Assembly.LoadFrom(pluginName);
            this.mouseControl   = mouseControl;
            this.saveData       = saveData;
            
            //  If a reticle is selected, the class for managing its drawing is created
            if  (reticle != null)
            {
                if(drawingClass != null)
                {
                    drawingClass.clearUp();
                }
                drawingClass = new ReticleDrawing();
                drawingClass.reticle = reticle;
            }
            else if (reticle == null && drawingClass != null)
            {
                // Limpiar pantalla de drawing class, eliminar todo?
                drawingClass.clearUp();
                drawingClass = null;
            }
            
            //  If saveData was selected, a file where contents will be written is created, after checking if a file has been
            //  created before (in which case, it's closed before creating the new file).
            if (saveData)
            {
                if(logging != null)
                {
                    logging.CloseLogTarget(logging.dataLoggger);
                }
                logging = new EyeTrackingLogging();
                string loggingDirectory = Directory.GetCurrentDirectory() + "//Eye Tracking Logging";
                string fileName = DateTime.Now.ToString("dd-M-yyyy_HH-mm-ss");
                logging.dataLoggger = logging.CreateLogTarget(loggingDirectory, fileName + ".csv");
            }
            
            //  If mouseControl was selected, the class for controlling the mouse through eye movements is instantiated
            if (mouseControl)
            {
                if(controller == null)
                {
                    controller = new MouseControl();
                    
                }
                if (generateClicks == null)
                {
                    generateClicks = new ClickCountdown(2000);
                }
                generateClicks.executeClick = mouseControl;
            }

        }

        /// <summary>
        /// Method for cleaning up the class, leaving it in the same state as before executing
        /// the method "initializeClass"
        /// </summary>
        public void clearClass() 
        {
            pluginAssembly      =   null;
            assemblyType        =   null;
            assemblyInstance    =   null;
            mouseControl        =   false;
            saveData            =   false;
            if (controller != null)
            {
                controller = null;
            }
            if (logging != null)
            {
                logging.CloseLogTarget(logging.dataLoggger);
                logging = null;
            }
            if (drawingClass != null)
            {
                drawingClass.clearUp();
                drawingClass = null;
            }
            //if(generateClicks != null)
            //{
                
            //}
        }

        /// <summary>
        /// Method for passing a form to the class in charge of handling click generation
        /// </summary>
        /// <param name="formReceived"></param>
        public void setEventsEyeTracking(Form formReceived) 
        {
            if (generateClicks == null)
                generateClicks = new ClickCountdown(2000);
            generateClicks.AssignEvent(formReceived);
        }

        /// <summary>
        /// Method that uses reflection to set up the eye tracking plugin that has been selected, specifically:
        /// 1. Subscribing to the "PropertyChanged" event (in order to detect when new eye tracking data is received)
        /// 2. Calling the "OpenConnection" method, to startup the connection with the eye tracker device.
        /// </summary>
        public void setUpAssembly() 
        {
            
            assemblyType        =   pluginAssembly.GetTypes()[0];
            assemblyInstance    =   Activator.CreateInstance(assemblyType);

            EventInfo evPropChange  =   assemblyType.GetEvent("PropertyChanged");
            Type delegateType       =   evPropChange.EventHandlerType;

            Delegate propChangeDel  =   Delegate.CreateDelegate(delegateType, this, "ChangeCoordinates");
            MethodInfo addHandler   =   evPropChange.GetAddMethod();
            Object[] addHandlerArgs =   { propChangeDel };
            addHandler.Invoke(assemblyInstance, addHandlerArgs);

            MethodInfo assemblyOpenConn =   assemblyType.GetMethod("OpenConnection");
            assemblyOpenConn.Invoke(assemblyInstance, new object[] { });
        }

        /// <summary>
        /// Method used in the subscription to "PropertyChanged" of the eye tracking plugin, it manages all actions linked
        /// to the eye tracker, whenever new data is received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeCoordinates(object sender, PropertyChangedEventArgs e)
        {
            //  First the property where the eye tracking data is saved is recovered
            PropertyInfo assemblyData               =   assemblyType.GetProperty("Data");
            Dictionary<string, string> Coordinates  =   (Dictionary<string, string>)assemblyData.GetValue(assemblyInstance);
            Data = Coordinates;

            //  Then, for each action linked to the new eye tracking coordinates, the program determines what to execute and what not to.
            //  These actions beings (up until now), drawing the reticle, saving the new data to a file and controlling the mouse's position.
            if (drawingClass != null)
            {
                drawingClass.updateData(Data["X_Coordinate"], Data["Y_Coordinate"]);
            }
            if (saveData)
            {
                logging.WriteToLog(logging.dataLoggger, Data["X_Coordinate"] + ", " + Data["Y_Coordinate"] + ", " + Data["Timestamp"]);
            }
            if (mouseControl)
            {
                controller.MoveCursor(Data["X_Coordinate"], Data["Y_Coordinate"]);
            }
        }

        /// <summary>
        /// Method fired when the property Data of the class is modified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}   
