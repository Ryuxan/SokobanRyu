using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRLabyrinth
{
    class Starter
    {
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Playground p = new Playground();
            //do anything
            p.BringToFront();
            p.Focus();
            Application.Run(p);            
        }
    }
}
