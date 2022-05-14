using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Rose.Class
{
    public class Barrier
    {
        public int x, y, size;

        Image image;

        public ObjectArea[] objectAreas;

        public Barrier(int x, int y)
        {
            this.x = x;
            this.y = y;

            size = 150;

            image = Image.FromFile("image\\barrier.png");
            image = new Bitmap(image, new Size(size, size));

            objectAreas = new ObjectArea[5];

            objectAreas[0] = new ObjectArea(x, y, 10, 50);
            objectAreas[1] = new ObjectArea(x + 140, y, 10, 50);
            objectAreas[2] = new ObjectArea(x + 10, y + 40, 130, 20);
            objectAreas[3] = new ObjectArea(x + 40, y + 60, 70, 50);
            objectAreas[4] = new ObjectArea(x + 55, y + 110, 40, 40);
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.DrawImage(image, x, y);

            //foreach (var barrierArea in objectAreas)
            //    barrierArea.Show(e); 
        }
    }
}