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
        public Image newImage;

        public EyeTrackingConfiguration()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  After loading the form, get the name of all assemblies and reticles
            string[] pluginsNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\PluginsEyeTracking\\", "*.dll")
                                                        .Select(p => Path.GetFileName(p)).ToArray();

            string[] reticleNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\RecursosEyeTracking\\", "*.png")
                                                        .Select(p => Path.GetFileName(p)).ToArray();
            //  Assign names to comboboxes
            trackingPlugins.Items.AddRange(pluginsNames);
            reticleSelected.Items.Add("None");
            reticleSelected.Items.AddRange(reticleNames);
            reticleExample.SizeMode = PictureBoxSizeMode.StretchImage;
            reticleDimensions.Text = "Ancho: 0px\nAlto: 0px";
        }

        private void cancelChanges_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        //  Crear un método para inicializar la clase intermedia.
        //  Luego, en la clase intermedia crear un método para inicializar la clase que dibuja la retícula.
        private void saveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            Image reticle = null;
            string selectedPlugin   =   Directory.GetCurrentDirectory() + "\\PluginsEyeTracking\\" + trackingPlugins.Text;
            string selectedReticle  =   Directory.GetCurrentDirectory() + "\\RecursosEyeTracking\\" + reticleSelected.Text;
            if (reticleSelected.Text != "" && reticleSelected.Text != "None")
                reticle = Image.FromFile(selectedReticle);

            if (trackingPlugins.Text != "")
            {
                Assembly pluginAssembly = Assembly.LoadFrom(selectedPlugin);
                IntermediateClass interClass = IntermediateClass.GetInstance();

                interClass.initializeClass(pluginAssembly, controlMouse.Checked, saveData.Checked, reticle);
                interClass.setUpAssembly();
            }
            this.Close();
        }

        private void reticleSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReticle = Directory.GetCurrentDirectory() + "\\RecursosEyeTracking\\" + reticleSelected.Text;
            if(reticleSelected.Text != "None")
            {
                Image reticle = Image.FromFile(selectedReticle);
                reticleExample.Image = reticle;
                reticleDimensions.Text = "Ancho: " + reticle.Width + "px\nAlto: " + reticle.Width + "px";
            }
        }

        private void controlMouse_MouseHover(object sender, EventArgs e)
        {
            toolTipMouseControl.Show("Si se selecciona, el usuario podrá controlar el \npuntero con sus movimientos oculares.", controlMouse);
        }

        private void saveData_MouseHover(object sender, EventArgs e)
        {
            toolTipSaveData.Show("Si se selecciona, los datos capturados por el eye tracker\nson guardados en un archivo .csv", saveData);
        }
    }
}