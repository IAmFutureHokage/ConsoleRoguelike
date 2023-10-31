using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Characters.Player
{
    public sealed class Player : LivingEntity
    {
        private readonly IMap _map;
        private int _actionCounter;

        public Player(IMap map, Vector2 startPosition, char symbol = 'o', int initialHealth = 100, int speed = 1)
            : base(startPosition, symbol, false, initialHealth, speed)
        {
            _map = map;
            _map.MoveGameObject(this, startPosition);
            _actionCounter = 0;
        }

        public override void Update()
        {
            base.Update();
            if (_actionCounter > 0) _actionCounter--;
        }

        public void Move(Vector2 direction)
        {
            if (_actionCounter > 0) return;

            var newPosition = Position + direction;

            _map.MoveGameObject(this, newPosition);
            _actionCounter = Speed;
            
        }

        public void Attack()
        {
            if (_actionCounter > 0) return;

            var attackDirection = Position - PreviousPosition;
            var attackPosition = Position + attackDirection;

            var objAtAttackPos = _map.GetTopGameObjectAt(attackPosition.X, attackPosition.Y);
            if (objAtAttackPos == null || objAtAttackPos is LivingEntity)
            {
                //ну тут тоже не ок, через мапу создавать, а не передавать в атаку мапу
                new Attack(attackPosition, Position, _map);
                _actionCounter = Speed;
            }
        }
    }
}