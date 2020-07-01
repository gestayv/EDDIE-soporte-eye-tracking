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
using ModuloRastreoOcular;  


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

            reticleExample.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/RecursosEyeTracking/" + "reticle_1.png");
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

            ClaseIntermedia test = ClaseIntermedia.GetInstance();
            test.pluginAssembly = pluginAssembly;
            test.setUpAssembly();
            test.PropertyChanged += TestReticleEvent;
        }

        public void TestReticleEvent(object sender, PropertyChangedEventArgs e)
        {
            ClaseIntermedia test2 = ClaseIntermedia.GetInstance();
            int x = (int)Double.Parse(test2.Data["X_Coordinate"]);
            int y = (int)Double.Parse(test2.Data["Y_Coordinate"]);
            reticleExample.Left = x;
            reticleExample.Top = y;
        }

    }
}
