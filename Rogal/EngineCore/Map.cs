using Rogal.Components;
using Rogal.Components.Base;

namespace Rogal.EngineCore
{
    public sealed class Map : IMap
    {
        public Stack<GameObject>[,] Data { get; private set; }
        private readonly Vector2 _size;
        private Vector2 _previousFinishPosition;

        public Map(int width = 100, int height = 30)
        {
            _size = new Vector2(width, height);
            Data = new Stack<GameObject>[_size.X, _size.Y];
            _previousFinishPosition = new Vector2(1, 1);    
            Initialize();
        }

        private void Initialize()
        {
            var finishPosition = DetermineFinishPosition(_previousFinishPosition);
            _previousFinishPosition = finishPosition;

            for (int y = 0; y < GetHeight(); y++)
            {
                for (int x = 0; x < GetWidth(); x++)
                {
                    Data[x, y] = new Stack<GameObject>();
                    if (x == 0 || y == 0 || x == GetWidth() - 1 || y == GetHeight() - 1)
                    {
                        Data[x, y].Push(new GameObject('#', new Vector2(x, y), false, 10));
                    }
                }
            }
            Data[finishPosition.X, finishPosition.Y].Push(new Finish(finishPosition));
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

            var stackAtNewPosition = Data[newPosition.X, newPosition.Y];
            if (stackAtNewPosition.Any(obj => obj is Player) && stackAtNewPosition.Any(obj => obj is Finish))
            {
                Vector2 playerPositionBeforeReset = _previousFinishPosition;
                Initialize();
                Data[playerPositionBeforeReset.X, playerPositionBeforeReset.Y].Push(gameObject);
            }
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
    }
}

