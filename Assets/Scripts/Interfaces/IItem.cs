using UnityEngine;

namespace Interfaces
{
    public interface IItem
    {
        void Grab(Transform owner);
        void Throw();
        public Transform Transform { get; }
    }
}