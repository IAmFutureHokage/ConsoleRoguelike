using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Characters
{
    public sealed class Archer : LivingEntity
    {
        private readonly IMap _map;
        private static readonly Random _random = new Random();

        public Archer(IMap map, Vector2 startPosition, char symbol = 'A', int initialHealth = 10, int speed = 10)
            : base(map, startPosition, symbol, false, initialHealth, speed)
        {
            _map = map;
            _map.MoveGameObject(this, startPosition);
        }

        public override void Update()
        {
            if (_actionCounter <= 0)
            {
                if (!TryAttack())
                {
                    MoveToRandomFreeAdjacentPosition();
                }
                _actionCounter = Speed;
            }
            base.Update();
        }

        private bool TryAttack()
        {
            var directions = new[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };
            foreach (var direction in directions)
            {
                if (ShootArrowIfPlayerInSight(direction))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ShootArrowIfPlayerInSight(Vector2 direction)
        {
            var checkPos = Position + direction;
            while (true)
            {
                var gameObject = _map.GetTopGameObjectAt(checkPos.X, checkPos.Y);
                if (gameObject != null)
                {
                    if (gameObject is Player)
                    {
                        _ = new Bullet(Position, direction, _map);
                        return true;
                    }
                    break;
                }
                checkPos += direction;
            }
            return false;
        }

        private void MoveToRandomFreeAdjacentPosition()
        {
            Vector2? newPosition = new[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) }
                .Select(direction => Position + direction)
                .Where(newPos => _map.IsPositionFree(newPos.X, newPos.Y))
                .OrderBy(x => _random.Next())
                .FirstOrDefault();

            if (newPosition.HasValue)
                Move(newPosition.Value - Position);
        }
    }
}
