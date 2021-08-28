using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D.Common;

namespace UsefulItems.CSharp.Test.CoreWinForms
{
    public partial class TestWF : Form
    {
        private Random random = new Random();

        private CentralСoordinateSystemConverter converter;
        private Graphics graphics;

        private List<Particle> particles = new List<Particle>();
        private List<Emitter> emitters = new List<Emitter>();
        private List<MassField> fields = new List<MassField>();
        private const int maxParticles = 20000;
        private const int emitPerStep = 20;

        public TestWF()
        {
            InitializeComponent();
            converter = new CentralСoordinateSystemConverter(mainDisplay.Width, mainDisplay.Height);
            mainDisplay.Image = new Bitmap(mainDisplay.Width, mainDisplay.Height);
            graphics = Graphics.FromImage(mainDisplay.Image);
            InitParticlesSystem();
            timer.Start();
        }

        private void InitParticlesSystem()
        {
            emitters.Add(EmitterBuilder.NewBuilder
                .SetRandom(random)
                .SetPosition(0, 0)
                .SetSize(1, 1)
                .SetStartSpeed(0, 2)
                .SetSpeedDelta(0)
                .SetAngleDelta(Math.PI)
                .Create());

            fields.Add(MassFieldBuilder.NewBuilder
                .SetPosition(-150, 0)
                .SetMass(900)
                .Create());

            fields.Add(MassFieldBuilder.NewBuilder
                .SetPosition(-50, 0)
                .SetMass(-50)
                .Create());
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            timer.Stop();
            Step();
            timer.Start();
        }

        private void Step()
        {
            RemoveParticles();
            EmitParticles();

            foreach (var particle in particles)
            {
                particle.AffectByFields(fields);
                particle.Move();
            }

            graphics.Clear(Color.Black);

            foreach (var emitter in emitters)
            {
                DrawEmitter(emitter, Brushes.Yellow);
            }

            foreach (var field in fields)
            {
                var brush = field.Mass == 0
                    ? Brushes.Gray
                    : field.Mass < 0
                        ? Brushes.Red
                        : Brushes.Green;
                DrawField(field, brush);
            }

            foreach (var particle in particles)
            {
                DrawParticle(particle.Position, Brushes.Blue);
            }

            mainDisplay.Refresh();
        }

        private void RemoveParticles()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                var position = converter.ConvertToScreen(particles[i].Position);
                if (position.X < 0 || position.X >= mainDisplay.Width ||
                    position.Y < 0 || position.Y >= mainDisplay.Height)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        private void EmitParticles()
        {
            for (int step = 0; step < emitPerStep; step++)
            {
                if (particles.Count >= maxParticles) return;
                foreach (var emitter in emitters)
                {
                    var particle = emitter.EmitParticle();
                    particles.Add(particle);
                }
            }
        }

        private void DrawEmitter(Emitter emitter, Brush brush)
        {
            var newPosition = converter.ConvertToScreen(emitter.Position);
            graphics.FillRectangle(brush, (int)newPosition.X, (int)newPosition.Y, (int)emitter.Size.X, (int)emitter.Size.Y);
        }

        private void DrawField(MassField field, Brush brush)
        {
            var newPosition = converter.ConvertToScreen(field.Position);
            graphics.FillEllipse(brush, (int)newPosition.X - 5, (int)newPosition.Y - 5, 10, 10);
        }

        private void DrawParticle(Vector2 position, Brush brush)
        {
            var newCoords = converter.ConvertToScreen(position);
            graphics.FillRectangle(brush, (int)newCoords.X, (int)newCoords.Y, 1, 1);
        }
    }
}
