using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.UI
{
    public class HealthBar : GameObject, IRenderCall
    {
        private Texture2D _frameTexture;
        private Texture2D _fillingTexture;
        
        public HealthBar(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            _frameTexture = gameCenter.ContentLoader.Textures["Frame"];
            _fillingTexture = gameCenter.ContentLoader.Textures["Square"];
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            spriteBatch.Draw(_frameTexture, new Rectangle(new Point(50), new Point(50, 150)), Color.White);
        }
    }
}