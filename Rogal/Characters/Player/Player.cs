using Rogal.Components.Base;
using Rogal.Components;
using Rogal.Utils;

public class Player : GameEntity
{
    private readonly IMap map;

    public Player(int x, int y, char symbol, int initialHealth, int speed, IMap map)
        : base(new Transform(x, y, speed), new Renderable(symbol), new Health(initialHealth))
    {
        this.map = map;
        map.MoveGameObject(this, x, y);
    }

    public void Attack(IMap map)
    {
        int attackX = Transform.X;
        int attackY = Transform.Y;

        if (Transform.X > Transform.PreviousX)
        {
            attackX++;
        }
        else if (Transform.X < Transform.PreviousX)
        {
            attackX--;
        }
        else if (Transform.Y > Transform.PreviousY)
        {
            attackY++;
        }
        else if (Transform.Y < Transform.PreviousY)
        {
            attackY--;
        }

        GameObject objAtAttackPos = map.GetGameObjectAt(attackX, attackY);
        if (objAtAttackPos == null || objAtAttackPos is GameEntity)
        {
            var attack = new Attack(attackX, attackY, map);
        }
    }
}

