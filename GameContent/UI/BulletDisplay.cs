using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Entities;

namespace MonoGameJam4.GameContent.UI
{
    public class BulletDisplay : GameObject, IRenderCall
    {
        private readonly Player _player;
        
        public BulletDisplay(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter, transform, name)
        {
            _player = player;
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            
        }
    }
}