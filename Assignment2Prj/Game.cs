using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment2Prj
{
    public partial class Game : Form
    {
        private readonly Timer _timer;
        private readonly Random _rng = new Random();

        private GameManager _manager;
        
        public Game()
        {
            InitializeComponent();

            Cursor.Hide();

            ClientSize = new Size(800, 600);
            _manager = new GameManager(ClientSize);

            _timer = new Timer { Interval = 1000 / 100 };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _manager.Update();

            Invalidate();
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            _manager.HandleMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _manager.Render(e.Graphics);
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
