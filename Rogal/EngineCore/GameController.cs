using ConsoleApp;
using Rogal.Components.Base;

public sealed class GameController
{
    private readonly GameInit _gameInit;

    public GameController(GameInit gameInit)
    {
        _gameInit = gameInit;
    }

    private enum Direction
    {
        Up,
        Left,
        Down,
        Right,
        Attack,
        None
    }

    private Direction GetDirectionFromKey(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.W => Direction.Up,
            ConsoleKey.A => Direction.Left,
            ConsoleKey.S => Direction.Down,
            ConsoleKey.D => Direction.Right,
            ConsoleKey.E => Direction.Attack,
            _ => Direction.None
        };
    }

    public void HandleInput(ConsoleKey key)
    {
        var direction = GetDirectionFromKey(key);

        if (direction == Direction.Attack)
        {
            _gameInit.Player.Attack();
            return;
        }

        var moveDirection = ConvertToVector2Direction(direction);
        _gameInit.Player.Move(moveDirection);
    }

    private Vector2 ConvertToVector2Direction(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector2(0, -1);
            case Direction.Left:
                return new Vector2(-1, 0);
            case Direction.Down:
                return new Vector2(0, 1);
            case Direction.Right:
                return new Vector2(1, 0);
            default:
                return new Vector2(0, 0);
        }
    }
}





