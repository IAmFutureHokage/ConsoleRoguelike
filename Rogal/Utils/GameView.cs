using System.Text;
using ConsoleApp;
using Rogal.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rogal.Utils
{
    public class GameView
    {
        private readonly Game game;

        public GameView(Game game)
        {
            this.game = game;
        }

        public void Draw()
        {
            var buffer = new StringBuilder();
            for (int y = 0; y < game.Map.Data.GetLength(1); y++)
            {
                for (int x = 0; x < game.Map.Data.GetLength(0); x++)
                {
                    GameObject gameObject = game.Map.Data[x, y];
                    if (gameObject != null)
                    {
                        gameObject.Update();
                        buffer.Append(gameObject.Renderable.Symbol);
                    }
                    else
                    {
                        buffer.Append(' ');
                    }
                }
                buffer.AppendLine();
            }

            buffer.Append($"Health: {game.Player.Health.Value} ");
            buffer.Append(" Punch: Press 'E'");
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer.ToString());
        }

    }
}
