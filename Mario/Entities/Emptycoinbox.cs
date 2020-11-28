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
    public class EmptyCoinBox : DefaultEntity
    {
        public EmptyCoinBox(GameObject gameObject) : base(gameObject, "emptycoinbox")
        {
            this.GetEntitySpriteSheet().DefineFrames(Direction.NONE, new int[] { 0 });
        }


        public override void OnCharacterCollision(Entity e, Direction d)
        {
            if (e.IsPlayer && e.HasUpwardVelocity)
            {
                if (this.Y + this.sprite.TextureRect.Height <= e.Y + Math.Abs(e.Velocity))
                {
                    e.Y = this.Y + this.sprite.TextureRect.Height;
                    e.Velocity = 5;
                }
            }

            base.OnCharacterCollision(e, d);

        }


    }
}
