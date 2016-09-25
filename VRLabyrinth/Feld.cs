using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VRLabyrinth
{
    public abstract class Feld
    {        
        protected Size si;
        protected Bitmap bmap;
        protected bool _block;
        public bool block {
            get { return _block; }           
        }
        protected RectangleF source = new RectangleF(0, 0, 16, 16);
        protected RectangleF dest = new RectangleF(0, 0, 16, 16);
        protected Point start;

        public Feld(Size si)
        {
            this.si = si;            
        }

        public Feld(Size si, Bitmap bm, int xstart, int ystart) : this(si)
        {
            start = new Point(xstart, ystart);          
        }

        public Feld(Size si, Bitmap bm, Point punkt) : this(si)
        {
            start = punkt;
        }

        public abstract Point getStartPoint();
        public abstract Point getAlternatePoint();        

        public abstract Bitmap getBMap();  

        protected abstract void initGraphic(Bitmap bm);
    }

    public class Block : Feld
    {        
        public Block(Size si, Bitmap bm, int xstart, int ystart) : base(si)
        {
            start = new Point(xstart, ystart);
            this._block = true;
            initGraphic(bm);
        }

        public Block(Size si, Bitmap bm, Point punkt) : base(si)
        {
            start = punkt;
            this._block = true;
            initGraphic(bm);
        }

        public override Point getStartPoint()
        {
            return start;
        }

        public override Point getAlternatePoint()
        {
            return new Point(-1, -1);
        }

        public override Bitmap getBMap()
        {
            return bmap;
        }

        protected override void initGraphic(Bitmap bm)
        {
            bmap = new Bitmap(si.Width, si.Height);
            source.X += 16;
            using (Graphics g = Graphics.FromImage(bmap))
            {
                g.DrawImage(bm, dest, source, GraphicsUnit.Pixel);
            }
        }
    }

    public class Background : Feld
    {
        public Background(Size si, Bitmap bm, int xstart, int ystart) : base(si)
        {
            start = new Point(xstart, ystart);
            this._block = false;
            initGraphic(bm);
        }

        public Background(Size si, Bitmap bm, Point punkt) : base(si)
        {
            start = punkt;
            this._block = false;
            initGraphic(bm);
        }

        public override Point getStartPoint()
        {
            return start;
        }

        public override Point getAlternatePoint()
        {
            return new Point(-1, -1);
        }

        public override Bitmap getBMap()
        {
            return bmap;
        }

        protected override void initGraphic(Bitmap bm)
        {
            bmap = new Bitmap(si.Width, si.Height);            
            using (Graphics g = Graphics.FromImage(bmap))
            {
                g.DrawImage(bm, dest, source, GraphicsUnit.Pixel);
            }
        }
    }

    public class Target : Feld
    {
        public Target(Size si, Bitmap bm, int xstart, int ystart): base(si)
        {
            start = new Point(xstart, ystart);
            this._block = false;
            initGraphic(bm);
        }

        public Target(Size si, Bitmap bm, Point punkt): base(si)
        {
            start = punkt;
            this._block = false;
            initGraphic(bm);
        }

        public override Point getStartPoint()
        {
            return start;
        }

        public override Point getAlternatePoint()
        {
            return new Point(-1, -1);
        }

        public override Bitmap getBMap()
        {
            return bmap;
        }

        protected override void initGraphic(Bitmap bm)
        {
            source.X += 16 * 2;
            bmap = new Bitmap(si.Width, si.Height);
            using (Graphics g = Graphics.FromImage(bmap))
            {
                g.DrawImage(bm, dest, source, GraphicsUnit.Pixel);
            }
        }
    }

    public class Kiste : Feld
    {

        int arrayY, arrayX;

        public Kiste(Size si, Bitmap bm, int xstart, int ystart)
            : base(si)
        {
            start = new Point(xstart, ystart);
            this._block = true;
            initGraphic(bm);
        }

        public Kiste(Size si, Bitmap bm, int xstart, int ystart, int arrayX, int arrayY)
            : base(si)
        {
            this.arrayX = arrayX;
            this.arrayY = arrayY;            
            start = new Point(xstart, ystart);
            this._block = true;
            initGraphic(bm);
        }

        public Kiste(Size si, Bitmap bm, Point punkt)
            : base(si)
        {
            start = punkt;
            initGraphic(bm);
        }

        public void highAdding()
        {
            //50 + x * 16, 50 + y * 16
            --arrayY;
            start.Offset(new Point(0, -16));
            //Postion = new Point(50 + xCoordinate * 16, 50 + (--yCoordinate) * 16);
        }

        public void downAdding()
        {
            //50 + x * 16, 50 + y * 16
            ++arrayY;
            start.Offset(new Point(0, 16));
            //Postion = new Point(50 + xCoordinate * 16, 50 + (++yCoordinate) * 16);
        }
        public void rightAdding()
        {
            //50 + x * 16, 50 + y * 16
            ++arrayX;
            start.Offset(new Point(16, 0));
            //Postion = new Point(50 + (++xCoordinate) * 16, 50 + yCoordinate * 16);
        }

        public void leftAdding()
        {
            //50 + x * 16, 50 + y * 16
            --arrayX;
            start.Offset(new Point(-16, 0));
            //Postion = new Point(50 + (--xCoordinate) * 16, 50 + yCoordinate * 16);
        }   

        public override Point getStartPoint()
        {
            return start;
        }

        public override Point getAlternatePoint()
        {
            return new Point(arrayX, arrayY);
        }

        public override Bitmap getBMap()
        {
            return bmap;
        }

        protected override void initGraphic(Bitmap bm)
        {
            source.X += 16 * 3;
            bmap = new Bitmap(si.Width, si.Height);
            using (Graphics g = Graphics.FromImage(bmap))
            {
                g.DrawImage(bm, dest, source, GraphicsUnit.Pixel);
            }
        }
    }
}
