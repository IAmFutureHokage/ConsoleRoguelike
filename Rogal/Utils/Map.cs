using Rogal.Components.Base;
using Rogal.Components;

namespace Rogal.Utils
{
    public class Map : IMap
    {
        public GameObject[,] Data { get; private set; }
        public Map(int width, int height)
        {
            Data = new GameObject[width, height];
            Initialize();
        }
        private void Initialize()
        {
            for (int y = 0; y < Data.GetLength(1); y++)
            {
                for (int x = 0; x < Data.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == Data.GetLength(0) - 1 || y == Data.GetLength(1) - 1)
                    {
                        var wallTransform = new Transform(x, y, 0);
                        var wallRenderable = new Renderable('#');
                        var wallHealth = new ImmutableHealth();
                        Data[x, y] = new Wall(wallTransform, wallRenderable, wallHealth);
                    }
                    else
                    {
                        Data[x, y] = null;
                    }
                }
            }
        }
        public void AddGameObject(GameObject gameObject)
        {
            Data[gameObject.Transform.X, gameObject.Transform.Y] = gameObject;
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            Data[gameObject.Transform.PreviousX, gameObject.Transform.PreviousY] = null;
        }


        public bool IsPositionFree(int x, int y)
        {
            if (x < 0 || x >= Data.GetLength(0) || y < 0 || y >= Data.GetLength(1))
            {
                return false;
            }
            return Data[x, y] == null;
        }

        public void MoveGameObject(GameObject gameObject, int newX, int newY)
        {
            if (!IsPositionFree(newX, newY))
            {
                return;
            }

            RemoveGameObject(gameObject);
            gameObject.Transform.X = newX;
            gameObject.Transform.Y = newY;
            AddGameObject(gameObject);
        }
        public GameObject GetGameObjectAt(int x, int y)
        {
            if (x >= 0 && x < Data.GetLength(0) && y >= 0 && y < Data.GetLength(1))
            {
                return Data[x, y];
            }
            return null;
        }

    }
}