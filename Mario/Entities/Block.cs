using GameEngine;
namespace Mario.Characters
{     public class Block : DefaultEntity
    {
        public Block(GameObject gameObject): base(gameObject,"block")
        {
            this.GetEntitySpriteSheet().DefineFrames(Direction.NONE, new int[] { 0 });
        }
    }
}
