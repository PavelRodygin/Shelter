using Interfaces;
using UnityEngine;

namespace Doors
{
    public class Door : MonoBehaviour, IOpenClosable
    {
        private Animator _animController;
        private bool _isOpen = false;
        private bool _isInteractable = true;
        public bool IsOpen { get; private set; }
        public bool IsInteractable { get; }
        private static readonly int Open1 = Animator.StringToHash("IsOpen");
        [SerializeField] private Transform point;
        public Transform PointToLook => point;
    
        
        
        private void Awake()
        {
            _animController = GetComponentInParent<Animator>();
        }

        public void Open()
        {
            _animController.SetBool(Open1, true);
            IsOpen = true;
        }

        public void Close()
        {
            _animController.SetBool(Open1, false);
            IsOpen = false;
        }
    }
}