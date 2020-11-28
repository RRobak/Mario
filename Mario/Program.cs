using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine;

namespace Mario
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {

            var game = new GameObject("Mario");

            ResourceManager.GetInstance().LoadSpriteSheetFromFile("mario", @"resources\mario.png", 10);
            
            MainScene s = new MainScene(game);
            s.Name = "play"; 
            game.SceneManager.AddScene(s);
            

            GameOver gameOver = new GameOver(game);
            gameOver.Name = "gameover";
            game.SceneManager.AddScene(gameOver);
            game.SceneManager.StartScene("gameover");

        }
    }
}
