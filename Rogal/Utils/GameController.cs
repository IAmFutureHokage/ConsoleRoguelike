using ConsoleApp;

public class GameController
{
    private readonly Game game;

    public GameController(Game game)
    {
        this.game = game;
    }

    public void HandleInput(ConsoleKey key)
    {
        int newX = game.Player.Transform.X;
        int newY = game.Player.Transform.Y;

        switch (key)
        {
            case ConsoleKey.W:
                if (game.Player.Transform.Y > 0) newY = game.Player.Transform.Y - 1;
                break;
            case ConsoleKey.A:
                if (game.Player.Transform.X > 0) newX = game.Player.Transform.X - 1;
                break;
            case ConsoleKey.S:
                if (game.Player.Transform.Y < game.Map.Data.GetLength(1) - 1) newY = game.Player.Transform.Y + 1;
                break;
            case ConsoleKey.D:
                if (game.Player.Transform.X < game.Map.Data.GetLength(0) - 1) newX = game.Player.Transform.X + 1;
                break;
            case ConsoleKey.E:
                game.Player.Attack(game.Map);
                break;
        }

        game.Player.Transform.IncreaseFrameCounter();

        if (game.Map.IsPositionFree(newX, newY) && game.Player.Transform.CanMove())
        {
            game.Player.Transform.Update(newX, newY);
            game.Map.MoveGameObject(game.Player, newX, newY);
        }
    }
}




