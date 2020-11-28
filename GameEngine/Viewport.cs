using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;

namespace GameEngine
{
    public struct ScreenLocation
    {
        public int X;
        public int Y;
    }

    public class Viewport : IDisposable
    {
        private GameObject gameObject;
        private int tileHeight = 0;
        private int tileWidth = 0;
        private int screenWidth = 0;
        private int screenHeight = 0;
        private int scrollSpeed = 0;
        private int screenTilesPerColumn;   
        private int screenTilesPerRow;      
        private Level level;
        private int originXtile = 0; 
        public int xOffset = 0;     
        private int originYtile = 0; 
        public int yOffset = 0;     
        public List<Entity> BackSprites = new List<Entity>();

        Sprite spBack = new Sprite();

        public bool IsEndOfLevel
        {
            get 
            {
                return (screenTilesPerColumn + originYtile >= level.GetTileColumns-1);
            }

        }

        public bool IsStartOfLevel
        {
            get
            {
                return(originYtile == 0);
            }

        }
        public int TileHeight
        {
            get { return tileHeight; }
        }

        public Viewport(GameObject gameObject, int tileHeight, int tileWidth, Level level)
        {
           this.gameObject = gameObject;
           screenHeight = (int)gameObject.Window.Size.X;
           screenWidth = (int)gameObject.Window.Size.Y;
           this.tileHeight = tileHeight;
           this.tileWidth = tileWidth;
           this.level = level;

            screenTilesPerRow = screenWidth / tileWidth;
            screenTilesPerColumn = screenHeight / tileHeight;
        }

        public bool Scroll (Direction d, int Speed)
        {
            scrollSpeed = Speed;
            return Scroll(d);
        }

        public bool Scroll(Direction d)
        {
            if (d == Direction.RIGHT)
            {
                if (!IsEndOfLevel)
                {
                    yOffset = yOffset - scrollSpeed;

                    if (yOffset <= -tileWidth)
                    {
                        yOffset = 0;
                        originYtile++;
                    }

                    return true;
                }
                else
                    return false;
            }

            if (d == Direction.LEFT)
            {
                if (!IsStartOfLevel)
                {
                    yOffset = yOffset - scrollSpeed;

                    if (yOffset > tileWidth)
                    {
                        yOffset = 0;
                        originYtile--;
                    }

                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        public List<Entity> Render()
        {
            List<Entity> newEntities = new List<Entity>();

            BackSprites.Clear();

            int screenX = 0;
            int screenY = -1;

            for (int x = originXtile; x < screenTilesPerRow + originXtile; x++)
            {
                for (int y = originYtile; y < screenTilesPerColumn + originYtile + 2; y++)
                {
                    if (!level.Tiles[x, y].Background)
                    {
                        if (level.Tiles[x, y].Entity != "")
                        {
                            string entityName = level.Tiles[x, y].Entity;
                            if (level.Tiles[x, y].Static != true)
                            {
                                Tile t = new Tile();
                                t.Background = true;
                                level.Tiles[x, y] = t;
                            }

                            ScreenLocation sl = TileToScreen(x, y);
                            Entity e2 = new Entity(this.gameObject);
                            e2.Name = entityName;
                            e2.X = sl.X;
                            e2.Y = sl.Y;
                            e2.OriginTileRow = x;
                            e2.OriginTileCol = y;
                            newEntities.Add(e2);
                        }
                        else
                        { 
                            spBack = new Sprite(ResourceManager.GetInstance().GetTexture(level.Tiles[x,y].Resource));
                            int x1 = (tileHeight * screenY) + yOffset;
                            int y1 = (tileWidth * screenX) + xOffset;
                            spBack.Position = new Vector2f(x1, y1);
                            spBack.Draw(gameObject.Window, RenderStates.Default);

                        }
                        
                    }
                    screenY++;
                }

                screenY = -1;
                screenX++;
            }

            return newEntities;
        }

        public ScreenLocation TileToScreen(int row, int col)
        {
            ScreenLocation s = new ScreenLocation();

            s.Y = (row * tileHeight) * (originXtile+1);
            s.X = (col * tileWidth) - ((originYtile+1) * tileWidth);  
            return s;
        }

        public void Reset()
        {
            originYtile = 0;
            originXtile = 0;
            xOffset = 0;
            yOffset = 0;
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
                spBack.Dispose();
            }
        }

    }
}
