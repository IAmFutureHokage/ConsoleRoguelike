namespace ConsoleApp
{
    class Program
    {
        private static Game game;
        private static Timer timer;

        static Program()
        {
            timer = new Timer(TimerCallback, null, 0, 100);
            game = new Game(100, 25);
        }
        static void Main(string[] args)
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                game.Update(key);
            }
        }

        static void TimerCallback(Object? o)
        {
            game.Draw();
        }
    }
}

