using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Characters
{
    public sealed class Attack : GameObject
    {
        private readonly IMap _map;

        private int _updateCounter = 0;
        private Vector2 _prevPosition;

        public Attack(Vector2 startPosition, Vector2 previousPosition, IMap map)
            : base(DetermineSymbol(startPosition, previousPosition), startPosition, false, 1)
        {
            _prevPosition = previousPosition;
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

            Stack<GameObject> stackAtPosition = _map.Data[Position.X, Position.Y];

            foreach (var gameObject in stackAtPosition)
            {
                if (gameObject is Bullet bullet) 
                {
                    _map.RemoveGameObject(bullet);
                    break;
                }
                if (gameObject is LivingEntity livingEntity)
                {
                    livingEntity.TakeDamage(5, _prevPosition);
                    break;
                }
            }

            if (_updateCounter >= Speed)
            {
                _map.RemoveGameObject(this);
            }
        }
    }
}