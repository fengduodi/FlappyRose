using System;
using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Rose.Class
{
    class Player
    {
        public int x, y;

        Image image;

        public ObjectArea[] objectAreas;

        bool rise;

        Timer stopRiseTimer;

        public Player()
        {
            x = 200;
            y = 200;

            image = Image.FromFile("image\\player.png");
            image = new Bitmap(image, new Size(150, 100));

            objectAreas = new ObjectArea[5];

            objectAreas[0] = new ObjectArea(x + 10, y + 20, 20, 50);
            objectAreas[1] = new ObjectArea(x + 120, y + 20, 20, 50);
            objectAreas[2] = new ObjectArea(x + 30, y + 40, 30, 40);
            objectAreas[3] = new ObjectArea(x + 90, y + 40, 30, 40);
            objectAreas[4] = new ObjectArea(x + 60, y + 60, 30, 35);

            rise = false;

            stopRiseTimer = new Timer();
            stopRiseTimer.Interval = 100;
            stopRiseTimer.Tick += new EventHandler(StopRise);
            stopRiseTimer.Start();
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.DrawImage(image, x, y);

            //foreach (var objectArea in objectAreas)
            //    objectArea.Show(e);
        }

        public void Flying()
        {
            if (rise)
                Rise();
            else
                Fall();
        }

        void Fall()
        {
            int fallSpeed = 5;

            y += fallSpeed;

            foreach (var objectArea in objectAreas)
                objectArea.y += fallSpeed;
        }

        void Rise()
        {
            int riseSpeed = 10;

            y -= riseSpeed;

            foreach (var objectArea in objectAreas)
                objectArea.y -= riseSpeed;
        }

        public void Flap()
        {
            rise = true;

            stopRiseTimer.Start();
        }

        void StopRise(object sender, EventArgs e)
        {
            rise = false;

            stopRiseTimer.Stop();
        }
    }
}