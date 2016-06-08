using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VRLabyrinth
{
    class Dateileser
    {
        public Dateileser()
        {
            //leseein();
        }
                
        /// <summary>
        /// Read a Random File in the Standart Maps Directory
        /// </summary>
        /// <returns>Map in String Form</returns>
        public String leseein()
        {
            String strMap;
            string path;
            //Later Random File in the Map Directory or Browse
            string RandomMap = "test.txt";
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(base.GetType()).Location) +"\\Maps";

            if (Directory.CreateDirectory(path).Exists)
            {
                strMap = File.ReadAllText(path +"\\"+ RandomMap);
                return strMap;
            }            
            return null;
        }

        /// <summary>
        /// Read the Specified File in the Standart Maps Directory
        /// </summary>
        /// <param name="FileName">only FileName </param>
        /// <returns>Map in String Form</returns>
        public String leseein(string FileName)
        {
            String strMap;
            String path;
                        
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(base.GetType()).Location) + "\\Maps";

            strMap = File.ReadAllText(path + "\\" + FileName);
            return strMap;           
        }
    }
}
