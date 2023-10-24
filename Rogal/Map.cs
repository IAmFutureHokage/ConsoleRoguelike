namespace ConsoleApp
{
    public class Map
    {
        public char[,] Data { get; private set; }

        public Map(int width, int height)
        {
            Data = new char[width, height];
            Initialize();
        }

        private void Initialize()
        {
            for (int y = 0; y < Data.GetLength(1); y++)
            {
                for (int x = 0; x < Data.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == Data.GetLength(0) - 1 || y == Data.GetLength(1) - 1)
                        Data[x, y] = '#';
                    else
                        Data[x, y] = '.';
                }
            }
        }

        public bool IsWalkable(int x, int y)
        {
            return Data[x, y] != '#';
        }
    }
}
