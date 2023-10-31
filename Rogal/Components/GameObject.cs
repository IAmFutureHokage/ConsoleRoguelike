using Rogal.Components.Base;

public class GameObject
{
    public char Symbol { get; set; }
    public Vector2 Position
    {
        get => _position;
        set
        {
            PreviousPosition = _position;
            _position = value;
        }
    }
    private Vector2 _position;

    public Vector2 PreviousPosition { get; private set; }
    public bool IsPassable { get; set; }
    public int Speed { get; private set; }

    public GameObject(char symbol, Vector2 position, bool isPassable, int speed)
    {
        Symbol = symbol;
        _position = position;
        IsPassable = isPassable;
        Speed = speed;
        PreviousPosition = new Vector2( _position.X -1, _position.Y);
    }

    public virtual void Update()
    {
    }
}
