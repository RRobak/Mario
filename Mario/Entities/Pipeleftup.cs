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
    class Pipeleftup: DefaultEntity
    {
            public Pipeleftup(GameObject gameObject): base(gameObject, "pipeleftup")
            {
                 this.GetEntitySpriteSheet().DefineFrames(Direction.NONE, new int[] { 0 });
            }


        }
}



