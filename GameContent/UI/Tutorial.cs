using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.UI
{
    public class Tutorial : GameObject, IRenderCall
    {
        private float _displayTime = 5;
        private const string Text = "use the mouse to look around,the left mouse button to shoot and 'wasd' to move";
        private readonly SpriteFont _font;

        private float _ocopacity = 1;

        public Tutorial(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            _font = gameCenter.ContentLoader.ScoreFont;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _displayTime -= Time.DeltaTime;
            if (_displayTime < 0)
            {
                _ocopacity -= 0.02f;
            }

            if (_ocopacity < 0)
            {
                Deconstruct();
            }
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            Vector2 size = _font.MeasureString(Text);
            spriteBatch.DrawString(_font, Text, gameWindow.ScreenMiddlePoint + new Vector2(0, 300), Color.White * _ocopacity, 0, size / 2,
                new Vector2(0.5f), SpriteEffects.None, 0.5f);
        }
    }
}