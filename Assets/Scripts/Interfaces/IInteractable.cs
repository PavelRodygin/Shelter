using UnityEngine;

namespace DefaultNamespace.Interfaces
{
    public interface IInteractable
    {
        public bool IsWorking { get; }
        public Transform PointToLook { get; }
        void Interact();
    }
}