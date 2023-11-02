using ConsoleApp;

namespace Rogal.EngineCore
{
    public sealed class GameLoop
    {
        private readonly GameInit _gameInit;
        private readonly Timer _gameUpdateTimer;
        private readonly ManualResetEvent _gameEndEvent = new ManualResetEvent(false);

        public GameLoop(GameInit gameInit, int frameRate = 100)
        {
            _gameInit = gameInit;
            _gameUpdateTimer = new Timer(GameUpdateCallback, null, 0, frameRate);
            Task.Run(() => _gameInit.Updater.BeginKeyInputAsync());
        }

        private void GameUpdateCallback(object? state)
        {
            if (_gameInit.IsGameOver())
            {
                _gameUpdateTimer.Dispose();
                _gameEndEvent.Set();
                _gameInit.Updater.isGameOver = true;
                return;
            }
            _gameInit.Updater.UpdateAll();
            var key = _gameInit.Updater.GetAndResetLastKey();
            _gameInit.Controller.HandleInput(key);
            _gameInit.Renderer.Draw();
        }

        public void Run()
        {
            _gameEndEvent.WaitOne(); 
        }
    }
}





