using Rogal.Components.Base;
using System.Collections.Generic;

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
    }
}
