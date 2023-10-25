namespace Rogal.Components.Base
{
    public class ImmutableHealth : Health
    {
        public ImmutableHealth() : base(int.MaxValue)
        {
        }
        public override void TakeDamage(int amount)
        {

        }
    }
}