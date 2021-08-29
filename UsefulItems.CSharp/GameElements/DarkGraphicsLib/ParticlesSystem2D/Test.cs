using System;
using System.Collections.Generic;
using System.Text;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D.Common;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D
{
    public class Test
    {
        private Random random = new Random();

        private List<Particle> particles = new List<Particle>();
        private List<Emitter> emitters = new List<Emitter>();
        private List<MassField> fields = new List<MassField>();
        private const int maxParticles = 20000;
        private const int emitPerStep = 20;

        public CentralСoordinateSystemConverter Converter { get; set; }

        public Action<Particle> DrawParticle { get; set; }
        public Action<MassField> DrawField { get; set; }
        public Action<Emitter> DrawEmitter { get; set; }

        public void AddStartElements()
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

        public void Step()
        {
            RemoveParticles();
            EmitParticles();

            foreach (var particle in particles)
            {
                particle.AffectByFields(fields);
                particle.Move();
            }

            foreach (var emitter in emitters)
            {
                DrawEmitter(emitter);
            }

            foreach (var field in fields)
            {
                DrawField(field);
            }

            foreach (var particle in particles)
            {
                DrawParticle(particle);
            }
        }

        private void RemoveParticles()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                var position = Converter.ConvertToScreen(particles[i].Position);
                if (position.X < 0 || position.X >= Converter.Width ||
                    position.Y < 0 || position.Y >= Converter.Height)
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
    }
}
