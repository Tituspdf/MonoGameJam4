using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4.Engine
{
    public class InputManagement : IUpdateable
    {
        public void Update(GameTime gameTime)
        {
        }
    }

    public class Key
    {
        private readonly Keys _key;
        private bool _isPressed;

        public event Action Invoked;

        public Key(Keys key)
        {
            _key = key;
        }

        private void Update(KeyboardState keyboardState)
        {
            bool state = keyboardState.IsKeyDown(_key);

            if (!_isPressed) Invoked?.Invoke();

            _isPressed = state;
        }
    }
}