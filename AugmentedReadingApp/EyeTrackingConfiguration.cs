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

        private string pluginsCurrentRoute = Directory.GetCurrentDirectory() + "\\PluginsEyeTracking\\";
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

        /// <summary>
        /// Initializes a combobox in a winform, using the names of the files of a certain format,
        /// of a specific directory, as the items of said combobox.
        /// </summary>
        /// <param name="target">Combobox to be initialized</param>
        /// <param name="route">Route of the files to be used to fill the combobox</param>
        /// <param name="fileFormat">Format of the files to be used to fill the combobox</param>
        private void InitializeComboBox(ComboBox target, string route, string fileFormat)
        {
            if (target.Items.Count > 0)
                target.Items.Clear();

            string[] items = Directory.GetFiles(route, fileFormat).Select(p => Path.GetFileName(p)).ToArray();
            target.Items.Add("None");
            target.Items.AddRange(items);
            target.SelectedIndex = 0;
        }

        /// <summary>
        /// Method to use the current eyetracking configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            IntermediateClass interClass = IntermediateClass.GetInstance();
            string selectedPlugin = ((trackingPlugins.Text == "None") ? null : pluginsCurrentRoute + trackingPlugins.Text);
            string selectedReticle = ((reticleSelected.Text == "None") ? null : reticlesCurrentRoute + reticleSelected.Text);

            if (trackingPlugins.Text != null)
            {
                try
                {
                    
                    interClass.InitializeClass(selectedPlugin, selectedReticle, controlMouse.Checked, Decimal.ToInt32(clickTimer.Value), saveData.Checked, dataSaveCurrentRoute, fileNameTextBox.Text);
                    interClass.SetUpAssembly();
                    MessageBox.Show("Configuración seleccionada con éxito");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un plugin");
            }
        }

        /// <summary>
        /// Method to cancel the eyetracking configuration.
        /// </summary>
        /// <param name="sender"></param>   
        /// <param name="e"></param>
        private void CancelChanges_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to reset the current eye tracking configuration being used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetConfig_Click(object sender, EventArgs e)
        {
            IntermediateClass intermediate = IntermediateClass.GetInstance();
            intermediate.ClearClass();
            resetConfig.Enabled = false;
        }

        private void ReticleSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReticle = reticlesCurrentRoute + "\\" + reticleSelected.Text;
            if (reticleSelected.Text != "None")
            {
                Image reticle = Image.FromFile(selectedReticle);
                reticleExample.Image = reticle;
                reticleDimensions.Text = "Ancho: " + reticle.Width + "px\nAlto: " + reticle.Width + "px";
            }
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

        
        // Configuration loading - saving methods
        private void loadConfig_Click(object sender, EventArgs e)
        {
            openFileConfig.Title = "Seleccione un archivo para cargar una configuración";
            openFileConfig.Filter = "Archivos de texto (*.txt)|*.txt|Archivo JSON (*.json)|*.json";
            SettingsManager settings = new SettingsManager();
            if (openFileConfig.ShowDialog() == DialogResult.OK)
            {
                
                //  Opening settings
                var filePath = openFileConfig.FileName;
                FormAttributes settingsAttribute = settings.LoadSettings(filePath);
                if(settingsAttribute != null)
                {
                    pluginsRoute.Text = settingsAttribute.pluginsRoute;
                    InitializeComboBox(trackingPlugins, pluginsRoute.Text, "*.dll");
                    trackingPlugins.SelectedIndex = settingsAttribute.pluginsIndex;
                    reticlesRoute.Text = settingsAttribute.reticlesRoute;
                    InitializeComboBox(reticleSelected, reticlesRoute.Text, "*.png");
                    reticleSelected.SelectedIndex = settingsAttribute.reticlesIndex;
                    controlMouse.Checked = settingsAttribute.mouseControl;
                    clickTimer.Value = settingsAttribute.clickTime;
                    saveData.Checked = settingsAttribute.saveData;
                    fileNameTextBox.Text = settingsAttribute.fileName;
                    saveFileRoute.Text = settingsAttribute.fileRoute;
                    MessageBox.Show("Configuración cargada con éxito");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la configuración seleccionada.");
                }
            }

        }
        
        private void saveConfig_Click(object sender, EventArgs e)
        {
            // dialog configuration
            saveFileConfig.Title = "Seleccione un directorio y un nombre para guardar su configuración";
            saveFileConfig.DefaultExt = "config - " + DateTime.Now.ToString("dd-M-yyyy_HH-mm-ss");
            saveFileConfig.Filter = "Archivos de texto (*.txt)|*.txt|Archivo JSON (*.json)|*.json";
            saveFileConfig.ShowDialog();

            // preparation of data to be saved
            object[] configData = { pluginsRoute.Text, trackingPlugins.SelectedIndex, reticlesRoute.Text, reticleSelected.SelectedIndex,
                controlMouse.Checked, Decimal.ToInt32(clickTimer.Value), saveData.Checked, fileNameTextBox.Text, saveFileRoute.Text };
            List<object> config = new List<object>();
            config.AddRange(configData);

            SettingsManager settings = new SettingsManager();
            if (settings.SaveSettings(saveFileConfig.FileName, config))
            {
                MessageBox.Show("Configuración guardada con éxito");
            }
            else
            {
                MessageBox.Show("No se pudo guardar la configuración seleccionada.");
            }
        }

        // Tooltips
        private void PluginsRouteBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTipBrowsePlugin.Show("Presione el botón para seleccionar una carpeta desde la cual se cargarán\nlos plugins de rastreo ocular.", pluginsRouteBrowse);
        }

        private void ReticlesRouteBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTipBrowseReticle.Show("Presione el botón para seleccionar una carpeta desde la cual se cargarán\nlas distintas retículas.", reticlesRouteBrowse);
        }

        private void SaveFileRouteBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTipBrowseSaveFile.Show("Presione el botón para seleccionar una carpeta en la cual se guardarán los archivos\n" +
                                        "generados a partir de los datos capturados por el dispositivo de rastreo ocular.", saveFileRouteBrowse);
        }

        private void ControlMouse_MouseHover(object sender, EventArgs e)
        {
            toolTipMouseControl.Show("Si se selecciona, el usuario podrá controlar el \npuntero con sus movimientos oculares.", controlMouse);
        }

        private void SaveData_MouseHover(object sender, EventArgs e)
        {
            toolTipSaveData.Show("Si se selecciona, los datos capturados por el eye tracker\nson guardados en un archivo .csv", saveData);
        }
    }
}