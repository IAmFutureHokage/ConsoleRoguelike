using ConsoleApp;

public class GameLoop
{
    private readonly Game game;
    private readonly Timer timer;
    private ConsoleKey lastKey;

    public GameLoop(Game game, int updateIntervalMilliseconds)
    {
        this.game = game;
        timer = new Timer(TimerCallback, null, 0, updateIntervalMilliseconds);
    }

    private void TimerCallback(object state)
    {
        game.Controller.HandleInput(lastKey);  
        game.View.Draw();
        lastKey = ConsoleKey.NoName;
    }

    public void Run()
    {
        while (!game.IsGameOver())
        {
            var key = Console.ReadKey(true).Key;
            lastKey = key;
        }
    }
}





