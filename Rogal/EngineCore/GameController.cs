using ConsoleApp;
using Rogal.Components.Base;

namespace Rogal.EngineCore
{
    public sealed class GameController
    {
        private readonly GameInit _gameInit;

        public GameController(GameInit gameInit)
        {
            _gameInit = gameInit;
        }

        private enum Actions
        {
            Up,
            Left,
            Down,
            Right,
            Attack,
            None
        }

        private static Actions GetActionsFromKey(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.W => Actions.Up,
                ConsoleKey.A => Actions.Left,
                ConsoleKey.S => Actions.Down,
                ConsoleKey.D => Actions.Right,
                ConsoleKey.E => Actions.Attack,
                _ => Actions.None
            };
        }

        public void HandleInput(ConsoleKey key)
        {
            var action = GetActionsFromKey(key);
            var moveAction = ConvertToVector2Direction(action);
            _gameInit.Player.Move(moveAction);
        }

        private Vector2 ConvertToVector2Direction(Actions action)
        {
            switch (action)
            {
                case Actions.Up:
                    return new Vector2(0, -1);
                case Actions.Left:
                    return new Vector2(-1, 0);
                case Actions.Down:
                    return new Vector2(0, 1);
                case Actions.Right:
                    return new Vector2(1, 0);
                case Actions.Attack:
                    _gameInit.Player.Attack();
                    return new Vector2(0, 0);
                case Actions.None:
                    return new Vector2(0, 0);
                default:
                    return new Vector2(0, 0);
            }
        }
    }
}