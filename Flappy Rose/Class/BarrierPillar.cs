using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Flappy_Rose.Class
{
    public class BarrierPillar
    {
        public int x;

        public List<Barrier> barriers;

        public BarrierPillar(int x)
        {
            this.x = x;

            barriers = new List<Barrier>();

            BuildPillar(x);
        }

        public void Show(PaintEventArgs e)
        {
            foreach (var barrier in barriers)
                barrier.Show(e);
        }

        public void Shift()
        {
            int speed = 7;

            x -= speed;

            foreach (var barrier in barriers)
            {
                barrier.x -= speed;

                foreach (var objectArea in barrier.objectAreas)
                    objectArea.x -= speed;
            }

            if (x < -barriers[0].size)
            {
                List<BarrierPillar> barrierPillars = Form1.barrierPillars;

                barrierPillars.Remove(this);
                barrierPillars.Add(new BarrierPillar(barrierPillars[barrierPillars.Count - 1].x + 500));

                Form1.score++;
            }
        }

        void BuildPillar(int x)
        {
            Random random = new Random();

            int count = random.Next(1, 6);

            int startPosition = -75;

            int space = 300;

            for (int i = 0; i < count; i++)
                barriers.Add(new Barrier(x, startPosition + i * 50));

            for (int i = 0; i < 10 - count; i++)
                barriers.Add(new Barrier(x, startPosition + count * 50 + space + i * 50));
        }
    }
}