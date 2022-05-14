using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Rose.Class
{
    public class ObjectArea
    {
        public int x, y, w, h;

        public ObjectArea(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public void Show(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.DrawRectangle(new Pen(Color.Lime), x, y, w, h);
        }
    }
}