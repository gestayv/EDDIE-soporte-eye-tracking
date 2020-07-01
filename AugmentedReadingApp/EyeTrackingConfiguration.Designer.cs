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
            this.trackingPlugins = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveChanges = new System.Windows.Forms.Button();
            this.cancelChanges = new System.Windows.Forms.Button();
            this.reticleExample = new System.Windows.Forms.PictureBox();
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
            this.saveChanges.Location = new System.Drawing.Point(144, 234);
            this.saveChanges.Name = "saveChanges";
            this.saveChanges.Size = new System.Drawing.Size(75, 23);
            this.saveChanges.TabIndex = 2;
            this.saveChanges.Text = "Ok";
            this.saveChanges.UseVisualStyleBackColor = true;
            this.saveChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.saveChanges_MouseClick);
            // 
            // cancelChanges
            // 
            this.cancelChanges.Location = new System.Drawing.Point(225, 234);
            this.cancelChanges.Name = "cancelChanges";
            this.cancelChanges.Size = new System.Drawing.Size(75, 23);
            this.cancelChanges.TabIndex = 3;
            this.cancelChanges.Text = "Cancel";
            this.cancelChanges.UseVisualStyleBackColor = true;
            this.cancelChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cancelChanges_MouseClick);
            // 
            // reticleExample
            // 
            this.reticleExample.BackColor = System.Drawing.Color.Transparent;
            this.reticleExample.Location = new System.Drawing.Point(119, 96);
            this.reticleExample.Name = "reticleExample";
            this.reticleExample.Size = new System.Drawing.Size(100, 92);
            this.reticleExample.TabIndex = 4;
            this.reticleExample.TabStop = false;
            // 
            // EyeTrackingConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 269);
            this.Controls.Add(this.reticleExample);
            this.Controls.Add(this.cancelChanges);
            this.Controls.Add(this.saveChanges);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackingPlugins);
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
    }
}