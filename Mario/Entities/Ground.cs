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
    public class Ground : DefaultEntity
    {
        public Ground(GameObject gameObject): base(gameObject, "ground")
        {
            this.GetEntitySpriteSheet().DefineFrames(Direction.NONE, new int[] { 0 });
        }

    }
}
