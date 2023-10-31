using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;

namespace Rogal.Characters.Player
{
    public sealed class Attack : GameObject
    {
        //не надо тут такого
        private readonly IMap _map;

        private int _updateCounter = 0;

        public Attack(Vector2 startPosition, Vector2 previousPosition, IMap map)
            : base(DetermineSymbol(startPosition, previousPosition), startPosition, false, 1)
        {
            if (startPosition.X == previousPosition.X && startPosition.Y == previousPosition.Y)
            {
                return;
            }
            _map = map;

            //ну не ок
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

            if (_updateCounter >= Speed)
            {
                //тоже не фонтан
                _map.RemoveGameObject(this);
            }
        }
    }
}