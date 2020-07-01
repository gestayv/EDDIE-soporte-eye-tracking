using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace AugmentedReadingApp
{
    public partial class EyeTrackingConfiguration : Form
    {
        public Type assemblyType;
        public object assemblyInstance;


        public EyeTrackingConfiguration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get the name of all plugins
            string[] pluginsNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "/PluginsEyeTracking", "Plugin*.dll")
                                                        .Select(p => Path.GetFileName(p)).ToArray();
            // Pass to the combobox
            trackingPlugins.Items.AddRange(pluginsNames);
        }

        private void cancelChanges_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void saveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            string selectedPlugin = Directory.GetCurrentDirectory() + "\\PluginsEyeTracking\\" + trackingPlugins.Text;

            //  The selected plugin's assembly is loaded from the full path.
            Assembly pluginAssembly = Assembly.LoadFrom(selectedPlugin);



            //  The assembly is instantiated assuming the class to be used is always the first Type defined
            //  This should be assigned to a global object that is created in the main form, so everyone can access to it.
            //  I also need a way of saving configurations (mouse control, reticle design?), so the program can know at any moment
            //  how the eye tracking is supossed to behave.
            assemblyType = pluginAssembly.GetTypes()[0];

            // todo lo que sigue debería manejarlo otra clase, la asignación previa es sobre un parámetro de la clase
            assemblyInstance = Activator.CreateInstance(assemblyType);
            
            EventInfo evPropChange = assemblyType.GetEvent("PropertyChanged");
            Type delegateType = evPropChange.EventHandlerType;

            Delegate propChangeDel = Delegate.CreateDelegate(delegateType, this, "ChangeCoordinates");

            MethodInfo addHandler = evPropChange.GetAddMethod();
            Object[] addHandlerArgs = { propChangeDel };
            addHandler.Invoke(assemblyInstance, addHandlerArgs);

            
            MethodInfo assemblyOpenConn = assemblyType.GetMethod("OpenConnection");
            assemblyOpenConn.Invoke(assemblyInstance, new object[] { });

            Console.WriteLine();

            // Tengo mi singleton, este qué hace? es el que se conecta con el assembly del plugin, pero es realmente necesario?
            // mi singleton no debería ser la instancia del assembly en cuestion? o el singleton debería contener la instancia del assembly?
        }

        public void ChangeCoordinates(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo Data = assemblyType.GetProperty("Data");
            Dictionary<string, string> Coordinates =  (Dictionary<string, string>)Data.GetValue(assemblyInstance);
            Console.WriteLine("Clase Intermedia - {0}, {1}, {2}", Coordinates["X_Coordinate"], Coordinates["Y_Coordinate"], Coordinates["Timestamp"]);
        }
    }
}
