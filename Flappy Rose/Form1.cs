using Flappy_Rose.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Flappy_Rose
{
    public partial class Form1 : Form
    {
        public static int windowsWidth, windowsHeight;

        Image backgroundImage;

        Player player;

        public static List<BarrierPillar> barrierPillars;

        Timer updateTimer;

        bool keyDown;

        public static int score;

        public Form1()
        {
            InitializeComponent();

            Size = new Size(1000, 600);

            windowsWidth = Width;
            windowsHeight = Height - SystemInformation.ToolWindowCaptionHeight;

            backgroundImage = Image.FromFile("image\\background.jpg");
            backgroundImage = new Bitmap(backgroundImage, new Size(windowsWidth, windowsHeight));

            Setting();

            updateTimer = new Timer();
            updateTimer.Interval = 10;
            updateTimer.Tick += new EventHandler(Run);
            updateTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.DrawImage(backgroundImage, 0, 0);

            player.Show(e);

            foreach (var barrierPillar in barrierPillars.ToArray())
                barrierPillar.Show(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyDown)
            {
                keyDown = true;

                player.Flap();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyDown = false;
        }

        void Run(object sender, EventArgs e)
        {
            player.Flying();

            foreach (var barrierPillar in barrierPillars.ToArray())
                barrierPillar.Shift();

            if (IsGameOver())
                JumpGameOverDialog();

            Invalidate();
        }

        void Setting()
        {
            player = new Player();

            barrierPillars = new List<BarrierPillar>();

            int pillarsCount = 5;

            for (int i = 0; i < pillarsCount; i++)
                barrierPillars.Add(new BarrierPillar(windowsWidth + i * 500));

            keyDown = false;

            score = 0;
        }

        bool IsGameOver()
        {
            if (player.y > windowsHeight)
                return true;

            foreach (var objectAreaP in player.objectAreas)
                foreach (var barrier in barrierPillars[0].barriers)
                    foreach (var objectAreaB in barrier.objectAreas)
                        if (IsOverlap(objectAreaP, objectAreaB))
                            return true;
                            
            return false;
        }

        bool IsOverlap(ObjectArea a, ObjectArea b)
        {
            return a.x < b.x + b.w && a.x + a.w > b.x &&
                   a.y < b.y + b.h && a.y + a.h > b.y;
        }

        void JumpGameOverDialog()
        {
            updateTimer.Stop();

            DialogResult dialogResult = MessageBox.Show("分數：" + score + "\n\n再玩一次？",
                                                        "遊戲結束",
                                                        MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Setting();

                updateTimer.Start();
            }
            else if (dialogResult == DialogResult.No)
                Environment.Exit(0);
        }
    }
}