using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Rendering
{
    public class Renderer : Component
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Transform _transform;

        public Renderer(GameObject gameObject, SpriteBatch spriteBatch, Texture2D texture) : base(gameObject)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;
            _transform = gameObject.Transform;
            gameObject.GameCenter.Rendered += Render;
        }

        private void Render()
        {
            
        }
    }
}