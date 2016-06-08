using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VRLabyrinth.Properties;


namespace VRLabyrinth
{
    public partial class Playground : Form
    {

        Dateileser datain;
        BufferedGraphics myBuffer;
        BufferedGraphicsContext currentContext;
        Timer timer1;
        public Playground()
        {
            InitializeComponent();
            zeichne();
            //zeichne_3D();
        }
                        
        Feld[,] FieldMap_2D;
        //Feld[, ,] FieldMap_3D;
        Object[] holder;
        Player spieler;

        private void zeichne()
        {            
            String Text;
            datain = new Dateileser();
            Size si = new Size(16, 16);
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            //timer1 = new Timer();
            //timer1.Interval = 30;
            ////timer1.AutoReset = true;
            //timer1.Tick += new EventHandler(this.OnTimer);

            //Player karte = new Player("M:\\VRSammel\\OcculusLabyrinth\\OcculusLabyrinth\\VRLabyrinth\\VRLabyrinth\\Resources\\hintergrund.bmp");
            Bitmap karte = new Bitmap(Resources.hintergrund);            
            //strMap = new Block(si, karte, 16, 16);

            Text = datain.leseein();
            String[] lines = Text.Split('\n');
            int numLines = lines.Length;
            int RowLengh = 0;
            foreach (string line in lines)
            {
                if (line.Length - 1 > RowLengh)
                    RowLengh = line.Length - 1;
            }
            
            //testbereich holder
            int i = 0;
            holder = new object[6];

            FieldMap_2D = new Feld[numLines, RowLengh];
            for (int y = 0; y < numLines; ++y)
            {
                int actualRowLengh = lines[y].Length - 1;

                for (int x = 0; x < RowLengh; ++x)
                {
                    //.#-kBZ
                    if (actualRowLengh <= x || lines[y][x] == '.')
                        FieldMap_2D[y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                    else if (lines[y][x] == '#')
                        FieldMap_2D[y, x] = new Block(si, karte, 50 + x * 16, 50 + y * 16);
                    else if (lines[y][x] == 'Z'){
                        FieldMap_2D[y, x] = new Target(si, karte, 50 + x * 16, 50 + y * 16);
                        anzTargets++;
                    }
                    else if (lines[y][x] == 'K')
                    {
                        FieldMap_2D[y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                        //FieldMap_2D[y, x] = new Kiste(si, karte, 50 + x * 16, 50 + y * 16);
                        holder[i++] = new Kiste(si, karte, 50 + x * 16, 50 + y * 16, x, y);
                    }
                    else if (lines[y][x] == 'B')
                    {
                        FieldMap_2D[y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                        spieler = new Player(x, y);
                        
                        //holder[i++] = new Kiste(si, karte, 50 + x * 16, 50 + y * 16, x, y);
                    }
                    else if (lines[y][x] == '-')
                        FieldMap_2D[y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                }
            }
            threadTargetStarter();
            //timer1.Start();
        }

        private void OnTimer(object sender, EventArgs e)
        {
            //myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            //foreach (Feld field in FieldMap_2D)
            //{
            //    myBuffer.Graphics.DrawImage(field.getBMap(), field.getStartPoint());
            //}

            //foreach (Kiste obj in holder)
            //{
            //    myBuffer.Graphics.DrawImage(obj.getBMap(), obj.getStartPoint());
            //}

            //myBuffer.Graphics.DrawImage(spieler.getBMap(), spieler.getActPoint());
            //myBuffer.Render();
        }

        #region Zeichne-3D
        /*private void zeichne_3D()
        {
            String Text;
            datain = new Dateileser();
            Size si = new Size(16, 16);

            Player karte = new Player("M:\\VRSammel\\OcculusLabyrinth\\OcculusLabyrinth\\VRLabyrinth\\VRLabyrinth\\Resources\\hintergrund.bmp");
            //strMap = new Block(si, karte, 16, 16);

            Text = datain.leseein();
            String[] lines = Text.Split('\n');
            int numLines = lines.Length;
            int RowLengh = 0;
            foreach (string line in lines)
            {
                if (line.Length - 1 > RowLengh)
                    RowLengh = line.Length - 1;
            }

            FieldMap_3D = new Feld[2,numLines, RowLengh];
            for (int y = 0; y < numLines; ++y)
            {
                int actualRowLengh = lines[y].Length - 1;

                for (int x = 0; x < RowLengh; ++x)
                {
                    //.#-kBZ
                    if (actualRowLengh <= x || lines[y][x] == '.' || lines[y][x] == 'B')
                        FieldMap_3D[0,y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                    else if (lines[y][x] == '#')
                        FieldMap_3D[0,y, x] = new Block(si, karte, 50 + x * 16, 50 + y * 16);
                    else if (lines[y][x] == 'Z')
                        FieldMap_3D[0,y, x] = new Target(si, karte, 50 + x * 16, 50 + y * 16);
                    else if (lines[y][x] == 'K')
                    {
                        FieldMap_3D[0,y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                        FieldMap_3D[1,y, x] = new Kiste(si, karte, 50 + x * 16, 50 + y * 16);
                    }
                    else if (lines[y][x] == '-')
                        FieldMap_3D[0,y, x] = new Background(si, karte, 50 + x * 16, 50 + y * 16);
                }
            }
        }*/
        #endregion

        protected override void OnPaint (PaintEventArgs e)
        {
            //base.OnPaint(e);

            
            foreach (Feld field in FieldMap_2D)
            {
                myBuffer.Graphics.DrawImage(field.getBMap(), field.getStartPoint());
            }

            foreach (Kiste obj in holder)
            {
                myBuffer.Graphics.DrawImage(obj.getBMap(), obj.getStartPoint());
            }

            myBuffer.Graphics.DrawImage(spieler.getBMap(), spieler.getActPoint());
            myBuffer.Render();            
        }

        private void playground_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.KeyValue);
            //Kiste kistHold;
            //kistHold = (Kiste)holder[0];
            switch (e.KeyCode)
            {
                case Keys.W:
                    //if (FieldMap_2D[spieler.yCoordinate - 1, spieler.xCoordinate].GetType().Name == "Kiste")
                    //    shift(FieldMap_2D[spieler.yCoordinate - 1, spieler.xCoordinate]
                    //        , FieldMap_2D[spieler.yCoordinate - 2, spieler.xCoordinate]
                    //        , "UP");

                    if (!hidding(FieldMap_2D[spieler.yCoordinate - 1, spieler.xCoordinate]) &&
                    Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), "UP"))
                        spieler.highAdding();
                    //kistHold.highAdding();
                    break;
                case Keys.S:
                    //if (FieldMap_2D[spieler.yCoordinate + 1, spieler.xCoordinate].GetType().Name == "Kiste")
                    //    shift(FieldMap_2D[spieler.yCoordinate + 1, spieler.xCoordinate]
                    //        , FieldMap_2D[spieler.yCoordinate + 2, spieler.xCoordinate]
                    //        , "DOWN");

                    if (!hidding(FieldMap_2D[spieler.yCoordinate + 1, spieler.xCoordinate]) &&
                    Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), "DOWN"))
                        spieler.downAdding();
                    //kistHold.downAdding();
                    break;
                case Keys.A:
                    //if (FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate - 1].GetType().Name == "Kiste")
                    //    shift(FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate - 1]
                    //        , FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate - 2]
                    //        , "LEFT");

                    if (!hidding(FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate - 1]) &&
                    Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), "LEFT"))
                        spieler.leftAdding();
                    //kistHold.leftAdding();
                    break;
                case Keys.D:
                    //if (FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate + 1].GetType().Name == "Kiste")
                    //    shift(FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate + 1]
                    //        , FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate + 2]
                    //        , "RIGHT");

                    if (!hidding(FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate + 1]) &&
                        Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), "RIGHT"))
                        spieler.rightAdding();
                    //kistHold.rightAdding();
                    break;
            }
            base.Refresh();
        }

        private bool shift(Kiste kiste, Feld feld2, string direction)
        {
            bool secondIsNotKiste = true;
            bool returnWert = false;
            Point targetPoint = kiste.getAlternatePoint();
            switch (direction)
            {
                case "UP":
                    targetPoint.Y -= 1;
                    break;
                case "DOWN":
                    targetPoint.Y += 1;
                    break;
                case "LEFT":
                    targetPoint.X -= 1;
                    break;
                case "RIGHT":
                    targetPoint.X += 1;
                    break;
                default:
                    throw new Exception("Direction Error");
            }
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

        private bool Kisthidding(Point PlayerPoint, string direction)
        {
            Point targetPoint = PlayerPoint;
            bool returnWert = true;
            int xP=0, yP=0;
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

        private Boolean hidding(object targetField)
        {
            Feld FieldCache = (Feld)targetField;
            if (FieldCache.block)
                return true;
            return false;
        }

        private void threadTargetStarter()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(targetErreicht));

            t.Name = "ZielPruefung";
            t.Start();
        }

        private int anzTargets = 0;
        
        private void targetErreicht()
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
                    MessageBox.Show("Gewonnen", "Winning", MessageBoxButtons.OK);
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
