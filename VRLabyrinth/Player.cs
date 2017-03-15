using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VRLabyrinth
{
    public class Player
    {
        private Bitmap bMap;
        public int xCoordinate { get; private set; }
        public int yCoordinate { get; private set; }
        private Point Postion;

        public Player()
        {
            initGraphic();
        }

        public Player(int xCoordinate, int yCoordinate)
            : this()
        {
            setStartPoint(xCoordinate, yCoordinate);
        }

        private void initGraphic()
        {
            bMap = new Bitmap(VRLabyrinth.Properties.Resources.player);
            //using (Graphics g = Graphics.FromImage(bMap))
            //{
            //    g.DrawImage(bm, dest, source, GraphicsUnit.Pixel);
            //}
        }

        public void setStartPoint(int xCoordinate, int yCoordinate)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            Postion = new Point(50 + xCoordinate * 16, 50 + yCoordinate * 16);
        }

        public Bitmap getBMap()
        {
            return bMap;            
        }

        public Point getActPoint()
        {
            return Postion;
        }

        public void highAdding()
        {
            //50 + x * 16, 50 + y * 16
            --yCoordinate;
            Postion.Offset(new Point(0, -16));
            //Postion = new Point(50 + xCoordinate * 16, 50 + (--yCoordinate) * 16);
        }

        public void downAdding()
        {
            //50 + x * 16, 50 + y * 16
            ++yCoordinate;
            Postion.Offset(new Point(0, 16));
            //Postion = new Point(50 + xCoordinate * 16, 50 + (++yCoordinate) * 16);
        }
        public void rightAdding()
        {
            //50 + x * 16, 50 + y * 16
            ++xCoordinate;
            Postion.Offset(new Point(16, 0));
            //Postion = new Point(50 + (++xCoordinate) * 16, 50 + yCoordinate * 16);
        }

        public void leftAdding()
        {
            //50 + x * 16, 50 + y * 16
            --xCoordinate;
            Postion.Offset(new Point(-16, 0));
            //Postion = new Point(50 + (--xCoordinate) * 16, 50 + yCoordinate * 16);
        }   
    }
}
