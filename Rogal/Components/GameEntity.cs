﻿using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Components
{
    public class LivingEntity : GameObject
    {
        public int Health { get; protected set; }
        protected int _actionCounter;
        private readonly IMap _map;

        public LivingEntity(IMap map, Vector2 position, char symbol = 'Ж', bool isPassable = false, int health = 100, int speed = 1)
            : base(symbol, position, isPassable, speed)
        {
            Health = health;
            _map = map;
            _actionCounter = 0;
        }

        public override void Update()
        {
            base.Update();
            if (_actionCounter > 0) _actionCounter--;
        }

        public virtual void Move(Vector2 direction)
        {
            if (_actionCounter > 0) return;

            var newPosition = Position + direction;
            if (_map.IsPositionFree(newPosition.X, newPosition.Y))
            {
                _map.MoveGameObject(this, newPosition);
            }
            _actionCounter = Speed;
        }

        public virtual void TakeDamage(int damageAmount, Vector2 attackerPosition)
        {
            Health -= damageAmount;
            if (Health <= 0)
            {
                Health = 0;
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


