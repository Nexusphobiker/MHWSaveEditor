using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHWSaveEditor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("MHWSaveEditor by Nexusphobiker");
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
