using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUpdater
{
    public partial class Form1 : Form
    {

        private void Update()
        {
            progressBar_loading.Value = 50;
            Thread updateThread = new Thread(() => {
                Updater upd = new Updater();
                bool weiter = true;
                while (weiter)
                {
                    string errorText = "";
                    try
                    {
                        upd.Update();
                    }
                    catch (Exception ex)
                    {
                        errorText = ex.Message;

                    }
                    Invoke(new Action(() =>
                    {

                        if (errorText == "")
                        {
                            progressBar_loading.Value = 100;
                            button_play.Text = "Play";
                            button_play.Enabled = true;
                            weiter = false;
                        }
                        else
                        {
                            progressBar_loading.Value = 100;
                            button_play.Text = "Error";
                            label_status.Text = "Error";
                            if(MessageBox.Show(errorText+"\nTry again?", "Update Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error)==DialogResult.No)
                            {
                                weiter = false;
                                Application.Exit();
                            }
                            else
                            {
                                progressBar_loading.Value = 50;
                            }

                        }

                    }));
                }
            });
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Settings.StartFile));
            process.StartInfo = psi;
            process.Start();
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Update();
        }
    }
}
