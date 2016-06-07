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
            //do anything
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new playground());            
        }
    }
}
