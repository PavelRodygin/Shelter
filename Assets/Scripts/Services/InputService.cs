using System.Numerics;

namespace Services
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";   
        protected const string Attack = "Attack";
        public abstract Vector2 Axis { get; }
        
        protected static Vector2 GetSimpleInputAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        
        public virtual bool IsAttackButtonUp() => SimpleInput.GetButtonUp(Attack);
    }
}