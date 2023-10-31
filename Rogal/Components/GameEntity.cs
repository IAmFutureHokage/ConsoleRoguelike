using Rogal.Components.Base;

namespace Rogal.Components
{
    public class LivingEntity : GameObject
    {
        public int Health { get; protected set; }

        public LivingEntity(Vector2 position, char symbol = '0', bool isPassable = false, int health = 100, int speed = 1)
            : base(symbol, position, isPassable, speed)
        {
            Health = health;
        }

        public virtual void TakeDamage(int damageAmount)
        {
            Health -= damageAmount;
            if (Health <= 0)
            {
                Health = 0;
            }
        }

    }
}

