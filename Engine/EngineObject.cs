using Microsoft.Xna.Framework;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4.Engine
{
    public abstract class EngineObject : IUpdateable
    {
        public virtual void OnInitialize()
        {
            
        }
        
        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}