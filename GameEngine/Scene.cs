using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Diagnostics;

namespace GameEngine
{
    public class Scene : IDisposable 
    {
        public string Name;
        public List<Entity> Entities = new List<Entity>();
        public Viewport viewPort;
        public Texture BackgroundTexture;
        public long LastCycleTime = 0;
        DateTime currentTime = System.DateTime.Now;
        DateTime targetTime = System.DateTime.Now;
        private bool pause = false;
        protected GameObject gameObject;
        protected Sprite BackSprite;
        public Color BackgroundColor = Color.Black;
        public Level level = new Level();
       
        public Scene(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public virtual void Initialize()
        {
            if (this.BackgroundTexture == null)
                BackSprite = new Sprite();
            else
                BackSprite = new Sprite(this.BackgroundTexture);
        }

        public virtual void Reset()
        {

        }

        public void Run()
        {
       
            while (gameObject.Window.IsOpen)
            {
                currentTime = System.DateTime.Now;

                gameObject.Window.Clear(this.BackgroundColor);

                this.DrawBackground();

                if (!pause)
                {
                    this.Update();
                    gameObject.Window.Display();
                }

                gameObject.Window.DispatchEvents();
            }
        }

        public virtual void HandleKeyPress(KeyEventArgs e)
        {

        }

        public virtual void HandleKeyReleased(KeyEventArgs e)
        {

        }

        public virtual void DrawBackground()
        {
            BackSprite.Position = new Vector2f(0, 0);

            BackSprite.Draw(gameObject.Window, RenderStates.Default);
        }

        public virtual void Update()
        {

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                BackSprite.Dispose();
            }
        }
        
        

        
    }
}
