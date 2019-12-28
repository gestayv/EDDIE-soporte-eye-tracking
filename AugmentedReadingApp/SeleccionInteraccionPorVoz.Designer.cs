namespace AugmentedReadingApp
{
    partial class SeleccionInteraccionPorVoz
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
            this.btn_guardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtn_voz_no = new System.Windows.Forms.RadioButton();
            this.rbtn_voz_si = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtn_no_botones = new System.Windows.Forms.RadioButton();
            this.rbtn_Si_botones = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(301, 209);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 14;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Activar interacción por voz:";
            // 
            // rbtn_voz_no
            // 
            this.rbtn_voz_no.AutoSize = true;
            this.rbtn_voz_no.Location = new System.Drawing.Point(18, 57);
            this.rbtn_voz_no.Name = "rbtn_voz_no";
            this.rbtn_voz_no.Size = new System.Drawing.Size(39, 17);
            this.rbtn_voz_no.TabIndex = 12;
            this.rbtn_voz_no.TabStop = true;
            this.rbtn_voz_no.Text = "No";
            this.rbtn_voz_no.UseVisualStyleBackColor = true;
            // 
            // rbtn_voz_si
            // 
            this.rbtn_voz_si.AutoSize = true;
            this.rbtn_voz_si.Location = new System.Drawing.Point(18, 34);
            this.rbtn_voz_si.Name = "rbtn_voz_si";
            this.rbtn_voz_si.Size = new System.Drawing.Size(34, 17);
            this.rbtn_voz_si.TabIndex = 11;
            this.rbtn_voz_si.TabStop = true;
            this.rbtn_voz_si.Text = "Si";
            this.rbtn_voz_si.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbtn_no_botones);
            this.panel1.Controls.Add(this.rbtn_Si_botones);
            this.panel1.Location = new System.Drawing.Point(12, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "¿Mostrar botones?";
            // 
            // rbtn_no_botones
            // 
            this.rbtn_no_botones.AutoSize = true;
            this.rbtn_no_botones.Location = new System.Drawing.Point(6, 39);
            this.rbtn_no_botones.Name = "rbtn_no_botones";
            this.rbtn_no_botones.Size = new System.Drawing.Size(39, 17);
            this.rbtn_no_botones.TabIndex = 17;
            this.rbtn_no_botones.TabStop = true;
            this.rbtn_no_botones.Text = "No";
            this.rbtn_no_botones.UseVisualStyleBackColor = true;
            // 
            // rbtn_Si_botones
            // 
            this.rbtn_Si_botones.AutoSize = true;
            this.rbtn_Si_botones.Location = new System.Drawing.Point(6, 16);
            this.rbtn_Si_botones.Name = "rbtn_Si_botones";
            this.rbtn_Si_botones.Size = new System.Drawing.Size(34, 17);
            this.rbtn_Si_botones.TabIndex = 16;
            this.rbtn_Si_botones.TabStop = true;
            this.rbtn_Si_botones.Text = "Si";
            this.rbtn_Si_botones.UseVisualStyleBackColor = true;
            // 
            // SeleccionInteraccionPorVoz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 251);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtn_voz_no);
            this.Controls.Add(this.rbtn_voz_si);
            this.Name = "SeleccionInteraccionPorVoz";
            this.Text = "SeleccionInteraccionPorVoz";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtn_voz_no;
        private System.Windows.Forms.RadioButton rbtn_voz_si;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtn_no_botones;
        private System.Windows.Forms.RadioButton rbtn_Si_botones;
    }
}