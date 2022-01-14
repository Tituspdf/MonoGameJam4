using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine.Entities;
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
        private readonly Window _window;
        private Color _color = Color.White;

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
            // fix direction
            relativePosition.Y *= -1;
            // align the position relative to the screen center
            relativePosition += _window.ScreenMiddlePoint;
            
            Vector2 origin = _texture.Bounds.Center.ToVector2();
            
            // hint: the renderer takes the rotation as degrees
            float rotation = _transform.Rotation;
            
            Rectangle destinationRectangle = new Rectangle((int) (relativePosition.X), (int) (relativePosition.Y), (int) size.X, (int) size.Y);
            
            _spriteBatch.Draw(_texture, destinationRectangle, null, _color, rotation, origin, SpriteEffects.None, 1);
            
            // https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html#Microsoft_Xna_Framework_Graphics_SpriteBatch_Draw_Microsoft_Xna_Framework_Graphics_Texture2D_Microsoft_Xna_Framework_Vector2_System_Nullable_Microsoft_Xna_Framework_Rectangle__Microsoft_Xna_Framework_Color_System_Single_Microsoft_Xna_Framework_Vector2_Microsoft_Xna_Framework_Vector2_Microsoft_Xna_Framework_Graphics_SpriteEffects_System_Single_
        }

        public override void Deconstruct()
        {
            base.Deconstruct();
            GameObject.GameCenter.Rendered -= Render;
        }
    }
}