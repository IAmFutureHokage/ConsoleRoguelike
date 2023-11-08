using Rogal.Characters;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace ConsoleApp
{
    sealed class Program
    {
        static void Main()
        {
            int mapWidth = 100;
            int mapHeight = 25;
            int frameRate = 100;

            var map = new Map(mapWidth, mapHeight);
            var player = new Player(map, new Vector2(1, 1));
            var updater = new GameUpdater(map);
            var controller = new GameController(player);
            var renderer = new GameRenderer(player, map);
            var gameLoop = new GameLoop(controller, renderer, updater, player, frameRate);

            gameLoop.Run();
        }
    }
}
