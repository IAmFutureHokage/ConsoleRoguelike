using ConsoleApp;
using Rogal.Components;
using Rogal.Components.Base;
using System.Text;

public sealed class GameRenderer
{
    private readonly GameInit _gameInit;
    private readonly Vector2 _mapSize;
    private char[] _buffer;

    public GameRenderer(GameInit gameInit)
    {
        _gameInit = gameInit;
        _mapSize.X = _gameInit.Map.GetWidth();
        _mapSize.Y = _gameInit.Map.GetHeight();
        _buffer = new char[_mapSize.X * _mapSize.Y + _mapSize.Y + 30];
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
        for (int y = 0; y < _mapSize.Y; y++)
        {
            for (int x = 0; x < _mapSize.X; x++)
            {
                var gameObject = _gameInit.Map.GetTopGameObjectAt(x, y);
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
        if (_gameInit.Player is LivingEntity playerEntity)
        {
            infoBuilder.Append($"Health: {playerEntity.Health}  Punch: Press 'E'");
        }

        string info = infoBuilder.ToString();
        int bufferIndex = _mapSize.X * _mapSize.Y + _mapSize.Y;
        for (int i = 0; i < info.Length && bufferIndex < _buffer.Length; i++)
        {
            _buffer[bufferIndex] = info[i];
            bufferIndex++;
        }
    }
}
