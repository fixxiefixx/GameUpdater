namespace GameUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_status = new System.Windows.Forms.Label();
            this.button_play = new System.Windows.Forms.Button();
            this.progressBar_loading = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox_logo.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(800, 377);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label_status);
            this.panel1.Controls.Add(this.button_play);
            this.panel1.Controls.Add(this.progressBar_loading);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 377);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 73);
            this.panel1.TabIndex = 1;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_status.Location = new System.Drawing.Point(13, 19);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(148, 16);
            this.label_status.TabIndex = 2;
            this.label_status.Text = "Checking for updates";
            // 
            // button_play
            // 
            this.button_play.Enabled = false;
            this.button_play.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_play.Location = new System.Drawing.Point(679, 6);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(109, 55);
            this.button_play.TabIndex = 1;
            this.button_play.Text = "Please wait";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.button_play_Click);
            // 
            // progressBar_loading
            // 
            this.progressBar_loading.Location = new System.Drawing.Point(12, 38);
            this.progressBar_loading.Name = "progressBar_loading";
            this.progressBar_loading.Size = new System.Drawing.Size(661, 23);
            this.progressBar_loading.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "GameUpdater";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button button_play;
        private System.Windows.Forms.ProgressBar progressBar_loading;
    }
}

