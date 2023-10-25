using Rogal.Components.Base;

namespace Rogal.Components
{
    public class GameObject
    {
        public Transform Transform { get; private set; }
        public Renderable Renderable { get; private set; }
        public Health Health { get; private set; }

        public GameObject(Transform transform, Renderable renderable, Health? health)
        {
            Transform = transform;
            Renderable = renderable;
            Health = health;
        }
    }
}
