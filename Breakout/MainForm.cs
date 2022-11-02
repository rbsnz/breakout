using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Breakout.Game;

namespace Breakout
{
    public partial class MainForm : Form, IUiManager
    {
        // A graphics object used for measuring strings.
        private readonly Graphics _graphics;

        private readonly Timer _timer;

        private readonly GameManager _manager;

        public MainForm()
        {
            InitializeComponent();

            _graphics = CreateGraphics();

            _manager = new GameManager(this, new SoundManager());

            _timer = new Timer { Interval = 1000 / 100 };
            _timer.Tick += OnTick;
            _timer.Start();
        }

        public SizeF MeasureString(string text, Font font) => _graphics.MeasureString(text, font);

        private void OnLoad(object sender, EventArgs e)
        {
            _manager.Initialize();
            _manager.AddScreen<TitleScreen>();
        }

        private void OnMouseMove(object sender, MouseEventArgs e) => _manager.HandleMouseMove(e);
        private void OnMouseDown(object sender, MouseEventArgs e) => _manager.HandleMouseDown(e);
        private void OnMouseUp(object sender, MouseEventArgs e) => _manager.HandleMouseUp(e);

        private void OnKeyDown(object sender, KeyEventArgs e) => _manager.HandleKeyDown(e);
        private void OnKeyUp(object sender, KeyEventArgs e) => _manager.HandleKeyUp(e);

        private void OnTick(object sender, EventArgs e)
        {
            _manager.Update();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _manager.Draw(e.Graphics);
        }        
    }
}
