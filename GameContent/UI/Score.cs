using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Entities;

namespace MonoGameJam4.GameContent.UI
{
    public class Score : GameObject, IRenderCall
    {
        private readonly SpriteFont _font;
        private readonly Texture2D _squareTexture;
        private Vector2 _position;

        private Player _player;
        private GameCenter _gameCenter;

        public Score(GameCenter gameCenter, Transform transform, string name, Player player) : base(gameCenter, transform, name)
        {
            _player = player;
            _font = gameCenter.ContentLoader.ScoreFont;
            _squareTexture = gameCenter.ContentLoader.Textures["Square"];
            _gameCenter = gameCenter;

        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            _position = new Vector2(_gameCenter.GameWindow.ScreenMiddlePoint.X, 150);
            string text = _player.Score.ToString();
            Vector2 size = _font.MeasureString(text);
            spriteBatch.DrawString(_font, text, _position - size / 2, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

            string upgradeState = $"level {_player.CurrentLevel} - next level at {(_player.CurrentLevel + 1) * 5}";
            size = _font.MeasureString(upgradeState);
            _position += new Vector2(0, 45);
            spriteBatch.DrawString(_font, upgradeState, _position, Color.White, 0, size / 2, Vector2.One * 0.5f, SpriteEffects.None, 0);
            
            
            if (_player.State != Player.PlayerState.Upgrading) return;
            
            spriteBatch.Draw(_squareTexture, new Rectangle(Point.Zero, gameWindow.ScreenSize.ToPoint()), null, Color.Black * 0.5f, 0, Vector2.Zero, SpriteEffects.None, 1);
            
            text = "Press 'SPACE' to upgrade!";
            size = _font.MeasureString(text);
            spriteBatch.DrawString(_font, text, gameWindow.ScreenMiddlePoint, Color.White, 0, size / 2, Vector2.One, SpriteEffects.None, 0);
        }
    }
}