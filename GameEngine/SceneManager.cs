using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace GameEngine
{
    public class SceneManager
    {
        Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

        public Scene CurrentScene = null;

        public SceneManager()
        {

        }

        public void AddScene(Scene s)
        {
            scenes.Add(s.Name, s);

            s.Initialize();
        }

        public void StartScene(string name)
        {
            

            CurrentScene = scenes[name];

            CurrentScene.Reset();
            CurrentScene.Run();
        }
        public Scene GetScene(string name)
        {
            return scenes[name];
        }

    }
}
