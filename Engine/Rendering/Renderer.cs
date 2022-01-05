using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Rendering
{
    public class Renderer
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Transform _transform;

        public Renderer(SpriteBatch spriteBatch, Texture2D texture, Transform transform)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;
            _transform = transform;
        }
    }
}