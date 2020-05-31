using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamePatchCreator
{
    public partial class Form1 : Form
    {
        private void ReadSettings()
        {
            textBox_oldPath.Text = Properties.Settings.Default.OldVersionPath;
            textBox_newPath.Text = Properties.Settings.Default.NewVersionPath;
            textBox_outputFile.Text = Properties.Settings.Default.OutputFile;
        }

        private void WriteSettings()
        {
            Properties.Settings.Default.OldVersionPath = textBox_oldPath.Text;
            Properties.Settings.Default.NewVersionPath = textBox_newPath.Text;
            Properties.Settings.Default.OutputFile = textBox_outputFile.Text;
            Properties.Settings.Default.Save();
        }

        public Form1()
        {
            InitializeComponent();
            ReadSettings();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteSettings();
        }

        private void button_createPatch_Click(object sender, EventArgs e)
        {
            button_createPatch.Enabled = false;

            PatchCreator pc = new PatchCreator();
            pc.CreatePatch(textBox_oldPath.Text, textBox_newPath.Text, textBox_outputFile.Text);

            button_createPatch.Enabled = true;
        }
    }
}
