using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.UI
{
    public class Score : GameObject, IRenderCall
    {
        private readonly SpriteFont _font;
        private readonly Vector2 _position;
        
        public Score(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            _font = gameCenter.ContentLoader.ScoreFont;
            _position = new Vector2(gameCenter.GameWindow.ScreenMiddlePoint.X, 150);
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            string text = "Hello World";
            Vector2 size = _font.MeasureString(text);
            spriteBatch.DrawString(_font, text, _position - size / 2, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }
    }
}