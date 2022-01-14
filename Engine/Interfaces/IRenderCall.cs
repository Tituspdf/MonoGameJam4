using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJam4.Engine.Interfaces
{
    /// <summary>
    /// interface that gives the derived class to be called by the <see cref="GameCenter.Draw"/> 
    /// </summary>
    public interface IRenderCall
    {
        public void Render(SpriteBatch spriteBatch);
    }
}