using ConsoleApp;

namespace Rogal.EngineCore
{
    public sealed class GameLoop
    {
        private readonly GameInit _gameInit;
        private readonly Timer _gameUpdateTimer;
        private ConsoleKey _lastKey;
        private readonly ManualResetEvent _gameEnded = new(false);

        public GameLoop(GameInit gameInit, int frameRate = 100)
        {
            _gameInit = gameInit;
            _gameUpdateTimer = new Timer(GameUpdateCallback, null, 0, frameRate);
            BeginKeyInput();
            _gameEnded.WaitOne();
        }

        private void GameUpdateCallback(object? state)
        {
            if (_gameInit.IsGameOver())
            {
                _gameUpdateTimer.Dispose();
                _gameEnded.Set();
                return;
            }

            _gameInit.Updater.UpdateAll();
            _gameInit.Controller.HandleInput(_lastKey);
            _gameInit.Renderer.Draw();
            _lastKey = ConsoleKey.NoName;
        }

    // Переделать
        private void BeginKeyInput()
        {
            Task.Run(() =>
            {
                var keyInfo = Console.ReadKey(intercept: true);
                _lastKey = keyInfo.Key;
                if (!_gameInit.IsGameOver())
                    BeginKeyInput();
            });
        }
    }
}