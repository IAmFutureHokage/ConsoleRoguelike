using Rogal.Characters;
using Rogal.Components;
using Rogal.Components.Base;

namespace Rogal.EngineCore
{
    public interface IMap
    {
        Stack<GameObject>[,] Data { get; }
        int GetWidth();
        int GetHeight();
        void AddGameObject(GameObject gameObject);
        void RemoveGameObject(GameObject gameObject);
        bool IsPositionFree(int x, int y);
        void MoveGameObject(GameObject gameObject, Vector2 newPosition);
        GameObject GetTopGameObjectAt(int x, int y);
        void Finished(Player player, Vector2 position);
    }
}
