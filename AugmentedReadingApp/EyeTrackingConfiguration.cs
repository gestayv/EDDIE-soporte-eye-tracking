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

// ruta default se asigna en load, para plugins, retículas y guardado de archivos
// cuando se selecciona una nueva ruta:
    // se actualiza el textbox, y para plugins y retículas se actualizan las combobox

namespace AugmentedReadingApp
{
    public partial class EyeTrackingConfiguration : Form
    {
        public Type assemblyType;
        public object assemblyInstance;

        private string pluginsCurrentRoute  = Directory.GetCurrentDirectory() + "\\PluginsEyeTracking\\";
        private string reticlesCurrentRoute = Directory.GetCurrentDirectory() + "\\RecursosEyeTracking\\";
        private string dataSaveCurrentRoute = Directory.GetCurrentDirectory() + "\\Eye Tracking Logging\\";

        public EyeTrackingConfiguration()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  Combobox population
            InitializeComboBox(trackingPlugins, pluginsCurrentRoute, "*.dll");
            InitializeComboBox(reticleSelected, reticlesCurrentRoute, "*.png");
            
            reticleExample.SizeMode = PictureBoxSizeMode.StretchImage;
            reticleDimensions.Text = "Ancho: 0px\nAlto: 0px";

            pluginsRoute.Text = pluginsCurrentRoute;
            reticlesRoute.Text = reticlesCurrentRoute;
            saveFileRoute.Text = dataSaveCurrentRoute;

            IntermediateClass intermediate = IntermediateClass.GetInstance();
            if (intermediate.pluginAssembly == null)
            {
                resetConfig.Enabled = false;
            }
        }

        private void InitializeComboBox(ComboBox target, string route, string fileFormat) 
        {
            if (target.Items.Count > 0)
                target.Items.Clear();

            string[] items = Directory.GetFiles(route, fileFormat).Select(p => Path.GetFileName(p)).ToArray();
            target.Items.Add("None");
            target.Items.AddRange(items);
            target.SelectedIndex = 0;
        }

        private void CancelChanges_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to save the current eyetracking configuration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            IntermediateClass interClass = IntermediateClass.GetInstance();
            string selectedPlugin   =   pluginsCurrentRoute + trackingPlugins.Text;
            string selectedReticle  =   reticlesCurrentRoute + reticleSelected.Text;

            if (trackingPlugins.Text != "")
            {
                try
                {
                    interClass.InitializeClass(selectedPlugin, selectedReticle, controlMouse.Checked, Decimal.ToInt32(clickTimer.Value), saveData.Checked, dataSaveCurrentRoute, fileNameTextBox.Text);
                    interClass.SetUpAssembly();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un plugin");
            }
        }

        private void ResetConfig_Click(object sender, EventArgs e)
        {
            IntermediateClass intermediate = IntermediateClass.GetInstance();
            intermediate.ClearClass();
            resetConfig.Enabled = false;
        }

        private void ReticleSelected_SelectedIndexChanged(object sender, EventArgs e)
        {   
            string selectedReticle = Directory.GetCurrentDirectory() + "\\RecursosEyeTracking\\" + reticleSelected.Text;
            if(reticleSelected.Text != "None")
            {
                Image reticle = Image.FromFile(selectedReticle);
                reticleExample.Image = reticle;
                reticleDimensions.Text = "Ancho: " + reticle.Width + "px\nAlto: " + reticle.Width + "px";
            }
        }

        private void ControlMouse_MouseHover(object sender, EventArgs e)
        {
            toolTipMouseControl.Show("Si se selecciona, el usuario podrá controlar el \npuntero con sus movimientos oculares.", controlMouse);
        }

        private void SaveData_MouseHover(object sender, EventArgs e)
        {
            toolTipSaveData.Show("Si se selecciona, los datos capturados por el eye tracker\nson guardados en un archivo .csv", saveData);
        }

        private void SaveData_CheckedChanged(object sender, EventArgs e)
        {
            if (saveData.Checked)
            {
                fileNameTextBox.Enabled = true;
                saveFileRouteBrowse.Enabled = true;
            }
            else
            {
                fileNameTextBox.Enabled = false;
                saveFileRouteBrowse.Enabled = false;
            }
        }

        private void ControlMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (controlMouse.Checked)
            {
                clickTimer.Enabled = true;
            }
            else
            {
                clickTimer.Enabled = false;
            }
        }

        private void PluginsRoute_Click(object sender, EventArgs e)
        {
            routeBrowser.Description = "Seleccione la carpeta donde se ubican los plugins de rastreo ocular";
            if (routeBrowser.ShowDialog() == DialogResult.OK)
            {
                pluginsRoute.Text = routeBrowser.SelectedPath;
                pluginsCurrentRoute = routeBrowser.SelectedPath;
                InitializeComboBox(trackingPlugins, pluginsCurrentRoute, "*.dll");
            }
        }

        private void ReticlesRoute_Click(object sender, EventArgs e)
        {
            routeBrowser.Description = "Seleccione la carpeta donde se ubican las retículas";
            if (routeBrowser.ShowDialog() == DialogResult.OK)
            {
                reticlesRoute.Text = routeBrowser.SelectedPath;
                reticlesCurrentRoute = routeBrowser.SelectedPath;
                InitializeComboBox(reticleSelected, reticlesCurrentRoute, "*.png");
            }
        }

        private void SaveFileRoute_Click(object sender, EventArgs e)
        {
            routeBrowser.Description = "Seleccione la carpeta donde se desean almacenar los archivos con los datos generados por el eye tracker";
            if (routeBrowser.ShowDialog() == DialogResult.OK)
            {
                saveFileRoute.Text = routeBrowser.SelectedPath;
                dataSaveCurrentRoute = routeBrowser.SelectedPath;
            }
        }
    }
}