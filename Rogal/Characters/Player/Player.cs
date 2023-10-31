using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

public sealed class Player : LivingEntity
{
    private readonly IMap _map;
    private int actionCounter;

    public Player(IMap map, Vector2 startPosition, char symbol = 'o', int initialHealth = 100, int speed = 1)
        : base(startPosition, symbol, false, initialHealth, speed)
    {
        _map = map;
        _map.MoveGameObject(this, startPosition);
        actionCounter = 0;
    }

    public override void Update()
    {
        base.Update();
        if (actionCounter > 0) actionCounter--;
    }

    public void Move(Vector2 direction)
    {
        if (actionCounter > 0) return;

        var newPosition = Position + direction;

        if (_map.IsPositionFree(newPosition.X, newPosition.Y))
        {
            _map.MoveGameObject(this, newPosition);
            actionCounter = Speed;
        }
    }

    public void Attack()
    {
        if (actionCounter > 0) return;

        var attackDirection = Position - PreviousPosition;
        var attackPosition = Position + attackDirection;

        var objAtAttackPos = _map.GetTopGameObjectAt(attackPosition.X, attackPosition.Y);
        if (objAtAttackPos == null || objAtAttackPos is LivingEntity)
        {
            new Attack(attackPosition, Position, _map);
            actionCounter = Speed;
        }
    }
}

