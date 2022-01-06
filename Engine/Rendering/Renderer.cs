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
        private Window _window;

        public Renderer(GameObject gameObject, string textureName) : base(gameObject)
        {
            _spriteBatch = gameObject.GameCenter.SpriteBatch;
            _textureName = textureName;
            _transform = gameObject.Transform;
            gameObject.GameCenter.Rendered += Render;
            _camera = gameObject.GameCenter.Camera;
            _window = gameObject.GameCenter.GameWindow;
        }

        private void Render()
        {
            _texture ??= GameObject.GameCenter.ContentLoader.Textures[_textureName];

            Vector2 size = _transform.Scale * _camera.PixelsPerUnit;
            
            // find out the position relative to the camera
            Vector2 relativePosition = _transform.Position - _camera.Transform.Position;
            // convert the position into pixel space
            relativePosition *= _camera.Zoom * _camera.PixelsPerUnit;
            // move the origin point to the bottom left of the sprite
            relativePosition.Y -= size.Y;
            // align the position relative to the screen center
            relativePosition += _window.ScreenMiddlePoint;
            // draw the sprite
            _spriteBatch.Draw(_texture, new Rectangle((int)relativePosition.X, (int)relativePosition.Y,(int)size.X, (int)size.Y), Color.White);
        }
    }
}