using ConsoleApp;
using Rogal.Characters;
using System.Numerics;

namespace Rogal.EngineCore
{
    public sealed class GameLoop
    {
        private readonly Timer _gameUpdateTimer;
        private readonly ManualResetEvent _gameEndEvent = new ManualResetEvent(false);
        private readonly GameController _controller;
        private readonly GameRenderer _renderer;
        private readonly GameUpdater _updater;
        private readonly Player _player;
        public bool isGameOver = false;

        public GameLoop(GameController controller, GameRenderer renderer, GameUpdater updater, Player player, int frameRate = 100)
        {
            _controller = controller;
            _renderer = renderer;
            _updater = updater;
            _player = player;
            _player.PlayerDied += OnPlayerDied;
            _gameUpdateTimer = new Timer(GameUpdateCallback, null, 0, frameRate);
            Task.Run(() => _updater.BeginKeyInputAsync());
        }
        private void OnPlayerDied()
        {
            isGameOver = true;
        }

        private void GameUpdateCallback(object? state)
        {
            if (isGameOver) 
            {
                _gameUpdateTimer.Dispose();
                _gameEndEvent.Set();
                _updater.isGameOver = true;
                _player.PlayerDied -= OnPlayerDied;
                Console.WriteLine("\nGood game well played");
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





