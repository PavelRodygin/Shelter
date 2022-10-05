using Interfaces;
using UnityEngine;

namespace PlayerScripts
{
    public class Hand : MonoBehaviour, IItem
    {
        public void Grab(Transform owner)
        {
        }

        public void Throw()
        {
        }

        public Transform Transform { get; }
    }
}
