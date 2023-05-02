using Interfaces;
using UnityEngine;

namespace Core.AbstractClasses
{
    public abstract class OpenClosable : MonoBehaviour, IOpenClosable
    {
        private Animator _animController;
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
            _animController = GetComponentInParent<Animator>();
        }

        public virtual void Open()
        {
            _animController.SetBool(Open1, true);
            IsInteractable = false;
            IsOpen = true;
        }

        public virtual void Close()
        {
            _animController.SetBool(Open1, false);
            IsInteractable = false;
            IsOpen = false;
        }
        
        public void IndicateOpenClose()
        {
            IsInteractable = true; 
        }
    }
}