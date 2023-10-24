namespace ConsoleApp
{
    public class Attack
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Frame { get; private set; }
        public char Symbol { get; }  

        public Attack(int x, int y, char symbol) 
        {
            X = x;
            Y = y;
            Frame = 0;
            Symbol = symbol;
        }

        public bool Update()
        {
            Frame++;
            return Frame < 4;
        }
    }
}