using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace Mario.Characters
{
    public class Brick : DefaultEntity
    {
        private bool bumping = false;

        public Brick(GameObject gameObject) : base(gameObject,"brick")
        {
            this.GetEntitySpriteSheet().DefineFrames(Direction.NONE, new int[] { 0 });
        }

        public override void Update()
        {
            base.Update();

            if (bumping)
            {
                Velocity += 5;
                if (Velocity == 0)
                    bumping = false;
            }
            else
                Velocity = 0;
        }

        public override void OnCharacterCollision(Entity e, Direction d)
        {
            if (e.IsPlayer && e.HasUpwardVelocity)
            {
                if (this.Y + this.sprite.TextureRect.Height <= e.Y + Math.Abs(e.Velocity))
                {
                    e.Y = this.Y + this.sprite.TextureRect.Height;
                    e.Velocity = 5;
                    Velocity = -20;
                    bumping = true;
                }
            }
            base.OnCharacterCollision(e, d);
        }
    }
    }

