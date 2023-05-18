using Core.GameInterfaces;
using UnityEngine;

namespace Core.AbstractClasses
{
    public abstract class OpenClosable : MonoBehaviour, IOpenClosable
    {
        protected Animator AnimController;
        protected bool _isOpen = false;
        protected bool _isInteractable = true;
        public bool IsOpen { get; protected set; }
        public bool IsInteractable
        {
            get => _isInteractable;
            private set => _isInteractable = value;
        }
        private static readonly int Open1 = Animator.StringToHash("IsOpen");
        [SerializeField] private Transform pointToLook;
        public Transform PointToLook => pointToLook;
        
        protected virtual void Awake()
        {
            AnimController = GetComponentInParent<Animator>();
        }

        public virtual void OpenClose()
        {
            if (_isOpen)
            {
                AnimController.SetBool(Open1, false);
                IsInteractable = false;
                IsOpen = false;
            }
            else
            {
                AnimController.SetBool(Open1, true);
                IsInteractable = false;
                IsOpen = true;
            }
        }
        
        public void IndicateOpenClose()
        {
            IsInteractable = true; 
        }
    }
}