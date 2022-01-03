using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using IUpdateable = MonoGameJam4.Engine.Interfaces.IUpdateable;

namespace MonoGameJam4.Engine.Input
{
    public class InputManagement : IUpdateable
    {
        private readonly Dictionary<Keys, Key> _callbacks;

        public InputManagement()
        {
            _callbacks = new Dictionary<Keys, Key>();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (var callback in _callbacks)
            {
                callback.Value.Update(keyboardState);
            }
        }

        public Key GetCallback(Keys keys)
        {
            if (!_callbacks.ContainsKey(keys))
            {
                _callbacks.Add(keys, new Key(keys));
            }

            return _callbacks[keys];
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

        public void Update(KeyboardState keyboardState)
        {
            bool state = keyboardState.IsKeyDown(_key);

            if (!_isPressed) Invoked?.Invoke();

            _isPressed = state;
        }
    }
}