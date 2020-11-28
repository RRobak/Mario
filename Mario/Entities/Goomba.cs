using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Mario.Characters
{
    public class Goomba : DefaultEntity
    {
        public Goomba(GameObject gameObject):base(gameObject,"goomba",-10)
        {
            this.EntitySpriteSheet.DefineFrames(Direction.RIGHT, new int[] { 0, 1, 2 });
            this.EntitySpriteSheet.DefineFrames(Direction.LEFT, new int[] { 0, 1, 2 });
            this.EntitySpriteSheet.DefineFrames(Direction.JUMPRIGHT, new int[] { 0 });
            this.EntitySpriteSheet.DefineFrames(Direction.JUMPLEFT, new int[] { 0 });
            this.Facing = Direction.LEFT;
        }
        public override void Update()
        {
            if (IsBumpedFromBelow)
            {
                this.IgnoreAllCollisions = true;
                this.IsAffectedByGravity = true;
                this.Velocity = -40;
                this.IsJumping = true;
                this.EntitySpriteSheet.DefineFrames(Direction.JUMPRIGHT, new int[] { 2 });
                this.EntitySpriteSheet.DefineFrames(Direction.JUMPLEFT, new int[] { 2 });

                IsBumpedFromBelow = false;
            }

            base.Update();
        }

        public override void OnCharacterCollision(Entity e, Direction d)
        {
            base.OnCharacterCollision(e, d);
            if (e.GetType() == typeof(Mario) && e.Y <= this.Y)
            {
                e.IsJumping = true;
                e.Velocity = -45;
            }
        }

    }
}
