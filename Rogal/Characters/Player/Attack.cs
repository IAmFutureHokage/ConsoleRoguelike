using Rogal.Components.Base;
using Rogal.EngineCore;

public sealed class Attack : GameObject
{
    private readonly IMap _map;
    private int _updateCounter = 0;

    public Attack(Vector2 startPosition, Vector2 previousPosition, IMap map)
        : base(DetermineSymbol(startPosition, previousPosition), startPosition, false, 1)
    {
        if (startPosition.X == previousPosition.X && startPosition.Y == previousPosition.Y)
        {
            return;
        }
        _map = map;
        _map.AddGameObject(this);
    }

    private static char DetermineSymbol(Vector2 startPosition, Vector2 previousPosition)
    {
        return startPosition.X != previousPosition.X ? '/' :
               startPosition.Y != previousPosition.Y ? '\\' : '/';
    }

    public override void Update()
    {
        _updateCounter++;

        if (_updateCounter >= Speed)
        {
            _map.RemoveGameObject(this);
        }
    }
}
