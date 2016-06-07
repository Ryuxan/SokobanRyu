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
        
        //public void leseein()
        //{            
        //    String strMap;           

        //    strMap = File.ReadAllText("M:\\VRSammel\\OcculusLabyrinth\\OcculusLabyrinth\\VRLabyrinth\\VRLabyrinth\\strMap.txt");            
        //    //Console.WriteLine(strMap);
        //}

        public String leseein()
        {
            String strMap;

            strMap = File.ReadAllText("M:\\VRSammel\\OcculusLabyrinth\\OcculusLabyrinth\\VRLabyrinth\\VRLabyrinth\\test.txt");
            //strMap = File.ReadAllText(VRLabyrinth.Properties.Resources.test);

            return strMap;
            //Console.WriteLine(strMap);
        }

        public String leseein(string wert)
        {
            String strMap;

            //            strMap = File.ReadAllText("M:\\VRSammel\\OcculusLabyrinth\\OcculusLabyrinth\\VRLabyrinth\\VRLabyrinth\\strMap.txt");
            strMap = File.ReadAllText(VRLabyrinth.Properties.Resources.test);

            return strMap;
            //Console.WriteLine(strMap);
        }
    }
}
