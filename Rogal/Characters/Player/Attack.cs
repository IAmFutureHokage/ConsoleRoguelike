using Rogal.Components.Base;
using Rogal.Utils;

namespace Rogal.Components
{
    public class Attack : GameObject
    {
        private readonly IMap map;
        private int framesAlive;

        public Attack(int startX, int startY, int prevX, int prevY, IMap map)
        : base(new Transform(startX, startY, 20), DetermineRenderable(startX, startY, prevX, prevY))
        {
            this.map = map;
            framesAlive = 0;
            map.AddGameObject(this);
        }


        private static Renderable DetermineRenderable(int startX, int startY, int prevX, int prevY)
        {
            if (startX > prevX || startX < prevX)
            {
                return new Renderable('/');
            }
            else if (startY > prevY || startY < prevY)
            {
                return new Renderable('\\');
            }
            return new Renderable('/'); // default symbol
        }


        public override void Update()
        {
            base.Update();
            map.RemoveGameObject(this);
        }
    }
}
