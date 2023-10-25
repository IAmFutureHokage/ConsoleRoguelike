using Rogal.Components.Base;

namespace Rogal.Components
{
    public class Wall : GameObject
    {
        public Wall(Transform transform, Renderable renderable, ImmutableHealth health)
            : base(transform, renderable, health)
        {
        }
    }
}

