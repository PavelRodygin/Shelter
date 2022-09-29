using System.Collections.Generic;
using DefaultNamespace.Interfaces;
using UnityEngine;

namespace Interactables
{
    public class Generator : MonoBehaviour, IInteractable
    {
        private Animator _animController;
        private bool _isWorking;
        private bool _isBroken;
        private static readonly int Working1 = Animator.StringToHash("IsWorking");
        private static readonly int Broken1 = Animator.StringToHash("IsBroken");
        [SerializeField] private Transform point;
        [SerializeField] private List<GameObject> lights;
        public Transform PointToLook => point;
        public bool IsWorking => _isWorking;

        public bool IsBroken => _isBroken;


        private void Awake()
        {
            _animController = GetComponentInParent<Animator>();
        }

        public void Interact()
        {
            _isWorking = true;
            _animController.SetBool(Working1, IsWorking);
        }

        public void Break()
        {
            _isBroken = true;
            _animController.SetBool(Broken1,true);
                //проиграть звук
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }
}