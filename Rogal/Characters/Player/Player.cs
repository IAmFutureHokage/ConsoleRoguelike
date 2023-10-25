using Rogal.Components.Base;
using Rogal.Components;
using Rogal.Utils;

namespace Rogal.Characters.Player
{
    public class Player : GameObject
    {
        public int Score { get; private set; }

        public Player(int x, int y, char symbol, int initialHealth, int speed, IMap map)
        : base(new Transform(x, y, speed), new Renderable(symbol), new Health(initialHealth))
        {
            map.MoveGameObject(this, x, y);
        }
    }
}
