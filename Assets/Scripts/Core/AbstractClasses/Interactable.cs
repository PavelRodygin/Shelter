using Core.GameInterfaces;
using Interfaces;
using UnityEngine;

namespace Core.AbstractClasses
{
    public class Interactable : IInteractable
    {
        public bool IsWorking { get; }
        public Transform PointToLook { get; }
        public void Interact()
        {
            
        }
    }
}