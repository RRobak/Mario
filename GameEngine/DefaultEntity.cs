using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class DefaultEntity:Entity,IDisposable
    {
        public DefaultEntity(GameObject gameObject, string Name,int x,int y) : base(gameObject)
        {
            this.Name = Name;
            this.SetEntitySpriteSheet(ResourceManager.GetInstance().GetSpriteSheet(Name));
            this.IsPlayer = true;
            this.Facing = Direction.RIGHT;
            this.X = x;
            this.Y = y;
            this.Acceleration = 10;
            this.AllowOffscreen = false;
        }
        public DefaultEntity(GameObject gameObject, string Name) : base(gameObject)
        {
            this.Name = Name;
            this.SetEntitySpriteSheet(ResourceManager.GetInstance().GetSpriteSheet(Name));
            this.Facing = Direction.NONE;
            this.IsPlayer = false;
            this.IsStatic = true;
            this.Acceleration = 0;
            this.AllowOffscreen = false;
        }
        public DefaultEntity(GameObject gameObject, string Name,int Acceleration) : base(gameObject)
        {
            this.Name = Name;
            this.SetEntitySpriteSheet(ResourceManager.GetInstance().GetSpriteSheet(Name));
            this.Facing = Direction.LEFT;
            this.IsPlayer = false;
            this.IsMoving = true;
            this.Acceleration = Acceleration;
            this.AllowOffscreen = true;
        }
    }
    
}
