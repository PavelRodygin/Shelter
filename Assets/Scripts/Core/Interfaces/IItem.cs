using UnityEngine;

namespace Core.GameInterfaces
{
    public interface IItem
    {
        public Transform Transform { get; }
        
        void GetGrabbed(Transform owner);
        void GetThrown(Vector3 throwForce);
    }
}