using Core.GameInterfaces;
using UnityEngine;
using Zenject;

namespace Core.AbstractClasses
{
    public abstract class OpenClosable : MonoBehaviour, IOpenClosable
    {
        [Inject]
        protected Animator AnimController;
        protected bool _isOpen = false;
        protected bool _isInteractable = true;
        public bool IsOpen { get; protected set; }
        public bool IsInteractable
        {
            get => _isInteractable;
            private set => _isInteractable = value;
        }
        private static readonly int Open = Animator.StringToHash("IsOpen");
        [SerializeField] private Transform pointToLook;
        public Transform PointToLook => pointToLook;
        
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
                Debug.Log(IsOpen);
                AnimController.SetBool(Open, true);
                IsInteractable = false;
                _isOpen = true;
            }
        }
        
        public void IndicateOpenClose()
        {
            IsInteractable = true; 
        }
    }
}