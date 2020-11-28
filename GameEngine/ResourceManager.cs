using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Audio;

namespace GameEngine
{
    public class ResourceManager
    {
        private static ResourceManager instance = null;
        Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        Dictionary<string, SpriteSheet> spriteSheets = new Dictionary<string, SpriteSheet>();

        public static ResourceManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ResourceManager();
            }
            return instance;
        }

        public void LoadTexture(string name, string path)
        {
            if(GetTexture(name) == null)
            { 
                Texture texture = new Texture(path);
                textures.Add(name, texture);
            }
        }

        public Texture GetTexture(string name)
        {
            if (textures.ContainsKey(name))
                return textures[name];
            else return null;
        }

        public void LoadSpriteSheetFromFile(string name, string path, int totalFrames)
        {
            if (!spriteSheets.ContainsKey(name))
            {
                SpriteSheet spriteSheet = new SpriteSheet();
                spriteSheet.texture = new Texture(path);
                spriteSheet.TotalFrames = totalFrames;

                spriteSheets.Add(name, spriteSheet);
            }
        }

        public SpriteSheet GetSpriteSheet(string name)
        {
            if (spriteSheets.ContainsKey(name))
                return spriteSheets[name];
            else return null;
        }

    }
}
