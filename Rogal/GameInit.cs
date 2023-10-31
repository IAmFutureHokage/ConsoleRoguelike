using Rogal.Characters.Player;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace ConsoleApp
{
    public sealed class GameInit
    {
        public Player Player { get; private set; }
        public IMap Map { get; private set; }
        public GameController Controller { get; private set; }
        public GameRenderer Renderer { get; private set; }
        public GameUpdater Updater { get; private set; }
       
        private readonly int _frameRate;

        public GameInit(int mapWidth = 100, int mapHeight = 30, int frameRate = 100)
        {
            _frameRate = frameRate;

            Map = new Map(mapWidth, mapHeight);
            Player = new Player(Map, new Vector2(1, 1));
            Updater = new GameUpdater(Map);

            //не ок
            Controller = new GameController(this);
            Renderer = new GameRenderer(this);
        }

        public void Run()
        {
            _ = new GameLoop(this, _frameRate);
        }

        public bool IsGameOver() => Player.Health <= 0;
    }
}
