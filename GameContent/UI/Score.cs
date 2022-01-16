using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.UI
{
    public class Score : GameObject, IRenderCall
    {
        public Score(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            
        }
    }
}