using Rogal.Components.Base;
using Rogal.Utils;

namespace Rogal.Components
{
    public class Attack : GameObject
    {
        private readonly IMap map;
        private int framesAlive;

        public Attack(int startX, int startY, IMap map)
            : base(new Transform(startX, startY, 1), DetermineRenderable(startX, startY))
        {
            this.map = map;
            this.framesAlive = 0;

            GameObject objAtAttackPos = map.GetGameObjectAt(Transform.X, Transform.Y);
            if (!(objAtAttackPos is GameEntity))
                map.AddGameObject(this);
        }

        private static Renderable DetermineRenderable(int startX, int startY)
        {
            return new Renderable('/');
        }

        public override void Update()
        {
            base.Update();
            map.RemoveGameObject(this);
        }
    }
}
