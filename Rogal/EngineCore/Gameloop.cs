using ConsoleApp;

namespace Rogal.EngineCore
{
    public sealed class GameLoop
    {
        private readonly Timer _gameUpdateTimer;
        private readonly ManualResetEvent _gameEndEvent = new ManualResetEvent(false);
        private readonly GameController _controller;
        private readonly GameRenderer _renderer;
        private readonly GameUpdater _updater;

        public GameLoop(GameController controller, GameRenderer renderer, GameUpdater updater, int frameRate = 100)
        {
            _controller = controller;
            _renderer = renderer;
            _updater = updater;
            _gameUpdateTimer = new Timer(GameUpdateCallback, null, 0, frameRate);
            Task.Run(() => _updater.BeginKeyInputAsync());
        }

        private void GameUpdateCallback(object? state)
        {
            if (false) 
            {
                _gameUpdateTimer.Dispose();
                _gameEndEvent.Set();
                _updater.isGameOver = true;
                return;
            }
            _updater.UpdateAll();
            var key = _updater.GetAndResetLastKey();
            _controller.HandleInput(key);
            _renderer.Draw();
        }

        public void Run()
        {
            _gameEndEvent.WaitOne(); 
        }
    }
}





