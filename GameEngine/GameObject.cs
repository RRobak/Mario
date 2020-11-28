using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace GameEngine
{
    public class GameObject : IDisposable
    {
        private RenderWindow window;
        public RenderWindow Window { get { return this.window; } }
        public SceneManager SceneManager = new SceneManager();
        

        public GameObject(string Title)
        {
            window = new RenderWindow(new VideoMode(1024u, 768u), Title, Styles.Default);
            window.SetVisible(true);
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(30);
            window.Closed += windowClosed;
            window.KeyPressed += windowKeyPressed;
            window.KeyReleased += windowKeyReleased;
        }

        void windowKeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            SceneManager.CurrentScene.HandleKeyPress(e);
        }

        void windowKeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            SceneManager.CurrentScene.HandleKeyReleased(e);
        }

        void windowClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        public void Close()
        {
            this.window.Close();
        }
        public float GetWindowX()
        {
            return window.Size.X;
        }
        public float GetWindowY()
        {
            return window.Size.Y;
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
                window.Dispose();
            }
        }
    }
}
