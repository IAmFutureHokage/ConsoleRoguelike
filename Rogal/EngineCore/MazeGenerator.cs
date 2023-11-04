using Rogal.Components;
using Rogal.Components.Base;

namespace Rogal.EngineCore
{
    public class MazeGenerator
    {
        private Random _random = new();
        private readonly Stack<GameObject>[,] _data;
        private readonly int _width;
        private readonly int _height;

        public MazeGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _data = new Stack<GameObject>[width, height];
        }

        public Stack<GameObject>[,] Generate(Vector2 start, Vector2 finish)
        {
            Initialize();
            GenerateMaze((int)start.X, (int)start.Y);
            MakeAccessible(finish);
            return _data;
        }

        private void Initialize()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _data[x, y] = new Stack<GameObject>();
                    _data[x, y].Push(new GameObject('▓', new Vector2(x, y), false, 10));
   
                }
            }
        }

        private void GenerateMaze(int x, int y)
        {
            _data[x, y].Clear();
            var directions = new (int, int)[] { (0, -2), (2, 0), (0, 2), (-2, 0) };
            Shuffle(directions);

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx, newY = y + dy;
                if (IsInsideBounds(newX, newY) && _data[newX, newY].Count > 0)
                {
                    _data[newX - dx / 2, newY - dy / 2].Clear();
                    GenerateMaze(newX, newY);
                }
            }
        }

        private void MakeAccessible(Vector2 finish)
        {
            var directions = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
            foreach (var (dx, dy) in directions)
            {
                int newX = (int)finish.X + dx, newY = (int)finish.Y + dy;
                if (IsInsideBounds(newX, newY))
                {
                    _data[newX, newY].Clear();
                }
            }
        }

        private void Shuffle<T>((T, T)[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        private bool IsInsideBounds(int x, int y)
        {
            return x > 0 && x < _width - 1 && y > 0 && y < _height - 1;
        }
    }

}
