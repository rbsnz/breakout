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

            _manager = new GameManager(Size);

            _timer = new Timer { Interval = 1000 / 60 };
            _timer.Tick += (s, e) => Invalidate();
            _timer.Start();
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
