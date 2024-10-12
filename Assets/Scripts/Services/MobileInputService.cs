using System.Numerics;

namespace Services
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => GetSimpleInputAxis();
    }
}