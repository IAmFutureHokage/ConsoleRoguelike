using Rogal.Components;
using Rogal.Components.Base;
using Rogal.EngineCore;


namespace Rogal.Characters
{
    public sealed class LiveNettle : LivingEntity
    {
        private readonly IMap _map;

        public LiveNettle(IMap map, Vector2 startPosition, char symbol = 'ж', int initialHealth = 15, int speed = 10)
            : base(map, startPosition, symbol, false, initialHealth, speed)
        {
            _map = map;
            _map.MoveGameObject(this, startPosition);
        }

        public override void Update()
        {
            if (_actionCounter <= 0)
            {
                bool attacked = false;
                List<Vector2> freePositions = new List<Vector2>();

                foreach (var direction in new Vector2[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) })
                {
                    Vector2 checkPosition = Position + direction;
                    var gameObject = _map.GetTopGameObjectAt(checkPosition.X, checkPosition.Y);

                    if (gameObject is Player)
                    {
                        Attack(gameObject as Player);
                        attacked = true;
                        break;
                    }
                    else if (_map.IsPositionFree(checkPosition.X, checkPosition.Y))
                    {
                        freePositions.Add(checkPosition);
                    }
                }

                if (!attacked && freePositions.Count > 0)
                {
                    Random random = new Random();
                    Vector2 newPosition = freePositions[random.Next(freePositions.Count)];
                    Move(newPosition - Position);
                }
            }

            base.Update();
        }


        private void Attack(Player player)
        {
            player.TakeDamage(5, Position);
            _actionCounter = Speed;
        }
    }
}
