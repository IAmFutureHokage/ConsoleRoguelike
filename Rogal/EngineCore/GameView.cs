using ConsoleApp;
using Rogal.Characters;
using Rogal.Components;
using Rogal.Components.Base;
using System.Text;

namespace Rogal.EngineCore
{
    public sealed class GameRenderer
    {
        private readonly char[] _buffer;
        private readonly Player _player;
        private readonly IMap _map;


        public GameRenderer(Player player, IMap map)
        {
            _player = player;
            _map = map;
            _buffer = new char[map.GetWidth() * map.GetHeight() + map.GetHeight() + 30];
        }

        public void Draw()
        {
            DrawMap();
            DrawInfoPanel();
            Console.SetCursorPosition(0, 0);
            Console.Write(_buffer);
        }

        private void DrawMap()
        {
            int bufferIndex = 0;
            for (int y = 0; y < _map.GetHeight(); y++)
            {
                for (int x = 0; x < _map.GetWidth(); x++)
                {
                    var gameObject = _map.GetTopGameObjectAt(x, y);
                    if (gameObject == null)
                    {
                        _buffer[bufferIndex] = ' ';
                        bufferIndex++;
                        continue;
                    }
                    _buffer[bufferIndex] = gameObject.Symbol;
                    bufferIndex++;
                }
                _buffer[bufferIndex] = '\n';
                bufferIndex++;
            }
        }

        private void DrawInfoPanel()
        {
            var infoBuilder = new StringBuilder(30);
            if (_player is LivingEntity playerEntity)
            {
                infoBuilder.Append($"Health: {playerEntity.Health}  Punch: Press 'E'");
            }

            string info = infoBuilder.ToString();
            int bufferIndex = _map.GetWidth() * _map.GetHeight() + _map.GetHeight();
            for (int i = 0; i < info.Length && bufferIndex < _buffer.Length; i++)
            {
                _buffer[bufferIndex] = info[i];
                bufferIndex++;
            }
        }
    }
}