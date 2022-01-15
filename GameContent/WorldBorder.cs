using Microsoft.Xna.Framework;
using MonoGameJam4.Engine.Entities;
using MonoGameJam4.Engine.Rendering;
using MonoGameJam4.Engine.WorldSpace;
using MonoGameJam4.GameContent.Entities;

namespace MonoGameJam4.GameContent
{
    public class WorldBorder : GameObject
    {
        public WorldBorder(GameCenter gameCenter, Transform transform, string name) : base(gameCenter, transform, name)
        {
            const float thickness = 0.5f;
            const string objectName = "Wall";
            Camera camera = gameCenter.Camera;
            Vector2 screenSize = gameCenter.GameWindow.ScreenSize;
            // left border
            GameCenter.GameObjects.Add(new Box(gameCenter,
                new Transform(camera.ScreenToWorldPosition(new Vector2(0, screenSize.Y / 2)),
                    new Vector2(thickness, camera.ScreenToWorldDistance(screenSize.Y)), 0), objectName, true, "Square"));
            
            // right border
            GameCenter.GameObjects.Add(new Box(gameCenter,
                new Transform(camera.ScreenToWorldPosition(new Vector2(screenSize.X, screenSize.Y / 2)),
                    new Vector2(thickness, camera.ScreenToWorldDistance(screenSize.Y)), 0), objectName, true, "Square"));
            
            // top border
            GameCenter.GameObjects.Add(new Box(gameCenter,
                new Transform(camera.ScreenToWorldPosition(new Vector2(screenSize.X / 2, 0)),
                    new Vector2(camera.ScreenToWorldDistance(screenSize.X), thickness), 0), objectName, true, "Square"));
            
            // bottom border
            GameCenter.GameObjects.Add(new Box(gameCenter,
                new Transform(camera.ScreenToWorldPosition(new Vector2(screenSize.X / 2, screenSize.Y)),
                    new Vector2(camera.ScreenToWorldDistance(screenSize.X), thickness), 0), objectName, true, "Square"));
        }
    }
}