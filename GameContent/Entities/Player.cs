using Microsoft.Xna.Framework;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Debugging;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Input;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.Entities
{
    public class Player : Actor, IRendering
    {
        public Renderer Renderer { get; set; }
        private InputManagement _input;
        
        public Player(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name, true)
        {
            Renderer = new Renderer(this, "Player");
            _input = gameCenter.InputManagement;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 move = _input.Movement * Time.DeltaTime * 2;
            
            MoveX(move.X, delegate(CollidingObject o) { Debug.Log(o.Name); });
            MoveY(move.Y, delegate(CollidingObject o) { Debug.Log(o.Name); });
        }
    }
}