using Rogal.Components.Base;

namespace Rogal.EngineCore
{
    public sealed class Map : IMap
    {
        public Stack<GameObject>[,] Data { get; private set; }
        private readonly Vector2 _size;

        public Map(int width = 100, int height = 30)
        {
            _size = new Vector2(width, height);
            Data = new Stack<GameObject>[_size.X, _size.Y];
            Initialize();
        }

        private void Initialize()
        {
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
    }
}

