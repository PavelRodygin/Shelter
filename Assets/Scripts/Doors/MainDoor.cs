using Interfaces;
using UnityEngine;

namespace Doors
{
    public class MainDoor : MonoBehaviour, IOpenClosable, IBreakable
    {
        private Animator _animController;
        private bool _isOpen = false;
        private bool _isInteractable = true;
        public bool IsOpen { get; private set; }
        public bool IsInteractable
        {
            get { return _isInteractable;}
            private set { _isInteractable = value; }
        }
        public bool IsBroken { get; private set; }
        private static readonly int Open1 = Animator.StringToHash("IsOpen");
        private static readonly int Broken1 = Animator.StringToHash("IsBroken");
        [SerializeField] private Transform wheel;
        public Transform PointToLook
        {
            get { return wheel; }
        }
        
        
        private void Awake()
        {
            _animController = GetComponentInParent<Animator>();
        }

        public void Open()
        {
            _animController.SetBool(Open1, true);
            IsInteractable = false;
            IsOpen = true;
        }

        public void Close()
        {
            _animController.SetBool(Open1, false);
            IsInteractable = false;
            IsOpen = false;
        }
        
        public void IndicateOpenClose()
        {
            IsInteractable = true; 
        }
        
        public void Break()
        {
            IsBroken = true;
            _animController.SetBool(Broken1, true);
            IsInteractable = false;
        }

        public void Fix()
        {
            IsInteractable = true;
        }
    }
}