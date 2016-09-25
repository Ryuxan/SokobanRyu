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
        /// Read a random Map file in the standard Mapsdirectory
        /// </summary>
        /// <returns>Map in String Form</returns>
        public String leseein()
        {
            String strMap;
            string path;
            //Later Random File in the Map Directory or Browse
            string RandomMap; // = "Map2.txt";
            string[] Maps;
            Random rnd = new Random(DateTime.Now.Millisecond);
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location) +"\\Maps";
            Maps = Directory.GetFiles(path, "Map*");

            RandomMap = Maps[rnd.Next(Maps.Length)];

            if (Directory.CreateDirectory(path).Exists)
            {
                //strMap = File.ReadAllText(path +"\\"+ RandomMap);
                strMap = File.ReadAllText(RandomMap);
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

            path = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location) + "\\Maps";

            strMap = File.ReadAllText(path + "\\" + FileName);
            return strMap;           
        }
    }
}
