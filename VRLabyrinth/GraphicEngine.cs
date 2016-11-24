using System;
using System.Collections.Generic;
using System.Drawing;
using VRLabyrinth.Properties;

namespace VRLabyrinth
{
    public static class GraphicEngine
    {
        public static Bitmap GraphicItems = new Bitmap(Resources.hintergrund);
        public static BufferedGraphics myBuffer;
        public static BufferedGraphicsContext currentContext;

        public static void initiateMap(string _sMap, ref Player _Spieler)
        {
            Size si = new Size(16, 16);
            String[] lines = _sMap.Split('\n');
            int numLines = lines.Length;
            int RowLengh = 0;
            List<Kiste> cacheKisteList = new List<Kiste>();

            foreach (string line in lines)
            {
                if (line.Length - 1 > RowLengh)
                    RowLengh = line.Length - 1;
            }

            Engine.FieldMap_2D = new Feld[numLines, RowLengh];

            for (int y = 0; y < numLines; ++y)
            {
                int actualRowLengh = lines[y].Length - 1;

                for (int x = 0; x < RowLengh; ++x)
                {
                    //.#-kBZ
                    if (actualRowLengh <= x || lines[y][x] == '.')
                    {
                        Engine.FieldMap_2D[y, x] = new Background(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16);
                    }
                    else if (lines[y][x] == '#')
                    {
                        Engine.FieldMap_2D[y, x] = new Block(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16);
                    }
                    else if (lines[y][x] == 'Z')
                    {
                        Engine.FieldMap_2D[y, x] = new Target(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16);
                        Engine.anzTargets++;
                    }
                    else if (lines[y][x] == 'K')
                    {
                        Engine.FieldMap_2D[y, x] = new Background(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16);
                        cacheKisteList.Add(new Kiste(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16, x, y));
                    }
                    else if (lines[y][x] == 'B')
                    {
                        Engine.FieldMap_2D[y, x] = new Background(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16);
                        _Spieler.setStartPoint(x, y);
                    }
                    else if (lines[y][x] == '-')
                    {
                        Engine.FieldMap_2D[y, x] = new Background(si, GraphicEngine.GraphicItems, 50 + x * 16, 50 + y * 16);
                    }
                }
            }
            Engine.holder = cacheKisteList.ToArray();
        }

        public static void paint(Playground _actPlayground)
        {
            try
            {
                myBuffer = currentContext.Allocate(_actPlayground.CreateGraphics(), _actPlayground.DisplayRectangle);
                foreach (Feld field in Engine.FieldMap_2D)
                {
                    myBuffer.Graphics.DrawImage(field.getBMap(), field.getStartPoint());
                }

                foreach (Kiste obj in Engine.holder)
                {
                    myBuffer.Graphics.DrawImage(obj.getBMap(), obj.getStartPoint());
                }

                myBuffer.Graphics.DrawImage(_actPlayground._spieler.getBMap(), _actPlayground._spieler.getActPoint());
                myBuffer.Render();
            }
            catch (NullReferenceException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                _actPlayground.timer1.Stop();
            }
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
    }
}
