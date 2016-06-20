using System;
using System.Drawing;
//using System.Collections.Generic;
//using System.Text;
//using VRLabyrinth.Properties;

namespace VRLabyrinth
{

    //TODO Engine Attribute and Functions from PlayGround move 
    class Engine
    {
        Dateileser datain;
        int anzTargets = 0;
        Object[] holder;
        Feld[,] FieldMap_2D;

        public void targetErreicht()
        {
            bool zielnichterreicht = true;
            do
            {
                int goalsArrived = anzTargets;
                foreach (Kiste k in holder)
                {
                    Feld testield = FieldMap_2D[k.getAlternatePoint().Y, k.getAlternatePoint().X];
                    if (FieldMap_2D[k.getAlternatePoint().Y, k.getAlternatePoint().X].GetType().Name == "Target")
                        goalsArrived--;
                    //k.getAlternatePoint()
                }
                if (goalsArrived == 0)
                {
                    
                    zielnichterreicht = false;
                }
            }
            while (zielnichterreicht);
            //globale goals arrived
            //anfang bei 0 mit jedem target field +1
            //ist auf dem feld der kiste ein target feld setze globale -1
            //immer zur laufzeit überprüfen
            //wenn cahce wert 0 erreicht sind alle ziele mit kiste belegt dann fertig
            //im eigenen thread daher eigene classe?
        }
    }        
}
