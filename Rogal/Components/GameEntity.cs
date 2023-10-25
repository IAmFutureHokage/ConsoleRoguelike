using Rogal.Components.Base;

namespace Rogal.Components
{
    public class GameEntity : GameObject
    {
        public Health Health { get; protected set; }

        public GameEntity(Transform transform, Renderable renderable, Health health)
            : base(transform, renderable)
        {
            Health = health;
        }

        public virtual void TakeDamage(int damageAmount)
        {
            Health.TakeDamage(damageAmount);
            if (Health.Value <= 0)
            {
                // Действия при смерти объекта, если это необходимо
            }
        }
    }
}
