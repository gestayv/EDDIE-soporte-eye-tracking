using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace ModuloRastreoOcular
{
    public class ClaseIntermedia
    {
        //  Attributes for implementing the class as a thread safe singleton
        private static ClaseIntermedia _Instance = null;
        private static readonly object padlock = new object();

        //  Attributes for reflection
        public Assembly pluginAssembly;
        public Type assemblyType;
        public object assemblyInstance;

        //  Attributes for handling new eye tracking data
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<string, string> _Data = new Dictionary<string, string>();

        public Dictionary<string, string> Data 
        {
            get { return _Data; }
            set 
            { 
                _Data = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }

        public static ClaseIntermedia GetInstance() 
        {
            if (_Instance == null) 
            {
                lock (padlock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new ClaseIntermedia();
                    }
                    return _Instance;
                }
            }
            return _Instance;
        }

        // TODO: revisar si necesito limpiar el assembly cuando se hace un cambio de plugin en la marcha.
        public void setUpAssembly() 
        {
            //  Se obtiene el tipo del assembly (primera clase declarada) y se genera una instancia
            assemblyType = pluginAssembly.GetTypes()[0];
            assemblyInstance = Activator.CreateInstance(assemblyType);
             
            //  Se obtiene el evento que se levanta al recibir nuevos datos del eye tracker y se
            //  le asigna un nuevo delegado en esta función.
            EventInfo evPropChange = assemblyType.GetEvent("PropertyChanged");
            Type delegateType = evPropChange.EventHandlerType;

            Delegate propChangeDel = Delegate.CreateDelegate(delegateType, this, "ChangeCoordinates");

            MethodInfo addHandler = evPropChange.GetAddMethod();
            Object[] addHandlerArgs = { propChangeDel };
            addHandler.Invoke(assemblyInstance, addHandlerArgs);

            //  Se invoca el método para establecer conexión con el eye tracker
            MethodInfo assemblyOpenConn = assemblyType.GetMethod("OpenConnection");
            assemblyOpenConn.Invoke(assemblyInstance, new object[] { });
        }

        public void ChangeCoordinates(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo assemblyData = assemblyType.GetProperty("Data");
            Dictionary<string, string> Coordinates = (Dictionary<string, string>)assemblyData.GetValue(assemblyInstance);
            Data = Coordinates;
            //Console.WriteLine("Clase Intermedia - {0}, {1}, {2}", Coordinates["X_Coordinate"], Coordinates["Y_Coordinate"], Coordinates["Timestamp"]);
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
            else
            {
                Console.WriteLine("handler null");
            }
        }
    }
}   
