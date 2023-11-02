namespace Rogal.EngineCore
{
    public class GameUpdater
    {
        private readonly IMap _map;
        private ConsoleKey _lastKey = ConsoleKey.NoName;

        public bool isGameOver = false;

        public GameUpdater(IMap map)
        {
            _map = map;
        }

        public void UpdateAll()
        {
            for (int y = 0; y < _map.GetHeight(); y++)
            {
                for (int x = 0; x < _map.GetWidth(); x++)
                {
                    var gameObject = _map.GetTopGameObjectAt(x, y);
                    gameObject?.Update();
                }
            }
        }

        public async Task BeginKeyInputAsync()
        {
            while (!isGameOver)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = await Task.Run(() => Console.ReadKey(intercept: true));
                    _lastKey = keyInfo.Key;
                }
                await Task.Delay(10);
            }
        }

        public ConsoleKey GetAndResetLastKey()
        {
            var key = _lastKey;
            _lastKey = ConsoleKey.NoName;
            return key;
        }
    }
}






