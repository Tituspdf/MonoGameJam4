﻿using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Input;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Player : GameObject, IRendering
    {
        public Renderer Renderer { get; set; }
        private InputManagement _input;
        
        public Player(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            Renderer = new Renderer(this, "Player");
            _input = gameCenter.InputManagement;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 movement = _input.Movement;
            Transform.Position += movement * Time.DeltaTime * 2;
        }
    }
}