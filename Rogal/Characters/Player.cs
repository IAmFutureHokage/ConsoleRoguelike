using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Characters
{
    public sealed class Player : LivingEntity
    {
        private readonly IMap _map;
        public event Action PlayerDied;

        public Player(IMap map, Vector2 startPosition, char symbol = '♦', int initialHealth = 100, int speed = 1)
            : base(map, startPosition, symbol, false, initialHealth, speed)
        {
            _map = map;
            _map.MoveGameObject(this, startPosition);
        }

        public override void Update()
        {
            base.Update();
            _map.Finished(this, Position);
        }

        public void Attack()
        {
            if (_actionCounter > 0) return;

            var attackDirection = Position - PreviousPosition;
            var attackPosition = Position + attackDirection;

            var objAtAttackPos = _map.GetTopGameObjectAt(attackPosition.X, attackPosition.Y);
            if (objAtAttackPos == null || objAtAttackPos is LivingEntity)
            {
                _ = new Attack(attackPosition, Position, _map);
                _actionCounter = Speed;
            }
        }

        public override void TakeDamage(int damageAmount, Vector2 attackerPosition)
        {
            Health -= damageAmount;
            if (Health <= 0)
            {
                Health = 0;
                PlayerDied?.Invoke();
                _map.RemoveGameObject(this);
            }
            else
            {
                _actionCounter = 0;

                var knockbackDirection = new Vector2(
                    Position.X - attackerPosition.X,
                    Position.Y - attackerPosition.Y
                );

                knockbackDirection = new Vector2(
                    knockbackDirection.X != 0 ? Math.Sign(knockbackDirection.X) : 0,
                    knockbackDirection.Y != 0 ? Math.Sign(knockbackDirection.Y) : 0
                );

                Move(knockbackDirection);
            }
        }
    }
}
