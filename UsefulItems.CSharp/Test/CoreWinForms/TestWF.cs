using System;
using System.Windows.Forms;
using System.Drawing;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;

namespace UsefulItems.CSharp.Test.CoreWinForms
{
    public partial class TestWF : Form
    {
        private Vector2 point = new Vector2(20, 20);
        private Vector2 windowSize;
        private Graphics graphics;

        public TestWF()
        {
            InitializeComponent();
            windowSize = new Vector2(mainDisplay.Width, mainDisplay.Height);
            mainDisplay.Image = new Bitmap((int)windowSize.X, (int)windowSize.Y);
            graphics = Graphics.FromImage(mainDisplay.Image);
            timer.Start();
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            var angle = Math.PI / 50 / 2; // 0.05;
            point.Rotate(angle);
            DrawPoint();
        }

        private void DrawPoint()
        {
            //graphics.Clear(Color.Black);
            DrawPoint(point, Brushes.Red);
            mainDisplay.Refresh();
        }

        private void DrawPoint(Vector2 point, Brush brush)
        {
            var x = (int)(windowSize.X / 2 + point.X);
            var y = (int)(windowSize.Y / 2 - point.Y);
            graphics.FillRectangle(brush, x, y, 1, 1);
        }
    }
}
