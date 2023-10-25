namespace Rogal.Components.Base
{
    public class Health
    {
        public Health(int initialValue)
        {
            Value = initialValue;
        }
        public int Value { get; private set; }
        public virtual void TakeDamage(int amount)
        {
            Value = Math.Max(0, Value - amount);
        }
    }
}
