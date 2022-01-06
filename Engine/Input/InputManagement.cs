using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameJam4.Engine.Input
{
    public class InputManagement : EngineObject
    {
        private readonly Dictionary<Keys, Key> _callbacks;

        public Vector2 Movement { get; private set; }

        public InputManagement()
        {
            _callbacks = new Dictionary<Keys, Key>();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            CalculateMovement(keyboardState);

            foreach (Key callback in _callbacks.Values.ToArray())
            {
                callback.Update(keyboardState);
            }
        }

        private void CalculateMovement(KeyboardState keyboardState)
        {
            Vector2 movement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W)) movement.Y += 1;
            if (keyboardState.IsKeyDown(Keys.S)) movement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.A)) movement.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D)) movement.X += 1;

            Movement = movement;
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

            if (!_isPressed && state) Invoked?.Invoke();

            _isPressed = state;
        }
    }
}