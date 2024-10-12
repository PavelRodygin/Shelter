using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Services
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                var axis = GetSimpleInputAxis();
                Debug.Log("Получили SimpleInput аксис" + axis);

                if (axis == Vector2.Zero)
                {
                    axis = GetUnityInputAxis();
                    Debug.Log("Получили юнити аксис" + axis);
                }
                
                return axis;
            }
        }

        private static Vector2 GetUnityInputAxis() => 
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        public override bool IsAttackButtonUp() => SimpleInput.GetButtonUp(Attack);
    }
}