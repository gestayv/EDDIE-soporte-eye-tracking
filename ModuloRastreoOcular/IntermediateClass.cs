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
        
        //  Attributes for controling mouse movement
        private bool mouseControl;
        private MouseControl controller;

        //  Attributes for saving data to file
        private bool saveData;
        private EyeTrackingLogging logging;

        //  Attributes for drawing a reticle to screen
        private ReticleDrawing drawingClass;

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

        //  Returns an unique instance of the class, ensuring it behaves like a singleton
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
        public void initializeClass(Assembly pluginAssembly, bool mouseControl, bool saveData, Image reticle)
        {
            this.pluginAssembly = pluginAssembly;
            this.mouseControl = mouseControl;
            this.saveData = saveData;
            if (reticle != null)
            {
                //  If a reticle is selected, the class for managing its drawing is created
                drawingClass = new ReticleDrawing();
                drawingClass.reticle = reticle;
            }
            if (saveData)
            {
                //  If saveData was selected, a file where contents will be written is created.
                logging = new EyeTrackingLogging();
                string loggingDirectory = Directory.GetCurrentDirectory() + "//Eye Tracking Logging";
                string fileName = DateTime.Now.ToString("dd-M-yyyy_HH-mm-ss");
                logging.dataLoggger = logging.CreateLogTarget(loggingDirectory, fileName+".csv");
            }
            if (mouseControl)
            {
                controller = new MouseControl();
            }

        }

        // TODO: revisar si necesito limpiar el assembly cuando se hace un cambio de plugin en la marcha.
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
        /// Method used in the subscription to "PropertyChanged" of the eye tracking plugin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeCoordinates(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo assemblyData               =   assemblyType.GetProperty("Data");
            Dictionary<string, string> Coordinates  =   (Dictionary<string, string>)assemblyData.GetValue(assemblyInstance);
            Data = Coordinates;
            //if (mouseControl) 
            //{
            //    MoveCursor(Int32.Parse(Data["X_Coordinate"]), Int32.Parse(Data["Y_Coordinate"]));
            //}
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

        //private void MoveCursor(int xCoordinate, int yCoordinate)
        //{
        //    Cursor cursorEyeMovement = new Cursor(Cursor.Current.Handle);
        //    Cursor.Position = new Point(xCoordinate, yCoordinate);
        //    // this.Location/Size es de la form
        //    Cursor.Clip = new Rectangle(currentForm.Location, currentForm.Size);
        //}
    }
}   
