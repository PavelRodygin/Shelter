using Core.GameInterfaces;
using UnityEngine;
using Zenject;

namespace Core.AbstractClasses
{
    [RequireComponent(typeof(Animator))]
    public abstract class OpenClosable : MonoBehaviour, IOpenClosable
    {
        [SerializeField] private Transform pointToLook;
        protected Animator AnimController;
        protected bool _isOpen;
        protected bool _isInteractable = true;
        private static readonly int Open = Animator.StringToHash("IsOpen");

        public Transform PointToLook => pointToLook;
        public bool IsOpen { get; protected set; }
        public bool IsInteractable
        {
            get => _isInteractable;
            private set => _isInteractable = value;
        }
        
        protected virtual void Awake()
        {
            AnimController = GetComponent<Animator>();
        }

        public virtual void OpenClose()
        {
            if (_isOpen)
            {
                AnimController.SetBool(Open, false);
                IsInteractable = false;
                _isOpen = false;
            }
            else
            {
                AnimController.SetBool(Open, true);
                IsInteractable = false;
                _isOpen = true;
            }
        }
        
        public void IndicateOpenClose() //For anim controller
        {
            IsInteractable = true; 
        }
    }
}