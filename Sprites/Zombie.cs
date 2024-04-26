using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieLand.Sprites
{
    public class Zombie : Sprite, ICollidable
    {
        public int Health { get; set; }

        public Scum Scum { get; set; }

        public float Speed;

        public Zombie(Texture2D texture) : base(texture)
        {
        }

        protected void Shoot(float speed)
        {

            var scum = Scum.Clone() as Scum;
            scum.Position = Position;
            scum.Layer = 0.1f;
            scum.LifeSpan = 5f;
            scum.Velocity = new Vector2(speed, 0f);
            scum.Parent = this;

            Children.Add(scum);
        }

        public virtual void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
