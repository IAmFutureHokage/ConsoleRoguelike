using Rogal.Characters.Player;
using Rogal.Utils;

namespace ConsoleApp
{
    public class Game
    {
        public Player Player { get; private set; }
        public IMap Map { get; private set; }
        public GameController Controller { get; private set; }
        public GameView View { get; private set; }

        public Game(int mapWidth, int mapHeight)
        {
            Map = new Map(mapWidth, mapHeight);
            Player = new Player(1, 1, 'o', 100, 1, Map);
            Controller = new GameController(this);
            View = new GameView(this);
        }

        public bool IsGameOver()
        {
            return Player.Health.Value <= 0;
        }
    }
}


