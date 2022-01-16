using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Entities;

namespace MonoGameJam4.GameContent.UI
{
    public class HealthBar : GameObject, IRenderCall
    {
        private readonly Texture2D _heartTexture;
        private readonly Texture2D _frameTexture;
        private readonly Texture2D _fillingTexture;

        private Player _player;

        private const int BarLength = 175;
        private static readonly Point BarPosition = new Point(110, 50);

        public HealthBar(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter,
            transform, name)
        {
            _player = player;
            _frameTexture = gameCenter.ContentLoader.Textures["Frame"];
            _fillingTexture = gameCenter.ContentLoader.Textures["Square"];
            _heartTexture = gameCenter.ContentLoader.Textures["Heart"];
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            spriteBatch.Draw(_heartTexture, new Rectangle(new Point(50), new Point(50)), Color.White);
            spriteBatch.Draw(_frameTexture, new Rectangle(BarPosition, new Point(BarLength, 50)), Color.White);
            spriteBatch.Draw(_fillingTexture,
                new Rectangle(BarPosition, new Point((int) Math.Round((float) BarLength * _player.HealthProgress), 50)),
                Color.White);
        }
    }
}