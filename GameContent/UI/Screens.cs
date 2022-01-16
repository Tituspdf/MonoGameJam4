using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJam4.Engine;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Interfaces;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;

namespace MonoGameJam4.GameContent.UI
{
    public class Screens : GameObject, IRenderCall
    {
        public enum GameState
        {
            MainMenu,
            Death,
            Play,
        }

        private GameState _state;

        private SpriteFont _fontNormal;

        public Screens(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            _state = GameState.MainMenu;
            _fontNormal = gameCenter.ContentLoader.ScoreFont;
            Time.Scale = 0;
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            switch (_state)
            {
                case GameState.MainMenu:
                {
                    string text = "Press 'SPACE' to play!";
                    Vector2 size = _fontNormal.MeasureString(text);
                    spriteBatch.DrawString(_fontNormal, text, new Vector2(50), Color.White, 0, Vector2.Zero,
                        Vector2.One, SpriteEffects.None, 1);
                    break;
                }
                case GameState.Death:
                    break;
                case GameState.Play:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}