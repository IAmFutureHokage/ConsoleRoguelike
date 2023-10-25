namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(100, 25);
            var gameLoop = new GameLoop(game, 100);
            gameLoop.Run();

            Console.ReadKey();
        }
    }
}

