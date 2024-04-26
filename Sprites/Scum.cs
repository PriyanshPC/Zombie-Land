using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZombieLand.Sprites
{
    public class Scum : Sprite, ICollidable
    {
        private float _timer;

        public Explosion Explosion;

        public float LifeSpan { get; set; }

        public Vector2 Velocity { get; set; }

        public Scum(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            Position += Velocity;
        }

        public void OnCollide(Sprite sprite)
        {


            // Bullets don't collide with Scum
            if (sprite is Bullet)
                return;

            // Enemies can't shoot eachother
            if (sprite is Enemy && Parent is Enemy)
                return;

            // Can't hit a player if they're dead
            if (sprite is Player && ((Player)sprite).IsDead)
                return;

            if (sprite is Enemy && Parent is Player)
            {
                IsRemoved = true;
                AddExplosion();
            }

            if (sprite is Player && Parent is Enemy)
            {
                IsRemoved = true;
                AddExplosion();
            }
        }

        private void AddExplosion()
        {
            if (Explosion == null)
                return;

            var explosion = Explosion.Clone() as Explosion;
            explosion.Position = Position;

            Children.Add(explosion);
        }
    }
}
