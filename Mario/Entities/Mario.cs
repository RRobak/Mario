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
    public class Mario : DefaultEntity
    {     
        public Mario(GameObject gameObject) : base(gameObject,"mario", 192, 576)
        {
            this.GetEntitySpriteSheet().DefineFrames(Direction.RIGHT, new int[] { 5, 6, 7, 9 });
            this.GetEntitySpriteSheet().DefineFrames(Direction.LEFT, new int[] { 4, 3, 2, 0 });
            this.GetEntitySpriteSheet().DefineFrames(Direction.JUMPRIGHT, new int[] { 8 });
            this.GetEntitySpriteSheet().DefineFrames(Direction.JUMPLEFT, new int[] { 1 });
        }
        public override void Update()
        {
            if (this.Y > this.gameObject.Window.Size.Y - 64)
                Die();

            base.Update();
        }
        public override void OnCharacterCollision(Entity e, Direction d)
        {
            if (e.IsStatic || e.IgnorePlayerCollisions)
                return;

            if (this.Y < e.Y)
            {
                
                e.Delete = true;

                this.IsJumping = true;
                this.Velocity = -45;
            }
            else
            {
                    Die();
            }
        }
        public void Die()
        {
            this.IsMoving = false;
    
            ((MainScene)gameObject.SceneManager.CurrentScene).Lives--;

            if (((MainScene)gameObject.SceneManager.CurrentScene).Lives == 0)
                gameObject.SceneManager.StartScene("gameover");
            else
                gameObject.SceneManager.StartScene("play");
        }

    }
}
