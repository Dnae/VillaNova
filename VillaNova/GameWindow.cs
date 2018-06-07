using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VillaNova
{
    public partial class GameWindow : Form
    {

        VillaNova vn;
        GamePanel gamepanel;
        int dirx, diry;

        public GameWindow(VillaNova vn)
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(this.GameWindow_KeyDown);
            this.KeyUp += new KeyEventHandler(this.GameWindow_KeyUp);
            this.vn = vn;

            gamepanel = new GamePanel()
            {
                Size = this.Size,
                Location = this.Location,
                BackColor = Color.Black
            };

            this.Controls.Add(gamepanel);
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = (25); // 10 secs
            timer.Tick += new EventHandler(RepaintGamePanel);
            timer.Start();
        }

        private void RepaintGamePanel(Object o, EventArgs e)
        {
            vn.MovePlayer(dirx, diry);
            gamepanel.SetDisplay(vn.GetImage());
            gamepanel.Refresh();
        }

        void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    diry = -2;
                    break;
                case Keys.A:
                    dirx = -2;
                    break;
                case Keys.S:
                    diry = 2;
                    break;
                case Keys.D:
                    dirx = 2;
                    break;
            }
        }

        void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    diry = 0;
                    break;
                case Keys.A:
                    dirx = 0;
                    break;
                case Keys.S:
                    diry = 0;
                    break;
                case Keys.D:
                    dirx = 0;
                    break;
            }
        }

        private class GamePanel : Panel
        {
            Bitmap display= new Bitmap(1, 1);

            public GamePanel()
            {
                this.DoubleBuffered = true;
            }

            public void SetDisplay(Bitmap display)
            {
                this.display = display;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                // Call the OnPaint method of the base class.  
                base.OnPaint(e);
                // Call methods of the System.Drawing.Graphics object.  
                e.Graphics.DrawImage(display, 0, 0, Width, Height);
            }
        }
    }
}
