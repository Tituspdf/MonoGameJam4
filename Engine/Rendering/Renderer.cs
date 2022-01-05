using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.Engine.Rendering
{
    public class Renderer : Component
    {
        private readonly SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private readonly string _textureName;
        private readonly Transform _transform;
        private readonly Camera _camera;

        public Renderer(GameObject gameObject, string textureName) : base(gameObject)
        {
            _spriteBatch = gameObject.GameCenter.SpriteBatch;
            _textureName = textureName;
            _transform = gameObject.Transform;
            gameObject.GameCenter.Rendered += Render;
            _camera = gameObject.GameCenter.Camera;
        }

        private void Render()
        {
            _texture ??= GameObject.GameCenter.ContentLoader.Textures[_textureName];
            
            Vector2 relativePosition = _transform.Position - _camera.Transform.Position;
            relativePosition *= _camera.Zoom * _camera.PixelsPerUnit;
            _spriteBatch.Draw(_texture, relativePosition, Color.White);
        }
    }
}