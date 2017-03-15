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
        public Timer timer1;
        public Player _spieler;

        public Playground()
        {
            InitializeComponent();
            _spieler = new Player();
            Engine.setPlayer(ref _spieler);
            timer1 = new Timer();
            timer1.Interval = 1000 / 60;
            timer1.Tick += new EventHandler(this.OnTimer);
            //Engine.Start();
            //zeichne_3D();
        }

        public void RegisterKeyType()
        {
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.playground_KeyDown);
        }

        public void UnRegisterKeyType()
        {
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.playground_KeyDown);
        }

        //public void End()
        //{
        //    timer1.Stop();
        //    DeRegisterKeyType();
        //    Engine.anzTargets = 0;
        //    Engine.FieldMap_2D = null;
        //    datain = null;
        //    MessageBox.Show("Winning");
        //}

        //private void Reset()
        //{
        //    threadTargetTest.Abort();
        //    timer1.Stop();
        //    DeRegisterKeyType();
        //    Engine.anzTargets = 0;
        //    Engine.FieldMap_2D = null;
        //    datain = null;
        //}

        //public void Start()
        //{
        //    String Text;
        //    RegisterKeyType();
        //    datain = new Dateileser();            
        //    currentContext = BufferedGraphicsManager.Current;
        //    myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);            

        //    Text = datain.leseein();
        //    GraphicEngine.initiateMap(Text, ref _spieler);        
        //    Engine.threadTargetStarter(out threadTargetTest);
        //    timer1.Start();
        //}

        private void OnTimer(object sender, EventArgs e)
        {
            GraphicEngine.paint(this);
            //GraphicEngine.paint(panel1);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);

        //    foreach (Feld field in Engine.FieldMap_2D)
        //    {
        //        myBuffer.Graphics.DrawImage(field.getBMap(), field.getStartPoint());
        //    }

        //    foreach (Kiste obj in Engine.holder)
        //    {
        //        myBuffer.Graphics.DrawImage(obj.getBMap(), obj.getStartPoint());
        //    }

        //    myBuffer.Graphics.DrawImage(_spieler.getBMap(), _spieler.getActPoint());
        //    //myBuffer.Render();
        //}

        private void playground_KeyDown(object sender, KeyEventArgs e)
        {
            #region oldKeyCheck
            //switch (e.KeyCode)
            //{
            //    case Keys.W:
            //        if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate - 1, spieler.xCoordinate]) &&
            //        Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Up))
            //            spieler.highAdding();
            //        break;
            //    case Keys.S:
            //        if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate + 1, spieler.xCoordinate]) &&
            //        Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Down))
            //            spieler.downAdding();
            //        break;
            //    case Keys.A:
            //        if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate - 1]) &&
            //        Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Left))
            //            spieler.leftAdding();
            //        break;
            //    case Keys.D:
            //        if (!Engine.hidding(Engine.FieldMap_2D[spieler.yCoordinate, spieler.xCoordinate + 1]) &&
            //            Engine.Kisthidding(new Point(spieler.xCoordinate, spieler.yCoordinate), Direction.Right))
            //            spieler.rightAdding();
            //        break;
            //    case Keys.F5:
            //        Restart();
            //        break;
            //}
            #endregion
            System.Diagnostics.Debug.WriteLine("KeyDown: " + e.KeyCode);
            Engine.KeyboardInput(e.KeyCode);
            //base.Refresh();
        }

        private void Playground_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit()
            Environment.Exit(0);
        }

        private void herToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString().Equals("Restart"))
            {
                Engine.Restart();
            }

            if (sender.ToString().Equals("Close"))
            {
                Engine.End();
                this.Close();
            }
        }
    }
}
