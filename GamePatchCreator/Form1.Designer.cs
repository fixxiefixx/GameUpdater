namespace GamePatchCreator
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_oldPath = new System.Windows.Forms.TextBox();
            this.textBox_newPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_outputFile = new System.Windows.Forms.TextBox();
            this.button_createPatch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Old Version Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "New Version Path:";
            // 
            // textBox_oldPath
            // 
            this.textBox_oldPath.Location = new System.Drawing.Point(108, 10);
            this.textBox_oldPath.Name = "textBox_oldPath";
            this.textBox_oldPath.Size = new System.Drawing.Size(534, 20);
            this.textBox_oldPath.TabIndex = 1;
            // 
            // textBox_newPath
            // 
            this.textBox_newPath.Location = new System.Drawing.Point(108, 39);
            this.textBox_newPath.Name = "textBox_newPath";
            this.textBox_newPath.Size = new System.Drawing.Size(534, 20);
            this.textBox_newPath.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Output file:";
            // 
            // textBox_outputFile
            // 
            this.textBox_outputFile.Location = new System.Drawing.Point(108, 71);
            this.textBox_outputFile.Name = "textBox_outputFile";
            this.textBox_outputFile.Size = new System.Drawing.Size(534, 20);
            this.textBox_outputFile.TabIndex = 3;
            // 
            // button_createPatch
            // 
            this.button_createPatch.Location = new System.Drawing.Point(528, 97);
            this.button_createPatch.Name = "button_createPatch";
            this.button_createPatch.Size = new System.Drawing.Size(114, 31);
            this.button_createPatch.TabIndex = 4;
            this.button_createPatch.Text = "Create Patch";
            this.button_createPatch.UseVisualStyleBackColor = true;
            this.button_createPatch.Click += new System.EventHandler(this.button_createPatch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 140);
            this.Controls.Add(this.button_createPatch);
            this.Controls.Add(this.textBox_outputFile);
            this.Controls.Add(this.textBox_newPath);
            this.Controls.Add(this.textBox_oldPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Game Patch Creator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_oldPath;
        private System.Windows.Forms.TextBox textBox_newPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_outputFile;
        private System.Windows.Forms.Button button_createPatch;
    }
}

