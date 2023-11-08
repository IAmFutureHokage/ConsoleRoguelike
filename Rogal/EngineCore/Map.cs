using Rogal.Characters;
using Rogal.Components;
using Rogal.Components.Base;

namespace Rogal.EngineCore
{
    public sealed class Map : IMap
    {
        public Stack<GameObject>[,] Data { get; private set; }
        private readonly Vector2 _size;
        private Vector2 _previousFinishPosition;
        private Random _random = new Random();

        public Map(int width = 100, int height = 30)
        {
            _size = new Vector2(width, height);
            Data = new Stack<GameObject>[_size.X, _size.Y];
            _previousFinishPosition = new Vector2(1, 1);
            Initialize();
        }

        private void Initialize()
        {
            var mazeGenerator = new MazeGenerator(_size.X, _size.Y);
            var finishPosition = DetermineFinishPosition(_previousFinishPosition);

            Data = mazeGenerator.Generate(_previousFinishPosition, finishPosition);
            Data[finishPosition.X, finishPosition.Y].Push(new Finish(finishPosition));
            //CreateNettles();
            _ = new Archer(this, new Vector2(3, 3));
            _previousFinishPosition = finishPosition;
        }

        private void CreateNettles()
        {
            List<Vector2> emptyCells = new List<Vector2>();

            for (int x = 0; x < _size.X; x++)
            {
                for (int y = 0; y < _size.Y; y++)
                {
                    if (Data[x, y].Count == 0)
                    {
                        emptyCells.Add(new Vector2(x, y));
                    }
                }
            }

            int nettlesToCreate = emptyCells.Count / 15;

            for (int i = 0; i < nettlesToCreate; i++)
            {
                Vector2 position;
                do
                {
                    position = emptyCells[_random.Next(emptyCells.Count)];
                }
                while (Distance(position, _previousFinishPosition) < 5);

                _ = new LiveNettle(this, position);
            }
        }
        private static int Distance(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }


        private Vector2 DetermineFinishPosition(Vector2 previousFinishPosition)
        {
            if (previousFinishPosition.X == 1 && previousFinishPosition.Y == 1)
            {
                return new Vector2(GetWidth() - 2, GetHeight() - 2);
            }
            return new Vector2(1, 1);
        }


        public void AddGameObject(GameObject gameObject)
        {
            Data[gameObject.Position.X, gameObject.Position.Y].Push(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            if (!Data[gameObject.Position.X, gameObject.Position.Y].Any())
            {
                return;
            }

            Data[gameObject.Position.X, gameObject.Position.Y].Pop();
        }

        public bool IsPositionFree(int x, int y)
        {
            if (!IsWithinBounds(x, y))
            {
                return false;
            }
            return Data[x, y].Count == 0 || Data[x, y].Peek().IsPassable;
        }

        public void MoveGameObject(GameObject gameObject, Vector2 newPosition)
        {
            if (!IsPositionFree(newPosition.X, newPosition.Y))
            {
                return;
            }
            RemoveGameObject(gameObject);
            gameObject.Position = newPosition;
            AddGameObject(gameObject);
        }


        public GameObject GetTopGameObjectAt(int x, int y)
        {
            if (Data[x, y].Any())
            {
                return Data[x, y].Peek();
            }
            return null;
        }

        private bool IsWithinBounds(int x, int y) => x >= 0 && x < _size.X && y >= 0 && y < _size.Y;

        public int GetWidth() => _size.X;

        public int GetHeight() => _size.Y;

        public void Finished(Player player, Vector2 position)
        {
            var stackAtNewPosition = Data[position.X, position.Y];
            if (stackAtNewPosition.Any(obj => obj is Player) && stackAtNewPosition.Any(obj => obj is Finish))
            {
                Vector2 playerPositionBeforeReset = _previousFinishPosition;
                Initialize();
                Data[playerPositionBeforeReset.X, playerPositionBeforeReset.Y].Push(player);
            }
        }


    }
}

