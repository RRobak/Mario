using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

namespace Mario
{
    public class MainScene : Scene
    {
        public Entity player;
        public int Lives = 3;
        private readonly string mapFileName = System.IO.Directory.GetCurrentDirectory() + @"\leveldata.xml";
        Clock coordinatedAnimClock = new Clock();
        private int coordinatedAnimFramePointer = 0;

        public MainScene(GameObject gameObject) : base(gameObject)
        {

        }
        public override void Initialize()
        {
            this.level.LoadMap(this.mapFileName, 1);
            this.BackgroundColor = this.level.BackgroundColor;

            viewPort = new Viewport(this.gameObject, 64, 64, this.level);


            base.Initialize();
        }

        public override void Reset()
        {
            this.level.LoadMap(this.mapFileName, 1);
            viewPort.Reset();
            this.Entities.Clear();
            Characters.Mario mario = new Characters.Mario(this.gameObject);
            player = mario;
            this.Entities.Add(player);
            base.Reset();
        }

        public override void HandleKeyPress(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Right)
            {
                player.Facing = Direction.RIGHT;
                player.IsMoving = true;
            }

            if (e.Code == Keyboard.Key.Left)
            {
                player.Facing = Direction.LEFT;
                player.IsMoving = true;
            }

            if (e.Code == Keyboard.Key.Space && player.IsJumping == false)
            {
                player.IsJumping = true;
                player.Velocity = -55;
            }

            if (e.Code == Keyboard.Key.Escape)
                this.gameObject.Close();

            base.HandleKeyPress(e);
        }

        public override void HandleKeyReleased(KeyEventArgs e)
        {
            if (!player.IsJumping)
                player.IsMoving = false;
        }

        public override void Update()
        {
            for (int x = 0; x < Entities.Count; x++)
            {
                Entity e = (Entity)Entities[x];
                if (coordinatedAnimClock.ElapsedTime.AsMilliseconds() > 100)
                {
                    foreach (Entity cz in Entities.Where(zx => zx.GetType() == typeof(Mario.Characters.CoinBox)))
                        cz.sprite.TextureRect = cz.EntitySpriteSheet.GetSprite(Direction.NONE, coordinatedAnimFramePointer);

                    foreach (Entity cz in Entities.Where(zx => zx.GetType() == typeof(Mario.Characters.Coin)))
                        cz.sprite.TextureRect = cz.EntitySpriteSheet.GetSprite(Direction.NONE, coordinatedAnimFramePointer);

                    coordinatedAnimClock.Restart();
                    coordinatedAnimFramePointer++;
                    if (coordinatedAnimFramePointer >= e.EntitySpriteSheet.SpriteFrames.Count)
                        coordinatedAnimFramePointer = 0;
                }
                e.Update();
                e.Draw();
                if (e.Velocity > viewPort.TileHeight) e.Velocity = viewPort.TileHeight;
                if (e.Velocity < -viewPort.TileHeight) e.Velocity = -viewPort.TileHeight;
                if (e.Y > -1)
                    CheckCharacterCollisions(e);

                
            }
            ViewportScrollHandler();
            for (int i = Entities.Count - 1; i >= 0; i--)
                if (Entities[i].Delete)
                    Entities.RemoveAt(i);
        }

        private void CheckCharacterCollisions(Entity e)
        {
            for (int x = 0; x < Entities.Count; x++)
            {
                Entity c = (Entity)Entities[x];
                if (c.IsStatic || c.IgnoreAllCollisions)
                    continue;

                if (c.ID != e.ID)
                {
                    FloatRect entityRect = e.sprite.GetGlobalBounds();
                    FloatRect area = new FloatRect();
                    FloatRect boundBox = c.sprite.GetGlobalBounds();
                    boundBox.Top = boundBox.Top + boundBox.Height + 1;
                    boundBox.Height = 1;

                    if (boundBox.Intersects(entityRect))
                    {
                        e.OnCharacterCollision(c, Direction.DOWN);
                        continue;
                    }

                    boundBox = c.sprite.GetGlobalBounds();
                    boundBox.Top = boundBox.Top - 1;
                    boundBox.Height = 1;

                    if (boundBox.Intersects(entityRect, out area))
                    {
                        e.OnCharacterCollision(c, Direction.UP);
                        continue;
                    }

                    boundBox = c.sprite.GetGlobalBounds();
                    boundBox.Left = boundBox.Left + boundBox.Width;
                    boundBox.Width = 1;

                    if (boundBox.Intersects(entityRect))
                    {
                        e.OnCharacterCollision(c, Direction.LEFT);
                        c.OnCharacterCollision(e, Direction.RIGHT);
                        continue;
                    }

                    boundBox = c.sprite.GetGlobalBounds();
                    boundBox.Width = 1;

                    if (boundBox.Intersects(entityRect))
                    {
                        e.OnCharacterCollision(c, Direction.RIGHT);
                        c.OnCharacterCollision(e, Direction.LEFT);
                        continue;
                    }

                }
            }

        }

        protected void ViewportScrollHandler()
        {         
            int midScreen = (int)gameObject.Window.Size.Y / 2;
            if (player.IsMoving && player.Facing == Direction.RIGHT && player.X >= midScreen && !viewPort.IsEndOfLevel)
            {
                player.X = midScreen;

                foreach (Entity character in gameObject.SceneManager.CurrentScene.Entities)
                {
                    if (!character.IsPlayer && character.Facing == Direction.RIGHT)
                    {
                        character.Acceleration = 0;
                    }
                    if (!character.IsPlayer && character.Facing == Direction.LEFT)
                    {
                        character.Acceleration = -20;
                    }


                }

                viewPort.Scroll(Direction.RIGHT, player.Acceleration);
            }
            else
            {
                foreach (Entity character in gameObject.SceneManager.CurrentScene.Entities)
                {
                    if (!character.IsPlayer && !character.IsStatic)
                    {
                        if (character.Acceleration == 0) character.Acceleration = 1;
                        character.Acceleration = Math.Sign(character.Acceleration) * 10;
                    }
                }

            }

        }

        public override void DrawBackground()
        {
            List<Entity> NewEntities = viewPort.Render();

            foreach (Entity e in NewEntities)
            {
                Entity c = null;

                switch (e.Name)
                {

                    case "ground": c = new Characters.Ground(this.gameObject); break;
                    case "brick": c = new Characters.Brick(this.gameObject); break;
                    case "pipeleftup": c = new Characters.Pipeleftup(this.gameObject); break;
                    case "piperightup": c = new Characters.Piperightup(this.gameObject); break;
                    case "pipeleft": c = new Characters.Pipeleft(this.gameObject); break;
                    case "piperight": c = new Characters.Piperight(this.gameObject); break;
                    case "coinbox": c = new Characters.CoinBox(this.gameObject); break;
                    case "emptycoinbox": c = new Characters.EmptyCoinBox(this.gameObject); break;
                    case "coinbounce": c = new Characters.CoinBounce(this.gameObject); break;
                    case "block": c = new Characters.Block(this.gameObject); break;
                    case "goomba": c = new Characters.Goomba(this.gameObject); break;
                    case "koopatroopa": c = new Characters.KoopaTroopa(this.gameObject); break;
                }

                c.X = e.X;
                c.Y = e.Y;
                c.OriginTileCol = e.OriginTileCol;
                c.OriginTileRow = e.OriginTileRow;

                var xx = Entities.Where(x => x.OriginTileRow == c.OriginTileRow && x.OriginTileCol == c.OriginTileCol);
                if (xx.Count() == 0)
                    Entities.Add(c);
            }

            NewEntities.Clear();

        }
    }
}
