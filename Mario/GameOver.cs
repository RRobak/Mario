using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

namespace Mario
{
    public class GameOver : Scene
    {
        Text text;

        public GameOver(GameObject gameObject) : base(gameObject)
        {
            this.BackgroundColor = Color.Black;
        }

        public override void Initialize()
        {
            Font arial = new Font(@"resources\arial.ttf");

            text = new Text("", arial);
            text.Position = new Vector2f(0, 0);
            text.CharacterSize = 30;

        }

        public override void Reset()
        {
           
        }

        public override void HandleKeyPress(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                gameObject.SceneManager.StartScene("play");
            }

            if (e.Code == Keyboard.Key.Escape)
                this.gameObject.Window.Close();

            base.HandleKeyPress(e);
        }

        public override void Update()
        {
            string t;
            t = "GAME OVER \npress [space]";
            text.DisplayedString = t;
            text.Position = new Vector2f(gameObject.GetWindowX()/2, gameObject.GetWindowY()/2);
            text.Draw(this.gameObject.Window, RenderStates.Default);

            ((MainScene)gameObject.SceneManager.GetScene("play")).Lives = 3;
           

        }

        

        public override void DrawBackground()
        {
        }
    }
}
