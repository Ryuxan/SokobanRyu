using System;
using System.Drawing;
//using System.Collections.Generic;
//using System.Text;
//using VRLabyrinth.Properties;

namespace VRLabyrinth
{
    public enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    };

    public static class Engine
    {
        //Dateileser datain;
        public static int anzTargets = 0;
        public static Object[] holder;
        public static Feld[,] FieldMap_2D;
        private static Playground Playground;
        private static Player Spieler;
        private static System.Threading.Thread threadTargetTest;
        //private Dateileser datain;

        public static int moves = 0;
        public static int shifts = 0;

        //public Engine(ref object[] holder, ref Feld[,] FieldMap_2D)
        //{
        //    this.holder = holder;
        //    this.FieldMap_2D = FieldMap_2D;
        //}

        public static void setPlayground(Playground _playground)
        {
            Playground = _playground;
        }

        public static void setPlayer(ref Player _spieler)
        {
            Spieler = _spieler;
        }

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
                    if (testield.GetType().Name == "Target")
                        goalsArrived--;
                    //k.getAlternatePoint()
                }
                if (goalsArrived == 0)
                {
                    zielnichterreicht = false;
                }
                System.Threading.Thread.Sleep(100);
            }
            while (zielnichterreicht);
            if (Playground != null)
                End();
        }

        public static Boolean hidding(object targetField)
        {
            Feld FieldCache = (Feld)targetField;
            if (FieldCache.block)
                return true;
            return false;
        }

        public static bool Kisthidding(Point _PlayerPoint, Direction _direction)
        {
            Point targetPoint = AltPoint(_PlayerPoint, _direction);
            bool returnWert = true;
            //hold the Field behind the target Point vor test can the chest shift
            Point behindTargetPoint = new Point(0, 0);
            behindTargetPoint = AltPoint(behindTargetPoint, _direction);

            foreach (Kiste k in holder)
            {
                if (targetPoint.Equals(k.getAlternatePoint()))
                {
                    returnWert = shift(k, FieldMap_2D[targetPoint.Y + behindTargetPoint.Y, targetPoint.X + behindTargetPoint.X], _direction);
                }
            }
            return returnWert;
        }

        private static Point AltPoint(Point _targetPoint, Direction _direction)
        {
            switch (_direction)
            {
                case Direction.Up:
                    _targetPoint.Y -= 1;
                    break;
                case Direction.Down:
                    _targetPoint.Y += 1;
                    break;
                case Direction.Left:
                    _targetPoint.X -= 1;
                    break;
                case Direction.Right:
                    _targetPoint.X += 1;
                    break;
                default:
                    throw new Exception("Direction Error");
            }
            return _targetPoint;
        }

        public static bool shift(Kiste kiste, Feld feld2, Direction direction)
        {
            bool secondIsNotKiste = true;
            bool returnWert = false;
            Point targetPoint = AltPoint(kiste.getAlternatePoint(), direction);
            //Point test = Point.Add(kiste.getAlternatePoint(), new Size(0, -1));


            foreach (Kiste k in holder)
            {
                if (kiste.getAlternatePoint().Equals(k.getAlternatePoint()))
                {
                    continue;
                }
                if (targetPoint.Equals(k.getAlternatePoint()))
                {
                    secondIsNotKiste = false;
                }
            }

            if (!hidding(feld2) && secondIsNotKiste)
            {
                switch (direction)
                {
                    case Direction.Up:
                        kiste.highAdding();
                        returnWert = true;
                        break;
                    case Direction.Down:
                        kiste.downAdding();
                        returnWert = true;
                        break;
                    case Direction.Left:
                        kiste.leftAdding();
                        returnWert = true;
                        break;
                    case Direction.Right:
                        kiste.rightAdding();
                        returnWert = true;
                        break;
                    default:
                        throw new Exception("Direction Error");
                }
                shifts++;
            }
            return returnWert;
        }

        public static void threadTargetStarter(out System.Threading.Thread threadTargetTest)
        {
            threadTargetTest = new System.Threading.Thread(new System.Threading.ThreadStart(targetErreicht));
            threadTargetTest.Name = "ZielPruefung";
            threadTargetTest.Start();
        }

        private static void threadTargetStarter()
        {
            threadTargetTest = new System.Threading.Thread(new System.Threading.ThreadStart(targetErreicht));
            threadTargetTest.Name = "ZielPruefung";
            threadTargetTest.Start();
        }

        public static void KeyboardInput(System.Windows.Forms.Keys _key)
        {
            Player spieler = Playground._spieler;
            switch (_key)
            {
                case System.Windows.Forms.Keys.W:
                    if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate - 1, spieler.xCoordinate]) &&
                    Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Up))
                        spieler.highAdding();
                    moves++;
                    break;
                case System.Windows.Forms.Keys.S:
                    if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate + 1, spieler.xCoordinate]) &&
                    Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Down))
                        spieler.downAdding();
                    moves++;
                    break;
                case System.Windows.Forms.Keys.A:
                    if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate - 1]) &&
                    Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Left))
                        spieler.leftAdding();
                    moves++;
                    break;
                case System.Windows.Forms.Keys.D:
                    if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate + 1]) &&
                        Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Right))
                        spieler.rightAdding();
                    moves++;
                    break;
                //case System.Windows.Forms.Keys.F5:
                //    Restart();
                //    break;
            }
        }

        public static void Restart()
        {
            Reset();
            Start();
        }

        public static void Start()
        {
            String Text;

            moves = 0;
            shifts = 0;
            Playground.RegisterKeyType();
            Dateileser datain = new Dateileser();
            GraphicEngine.currentContext = BufferedGraphicsManager.Current;
            GraphicEngine.myBuffer = GraphicEngine.currentContext.Allocate(Playground.CreateGraphics(), Playground.DisplayRectangle);

            Text = datain.leseein();
            //GraphicEngine.initiateMap(Text, ref Playground._spieler);
            GraphicEngine.initiateMap(Text, ref Spieler);

            Engine.threadTargetStarter();
            Playground.timer1.Start();
        }

        private static void Reset()
        {
            threadTargetTest.Abort();
            Playground.timer1.Stop();
            Playground.UnRegisterKeyType();
            Engine.anzTargets = 0;
            Engine.FieldMap_2D = null;
        }

        public static void End()
        {
            Playground.timer1.Stop();
            Playground.UnRegisterKeyType();
            Engine.anzTargets = 0;
            Engine.FieldMap_2D = null;
            System.Windows.Forms.MessageBox.Show("Winning");
        }
    }
}
