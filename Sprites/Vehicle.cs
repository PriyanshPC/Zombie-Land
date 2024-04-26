using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieLand.Sprites
{
    public class Vehicle : Sprite, ICollidable
    {
        public int Health { get; set; }

        public Bullet Bullet { get; set; }

        public float Speed;

        public Vehicle(Texture2D texture) : base(texture)
        {
        }

        protected void Shoot(float speed)
        {
            var X = Position.X;
            var Y = Position.Y - 55;

            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = new Vector2 (X,Y);
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(speed, 0f);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public virtual void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
