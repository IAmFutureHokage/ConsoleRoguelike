using System.Text;

namespace ConsoleApp
{
    public class Game
    {
        public Player Player { get; private set; }
        public Map Map { get; private set; }
        private DateTime lastMoveTime;

        public Game(int mapWidth, int mapHeight)
        {
            Map = new Map(mapWidth, mapHeight);
            Player = new Player(2, 2, Map);
        }

        public void Update(ConsoleKey key)
        {
            var now = DateTime.Now;
            if ((now - lastMoveTime).TotalMilliseconds < 50)
                return;

            int newX = Player.X;
            int newY = Player.Y;

            switch (key)
            {
                case ConsoleKey.W:
                    if (Player.Y > 0) newY = Player.Y - 1;
                    break;
                case ConsoleKey.A:
                    if (Player.X > 0) newX = Player.X - 1;
                    break;
                case ConsoleKey.S:
                    if (Player.Y < Map.Data.GetLength(1) - 1) newY = Player.Y + 1;
                    break;
                case ConsoleKey.D:
                    if (Player.X < Map.Data.GetLength(0) - 1) newX = Player.X + 1;
                    break;
                case ConsoleKey.E:
                    Player.Attack();
                    break;
            }
            if (Map.IsWalkable(newX, newY))
            {
                Player.Move(newX, newY);
                lastMoveTime = now;
            }
            else if (key == ConsoleKey.E)
            {
                Player.Attack();
            }

            Player.Update();
        }
        public void Draw()
        {
            Player.Update();
            var buffer = new StringBuilder();
            for (int y = 0; y < Map.Data.GetLength(1); y++)
            {
                for (int x = 0; x < Map.Data.GetLength(0); x++)
                {
                    if (x == Player.X && y == Player.Y)
                    {
                        buffer.Append('o');
                    }
                    else if (Player.CurrentAttack != null && x == Player.CurrentAttack.X && y == Player.CurrentAttack.Y)
                    {
                        buffer.Append(Player.CurrentAttack.Symbol);
                    }
                    else
                    {
                        buffer.Append(Map.Data[x, y]);
                    }
                }
                buffer.AppendLine();
            }

            buffer.AppendLine($"Health: {Player.Health}   Score: {Player.Score}");
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer.ToString());
        }

    }
}
