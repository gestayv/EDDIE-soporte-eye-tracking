namespace AugmentedReadingApp
{
    partial class EyeTrackingConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackingPlugins = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveChanges = new System.Windows.Forms.Button();
            this.cancelChanges = new System.Windows.Forms.Button();
            this.reticleExample = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.reticleSelected = new System.Windows.Forms.ComboBox();
            this.controlMouse = new System.Windows.Forms.CheckBox();
            this.reticleDimensions = new System.Windows.Forms.Label();
            this.toolTipMouseControl = new System.Windows.Forms.ToolTip(this.components);
            this.saveData = new System.Windows.Forms.CheckBox();
            this.toolTipSaveData = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.reticleExample)).BeginInit();
            this.SuspendLayout();
            // 
            // trackingPlugins
            // 
            this.trackingPlugins.FormattingEnabled = true;
            this.trackingPlugins.Location = new System.Drawing.Point(151, 34);
            this.trackingPlugins.Name = "trackingPlugins";
            this.trackingPlugins.Size = new System.Drawing.Size(149, 21);
            this.trackingPlugins.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plugin de rastreo ocular";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveChanges
            // 
            this.saveChanges.Location = new System.Drawing.Point(144, 248);
            this.saveChanges.Name = "saveChanges";
            this.saveChanges.Size = new System.Drawing.Size(75, 23);
            this.saveChanges.TabIndex = 2;
            this.saveChanges.Text = "Aplicar";
            this.saveChanges.UseVisualStyleBackColor = true;
            this.saveChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.saveChanges_MouseClick);
            // 
            // cancelChanges
            // 
            this.cancelChanges.Location = new System.Drawing.Point(225, 248);
            this.cancelChanges.Name = "cancelChanges";
            this.cancelChanges.Size = new System.Drawing.Size(75, 23);
            this.cancelChanges.TabIndex = 3;
            this.cancelChanges.Text = "Cerrar";
            this.cancelChanges.UseVisualStyleBackColor = true;
            this.cancelChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cancelChanges_MouseClick);
            // 
            // reticleExample
            // 
            this.reticleExample.BackColor = System.Drawing.Color.Transparent;
            this.reticleExample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reticleExample.Location = new System.Drawing.Point(151, 124);
            this.reticleExample.Name = "reticleExample";
            this.reticleExample.Size = new System.Drawing.Size(44, 44);
            this.reticleExample.TabIndex = 4;
            this.reticleExample.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Retícula";
            // 
            // reticleSelected
            // 
            this.reticleSelected.FormattingEnabled = true;
            this.reticleSelected.Location = new System.Drawing.Point(151, 85);
            this.reticleSelected.Name = "reticleSelected";
            this.reticleSelected.Size = new System.Drawing.Size(149, 21);
            this.reticleSelected.TabIndex = 7;
            this.reticleSelected.SelectedIndexChanged += new System.EventHandler(this.reticleSelected_SelectedIndexChanged);
            // 
            // controlMouse
            // 
            this.controlMouse.AutoSize = true;
            this.controlMouse.Location = new System.Drawing.Point(15, 187);
            this.controlMouse.Name = "controlMouse";
            this.controlMouse.Size = new System.Drawing.Size(103, 17);
            this.controlMouse.TabIndex = 8;
            this.controlMouse.Text = "Controlar Mouse";
            this.controlMouse.UseVisualStyleBackColor = true;
            this.controlMouse.MouseHover += new System.EventHandler(this.controlMouse_MouseHover);
            // 
            // reticleDimensions
            // 
            this.reticleDimensions.AutoSize = true;
            this.reticleDimensions.Location = new System.Drawing.Point(201, 140);
            this.reticleDimensions.Name = "reticleDimensions";
            this.reticleDimensions.Size = new System.Drawing.Size(48, 13);
            this.reticleDimensions.TabIndex = 6;
            this.reticleDimensions.Text = "Retícula";
            // 
            // saveData
            // 
            this.saveData.AutoSize = true;
            this.saveData.Location = new System.Drawing.Point(15, 210);
            this.saveData.Name = "saveData";
            this.saveData.Size = new System.Drawing.Size(95, 17);
            this.saveData.TabIndex = 10;
            this.saveData.Text = "Guardar Datos";
            this.saveData.UseVisualStyleBackColor = true;
            // 
            // EyeTrackingConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 283);
            this.Controls.Add(this.saveData);
            this.Controls.Add(this.controlMouse);
            this.Controls.Add(this.reticleSelected);
            this.Controls.Add(this.reticleDimensions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reticleExample);
            this.Controls.Add(this.cancelChanges);
            this.Controls.Add(this.saveChanges);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackingPlugins);
            this.DoubleBuffered = true;
            this.Name = "EyeTrackingConfiguration";
            this.Text = "Configuración Rastreo Ocular";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reticleExample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox trackingPlugins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveChanges;
        private System.Windows.Forms.Button cancelChanges;
        private System.Windows.Forms.PictureBox reticleExample;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox reticleSelected;
        private System.Windows.Forms.CheckBox controlMouse;
        private System.Windows.Forms.Label reticleDimensions;
        private System.Windows.Forms.ToolTip toolTipMouseControl;
        private System.Windows.Forms.CheckBox saveData;
        private System.Windows.Forms.ToolTip toolTipSaveData;
    }
}