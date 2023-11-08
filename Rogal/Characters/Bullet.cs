using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Characters
{
    public sealed class Bullet : GameObject
    {
        private readonly IMap _map;
        private readonly Vector2 _direction;
        private int _updateCounter;
        private readonly int _speed;

        public Bullet(Vector2 startPosition, Vector2 direction, IMap map, int speed = 2)
            : base(GetSymbolForDirection(direction), startPosition, false, 1)
        {
            _direction = direction;
            _map = map;
            _speed = speed;
            _map.AddGameObject(this);
        }

        private static char GetSymbolForDirection(Vector2 direction) =>
            direction.X != 0 ? '-' : '|';

        public override void Update()
        {
            if (_updateCounter <= 0)
            {
                Vector2 newPosition = Position + _direction;
                if (_map.IsPositionFree(newPosition.X, newPosition.Y))
                {
                    Move(newPosition);
                }
                else
                {
                    HandleCollision(newPosition);
                }
                _updateCounter = _speed;
            }
            else
            {
                _updateCounter--;
            }
        }

        private void Move(Vector2 newPosition)
        {
            _map.MoveGameObject(this, newPosition);
        }

        private void HandleCollision(Vector2 newPosition)
        {
            GameObject topObject = _map.GetTopGameObjectAt(newPosition.X, newPosition.Y);
            if (topObject is Player player)
            {
                player.TakeDamage(5, Position);
            }
            _map.RemoveGameObject(this);
        }
    }
}

