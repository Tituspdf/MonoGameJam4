using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJam4.Engine.Rendering
{
    public class ContentLoader
    {
        public Dictionary<string, Texture2D> Textures;
        public readonly Dictionary<string, SoundEffect> Sounds;
        public SpriteFont ScoreFont { get; private set; }
        
        public ContentLoader()
        {
            Textures = new Dictionary<string, Texture2D>();

            Sounds = new Dictionary<string, SoundEffect>();
        }

        public void LoadContend(ContentManager contentManager)
        {
            ScoreFont = contentManager.Load<SpriteFont>("ScoreFont");
            
            Textures.Add("Player", contentManager.Load<Texture2D>("Triangle"));
            Textures.Add("Square", contentManager.Load<Texture2D>("Square"));
            Textures.Add("Point", contentManager.Load<Texture2D>("Point"));
            Textures.Add("Frame", contentManager.Load<Texture2D>("Frame"));
            Textures.Add("Heart", contentManager.Load<Texture2D>("Heart"));
            Textures.Add("EmptyPoint", contentManager.Load<Texture2D>("EmptyPoint"));
            Textures.Add("BulletImage", contentManager.Load<Texture2D>("BuelletImage"));
            Textures.Add("MonoGameLogo", contentManager.Load<Texture2D>("MonogameLogo"));
            Textures.Add("Logo", contentManager.Load<Texture2D>("Logo"));
            
            Sounds.Add("EnemyExplode", contentManager.Load<SoundEffect>("EnemyExolodeSound"));
            Sounds.Add("LevelUp", contentManager.Load<SoundEffect>("LevelUp"));
            Sounds.Add("Shoot", contentManager.Load<SoundEffect>("Shoot"));
            Sounds.Add("UpgradeRise", contentManager.Load<SoundEffect>("UpgradeRise"));
        }
    }
}