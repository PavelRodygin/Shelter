using Interfaces;
using UnityEngine;

namespace GameScripts.PlayerScripts
{
    public class EmptyHand : MonoBehaviour, IItem
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
