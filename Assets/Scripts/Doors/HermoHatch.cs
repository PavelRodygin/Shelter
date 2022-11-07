using Interfaces;
using UnityEngine;

namespace Doors
{
    public class HermoHatch : MonoBehaviour, IOpenClosable, IBreakable
    {
        private Animator _animController;
        private bool isOpen = false;
        private bool _isInteractable = true;
        public bool IsOpen { get; private set; }
        public bool IsInteractable => _isInteractable;
        public bool IsBroken { get; private set; }
        private static readonly int Open1 = Animator.StringToHash("IsOpen");
        private static readonly int Broken1 = Animator.StringToHash("IsBroken");
        [SerializeField] private Transform point;
        public Transform PointToLook
        {
            get { return point; }
        }
    
        
        private void Awake()
        {
            _animController = GetComponentInParent<Animator>();
        }

        public void Open()
        {
            _animController.SetBool(Open1, true);
            _isInteractable = false;
            IsOpen = true;
        }

        public void Close()
        {
            _animController.SetBool(Open1, false);
            _isInteractable = false;
            IsOpen = false;
        }

        public void IndicateOpenClose()
        {
            _isInteractable = true;
        }

        public void Break()
        {
            IsBroken = true;
            _animController.SetBool(Broken1, true);
            _isInteractable = false;
        }

        public void Fix()
        {
        
        }
    }
}