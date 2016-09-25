using System;
using System.Drawing;
//using System.Collections.Generic;
//using System.Text;
//using VRLabyrinth.Properties;

namespace VRLabyrinth
{

    //TODO Engine Attribute and Functions from PlayGround move 
    public static class Engine
    {
        //Dateileser datain;
        public static int anzTargets = 0;
        public static Object[] holder;
        public static Feld[,] FieldMap_2D;
        public static Playground playground;
        

        //public Engine(ref object[] holder, ref Feld[,] FieldMap_2D)
        //{
        //    this.holder = holder;
        //    this.FieldMap_2D = FieldMap_2D;
        //}

        public static void targetErreicht()
        {
            System.Diagnostics.Debug.WriteLine("Start new Target Thread");
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
            if(playground != null)
                playground.End();
            //globale goals arrived
            //anfang bei 0 mit jedem target field +1
            //ist auf dem feld der kiste ein target feld setze globale -1
            //immer zur laufzeit überprüfen
            //wenn cahce wert 0 erreicht sind alle ziele mit kiste belegt dann fertig
            //im eigenen thread daher eigene classe?
        }

        public static Boolean hidding(object targetField)
        {
            Feld FieldCache = (Feld)targetField;
            if (FieldCache.block)
                return true;
            return false;
        }

        public static bool Kisthidding(Point PlayerPoint, string direction)
        {
            Point targetPoint = PlayerPoint;
            bool returnWert = true;
            //hold the Field behind the target Point vor test can the chest shift
            int xP = 0, yP = 0;

            switch (direction)
            {
                case "UP":
                    targetPoint.Y -= 1;
                    yP = -1;
                    break;
                case "DOWN":
                    targetPoint.Y += 1;
                    yP = 1;
                    break;
                case "LEFT":
                    targetPoint.X -= 1;
                    xP = -1;
                    break;
                case "RIGHT":
                    targetPoint.X += 1;
                    xP = 1;
                    break;
                default:
                    throw new Exception("Direction Error");
            }
            //vor jeder bewegung abfragen ob ich gegen kiste stoße wenn ja hidding abfragen und wenn keine dann kiste bewegen
            foreach (Kiste k in holder)
            {
                if (targetPoint.Equals(k.getAlternatePoint()))
                {
                    returnWert = shift(k, FieldMap_2D[targetPoint.Y + yP, targetPoint.X + xP], direction);
                }
            }
            return returnWert;
        }

        public static bool shift(Kiste kiste, Feld feld2, string direction)
        {
            bool secondIsNotKiste = true;
            bool returnWert = false;
            Point targetPoint = feld2.getAlternatePoint();
          
            foreach (Kiste k in holder)
            {
                if (targetPoint.Equals(k.getAlternatePoint()))
                {
                    secondIsNotKiste = false;
                }
            }

            if (!hidding(feld2) && secondIsNotKiste)
            {
                switch (direction)
                {
                    case "UP":
                        kiste.highAdding();
                        returnWert = true;
                        break;
                    case "DOWN":
                        kiste.downAdding();
                        returnWert = true;
                        break;
                    case "LEFT":
                        kiste.leftAdding();
                        returnWert = true;
                        break;
                    case "RIGHT":
                        kiste.rightAdding();
                        returnWert = true;
                        break;
                    default:
                        throw new Exception("Direction Error");
                }
            }
            return returnWert;
        }

        //public static void threadTargetStarter()
        //{
        //    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(targetErreicht));

        //    t.Name = "ZielPruefung";
        //    t.Start();
        //}
    }        
}
