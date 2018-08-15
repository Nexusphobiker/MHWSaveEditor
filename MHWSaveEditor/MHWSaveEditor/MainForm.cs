using Microsoft.Win32;
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

namespace MHWSaveEditor
{
    public partial class MainForm : Form
    {
        private IntPtr ConsoleHandle;
        public static ProgressBar progressBar;
        private MHWSave SaveFile;

        public MainForm()
        {
            InitializeComponent();
            //Set console as child
            ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            progressBar = progressBar_MainForm;
            Helper.SetParent(ConsoleHandle, consoleGroupBox.Handle);
            Helper.SetWindowLong(ConsoleHandle, Helper.WindowLongFlags.GWL_STYLE, 0x00080000);
            Helper.ShowWindow(ConsoleHandle, Helper.ShowWindowCommands.Maximize);
            this.ResizeEnd += MainForm_Resize;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Helper.SetParent(ConsoleHandle, consoleGroupBox.Handle);
            Helper.ShowWindow(ConsoleHandle, Helper.ShowWindowCommands.Maximize);
        }

        private void toolStripButton_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string steamPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "");
            if(steamPath != "")
            {
                steamPath = steamPath + "/userdata";
                Console.WriteLine("Found SteamPath " + steamPath);
                bool foundGamePath = false;
                foreach(var userdir in Directory.GetDirectories(steamPath))
                {
                    foreach(var gamedir in Directory.GetDirectories(userdir))
                    {
                        if (gamedir.Contains("582010"))
                        {
                            steamPath = (gamedir + "\\remote").Replace('/','\\');
                            Console.WriteLine("Found GameDir " + steamPath);
                            foundGamePath = true;
                            break;
                        }
                    }
                    if (foundGamePath)
                        break;
                }
            }
            openFileDialog.InitialDirectory = steamPath.Replace('/','\\');
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Console.WriteLine("Loading " + openFileDialog.FileName);
            SaveFile = new MHWSave(File.ReadAllBytes(openFileDialog.FileName));
            Console.WriteLine("Loaded " + SaveFile.FileSize + " bytes");

        }

        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            if (MessageBox.Show("Fix the checksum?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
                SaveFile.FixChecksum();
            if (MessageBox.Show("Encrypt the file?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
                SaveFile.Encrypt();
            SaveFile.Save(saveFileDialog.FileName);
        }

        private void toolStripButton_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MHW Save Editor by Nexusphobiker \nSource: https://github.com/Nexusphobiker", "About");
        }
    }
}
