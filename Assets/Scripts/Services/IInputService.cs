using System.Numerics;

namespace Services
{
    public interface IInputService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}   