using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using UsefulItems.CSharp.DarkGraphicsLib.CommonElements;
using UsefulItems.CSharp.DarkGraphicsLib.ParticleSystem2D;

using TestStstem = UsefulItems.CSharp.DarkGraphicsLib.ParticleSystem2D.Test;

namespace UsefulItems.CSharp.Test.CoreWinForms
{
    public partial class TestWF : Form
    {
        private CentralСoordinateSystemConverter converter;
        private TestStstem testSystem;
        private Graphics graphics;

        public TestWF()
        {
            InitializeComponent();
            converter = new CentralСoordinateSystemConverter(mainDisplay.Width, mainDisplay.Height);
            mainDisplay.Image = new Bitmap(mainDisplay.Width, mainDisplay.Height);
            graphics = Graphics.FromImage(mainDisplay.Image);
            testSystem = new TestStstem()
            {
                Converter = converter,
                DrawParticle = DrawParticle,
                DrawEmitter = DrawEmitter,
                DrawField = DrawField
            };
            testSystem.AddStartElements();
            timer.Start();
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            timer.Stop();
            graphics.Clear(Color.Black);
            testSystem.Step();
            mainDisplay.Refresh();
            timer.Start();
        }

        private void DrawEmitter(Emitter emitter)
        {
            var newPosition = converter.ConvertToScreen(emitter.Position);
            graphics.FillRectangle(Brushes.Yellow, (int)newPosition.X, (int)newPosition.Y, (int)emitter.Size.X, (int)emitter.Size.Y);
        }

        private void DrawField(MassField field)
        {
            var brush = field.Mass == 0
                ? Brushes.Gray
                : field.Mass < 0
                    ? Brushes.Red
                    : Brushes.Green;
            var newPosition = converter.ConvertToScreen(field.Position);
            graphics.FillEllipse(brush, (int)newPosition.X - 5, (int)newPosition.Y - 5, 10, 10);
        }

        private void DrawParticle(Particle particle)
        {
            var newCoords = converter.ConvertToScreen(particle.Position);
            graphics.FillRectangle(Brushes.Blue, (int)newCoords.X, (int)newCoords.Y, 1, 1);
        }
    }
}
