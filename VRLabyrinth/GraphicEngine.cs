using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
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
            Font drawFont = new Font("Arial", 13);
            SolidBrush drawBrush = new SolidBrush(Color.Red);

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

                //moves and shifts
                myBuffer.Graphics.DrawString("Moves " + Engine.moves, drawFont, drawBrush, new PointF(5, 30));
                myBuffer.Graphics.DrawString("Shifts " + Engine.shifts, drawFont, drawBrush, new PointF(150, 30));

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

    /// <span class="code-SummaryComment"><summary></span>
/// Selected Win API Function Calls
/// <span class="code-SummaryComment"></summary></span>

public class WinApi
{
    [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
    public static extern int GetSystemMetrics(int which);

    [DllImport("user32.dll")]
    public static extern void
        SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                     int X, int Y, int width, int height, uint flags);        

    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;
    private static IntPtr HWND_TOP = IntPtr.Zero;
    private const int SWP_SHOWWINDOW = 64; // 0×0040

    public static int ScreenX
    {
        get { return GetSystemMetrics(SM_CXSCREEN);}
    }

    public static int ScreenY
    {
        get { return GetSystemMetrics(SM_CYSCREEN);}
    }

    public static void SetWinFullScreen(IntPtr hwnd)
    {
        SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
    }
}

/// <span class="code-SummaryComment"><summary></span>
/// Class used to preserve / restore / maximize state of the form
/// <span class="code-SummaryComment"></summary></span>
public class FormState
{
    private System.Windows.Forms.FormWindowState winState;
    private System.Windows.Forms.FormBorderStyle brdStyle;
    private bool topMost;
    private Rectangle bounds;

    private bool IsMaximized = false;

    public void Maximize(System.Windows.Forms.Form targetForm)
    {
        if (!IsMaximized)
        {
            IsMaximized = true;
            Save(targetForm);
            targetForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            targetForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            targetForm.TopMost = true;
            WinApi.SetWinFullScreen(targetForm.Handle);
        }
    }

    public void Save(System.Windows.Forms.Form targetForm)
    {
        winState = targetForm.WindowState;
        brdStyle = targetForm.FormBorderStyle;
        topMost = targetForm.TopMost;
        bounds = targetForm.Bounds;
    }

    public void Restore(System.Windows.Forms.Form targetForm)
    {
        targetForm.WindowState = winState;
        targetForm.FormBorderStyle = brdStyle;
        targetForm.TopMost = topMost;
        targetForm.Bounds = bounds;
        IsMaximized = false;
    }
}
}
