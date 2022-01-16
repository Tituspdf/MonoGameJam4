using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Rendering;

namespace MonoGameJam4.Engine.Interfaces
{
    /// <summary>
    /// interface that makes the class visible for the <see cref="GameCenter.Draw"/> method from the game center 
    /// </summary>
    public interface IRenderCall
    {
        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow);
    }
}