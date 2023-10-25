using Rogal.Components;

namespace Rogal.Utils
{
    public interface IMap
    {
        GameObject[,] Data { get; }
        void AddGameObject(GameObject gameObject);
        void RemoveGameObject(GameObject gameObject);
        bool IsPositionFree(int x, int y);
        void MoveGameObject(GameObject gameObject, int newX, int newY);
        GameObject GetGameObjectAt(int x, int y);
    }
}
