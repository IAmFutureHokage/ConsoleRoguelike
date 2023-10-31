namespace Rogal.EngineCore
{
    public class GameUpdater
    {
        private IMap _map;

        public GameUpdater(IMap map)
        {
            _map = map;
        }

        public void UpdateAll()
        {
            for (int y = 0; y < _map.GetHeight(); y++)
            {
                for (int x = 0; x < _map.GetWidth(); x++)
                {
                    var gameObject = _map.GetTopGameObjectAt(x, y);
                    gameObject?.Update();
                }
            }
        }
    }
}
