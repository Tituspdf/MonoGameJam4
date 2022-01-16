using Microsoft.Xna.Framework;
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
        private readonly Texture2D _empty;
        private readonly Texture2D _full;
        private readonly Texture2D _icon;

        private readonly Point _anchorRight;
        
        public BulletDisplay(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter, transform, name)
        {
            _player = player;
            _full = gameCenter.ContentLoader.Textures["Point"];
            _empty = gameCenter.ContentLoader.Textures["EmptyPoint"];
            _icon = gameCenter.ContentLoader.Textures["Square"];

            _anchorRight = new Point((int) gameCenter.GameWindow.ScreenSize.X - 50, 50);
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            
        }
    }
}