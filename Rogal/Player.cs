using static ConsoleApp.Program;

namespace ConsoleApp
{
    public class Player
    {
        private Map map;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int PreviousX { get; private set; }
        public int PreviousY { get; private set; }
        public int Health { get; private set; } = 100;
        public int Score { get; private set; } = 0;
        public int LastAttackDirectionX { get; private set; }
        public int LastAttackDirectionY { get; private set; }
        public Attack? CurrentAttack { get; private set; }

        public Player(int x, int y, Map map)
        {
            X = PreviousX = x;
            Y = PreviousY = y;
            LastAttackDirectionX = 0;
            LastAttackDirectionY = 0;
            this.map = map;
        }
        public void Attack()
        {
            if (CurrentAttack == null)
            {
                int attackDirectionX = (X - PreviousX);
                int attackDirectionY = (Y - PreviousY);
                char attackSymbol;

                if (attackDirectionX == 0 && attackDirectionY == 0)
                {
                    attackDirectionX = LastAttackDirectionX;
                    attackDirectionY = LastAttackDirectionY;
                }
                else
                {
                    LastAttackDirectionX = attackDirectionX;
                    LastAttackDirectionY = attackDirectionY;
                }

                if (attackDirectionX > 0)
                    attackSymbol = 'D';
                else if (attackDirectionX < 0)
                    attackSymbol = 'C';
                else
                    attackSymbol = '\\';

                int attackX = X + attackDirectionX;
                int attackY = Y + attackDirectionY;
                CurrentAttack = new Attack(attackX, attackY, attackSymbol);
            }
        }
        public void Update()
        {
            if (CurrentAttack != null && !CurrentAttack.Update())
            {
                CurrentAttack = null;
            }
        }
        public void Move(int x, int y)
        {
            if (map.IsWalkable(x, y) && (x != X || y != Y))
            {
                PreviousX = X;
                PreviousY = Y;
                X = x;
                Y = y;
                LastAttackDirectionX = X - PreviousX;
                LastAttackDirectionY = Y - PreviousY;
            }
        }

    }
}
