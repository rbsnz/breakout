using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Breakout.Data;
using Breakout.Fonts;
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

            try
            {
                _graphics = CreateGraphics();

                _manager = new GameManager(
                    this,
                    new FontManager(@"res\font"),
                    new SoundManager(),
                    new HighScores()
                );

                _timer = new Timer { Interval = 1000 / 100 };
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"An error occurred during initialization.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                Close();
            }
        }

        public SizeF MeasureString(string text, Font font) => _graphics.MeasureString(text, font);

        private void OnLoad(object sender, EventArgs e)
        {
            _manager.AddScreen<TitleScreen>();

            _timer.Tick += OnTick;
            _timer.Start();
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
