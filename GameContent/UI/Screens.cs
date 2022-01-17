using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public GameState State;

        private SpriteFont _fontNormal;
        private SpriteFont _headFont;
        private Texture2D _squareTexture;
        private readonly Texture2D _monogameLogo;
        private readonly Texture2D _logo;

        public Screens(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            State = GameState.MainMenu;
            _fontNormal = gameCenter.ContentLoader.ScoreFont;
            _squareTexture = gameCenter.ContentLoader.Textures["Square"];
            _headFont = GameCenter.Content.Load<SpriteFont>("Header");
            Time.Scale = 0;

            _monogameLogo = gameCenter.ContentLoader.Textures["MonoGameLogo"];
            _logo = gameCenter.ContentLoader.Textures["Logo"];

            gameCenter.InputManagement.GetCallback(Keys.Space).Invoked += OnSpace;
        }

        private void OnSpace()
        {
            switch (State)
            {
                case GameState.MainMenu:
                {
                    State = GameState.Play;
                    Time.Scale = 1;
                    break;
                }
                case GameState.Death:
                {
                    State = GameState.Play;
                    Time.Scale = 1;
                    break;
                }
                case GameState.Play:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Render(SpriteBatch spriteBatch, Camera camera, Window gameWindow)
        {
            switch (State)
            {
                case GameState.MainMenu:
                {
                    spriteBatch.Draw(_squareTexture, new Rectangle(Point.Zero, gameWindow.ScreenSize.ToPoint()), null,
                        Color.Black, 0, Vector2.Zero, SpriteEffects.None, 0.9f);

                    string headline = "spaceangle";
                    Vector2 headerSize = _fontNormal.MeasureString(headline);
                    spriteBatch.DrawString(_fontNormal, headline, new Vector2(50, 25), Color.White, 0, Vector2.Zero,
                        Vector2.One * 2.5f, SpriteEffects.None, 1);

                    string text = "Press 'SPACE' to play!";
                    Vector2 size = _fontNormal.MeasureString(text);
                    spriteBatch.DrawString(_fontNormal, text, new Vector2(50, 400), Color.White, 0, Vector2.Zero,
                        Vector2.One, SpriteEffects.None, 1);

                    spriteBatch.Draw(_logo, new Rectangle(new Point(900, 100), new Point(200)), null, Color.White, 0,
                        Vector2.Zero, SpriteEffects.None, 1);
                    
                    spriteBatch.Draw(_monogameLogo, new Rectangle(new Point(900, 400), new Point(200)), null, Color.White, 0,
                        Vector2.Zero, SpriteEffects.None, 1);
                    break;
                }
                case GameState.Death:
                {
                    spriteBatch.Draw(_squareTexture, new Rectangle(gameWindow.ScreenMiddlePoint.ToPoint(), gameWindow.ScreenSize.ToPoint()), null,
                        Color.Black, 0, _squareTexture.Bounds.Center.ToVector2(), SpriteEffects.None, 0.9f);

                    string text = "Press 'SPACE' to try again!";
                    Vector2 size = _fontNormal.MeasureString(text);
                    spriteBatch.DrawString(_fontNormal, text, gameWindow.ScreenMiddlePoint, Color.White, 0, size / 2,
                        Vector2.One, SpriteEffects.None, 1);
                    
                    text = "You are Dead :O";
                    size = _fontNormal.MeasureString(text);
                    spriteBatch.DrawString(_fontNormal, text, gameWindow.ScreenMiddlePoint - new Vector2(0, 200), Color.White, 0, size / 2,
                        Vector2.One * 2, SpriteEffects.None, 1);
                    break;
                }
                case GameState.Play:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}