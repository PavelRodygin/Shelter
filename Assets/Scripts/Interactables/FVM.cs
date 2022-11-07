using DefaultNamespace.Interfaces;
using Interfaces;
using UnityEngine;

namespace Interactables
{
     public class FVM :  MonoBehaviour, IInteractable, IBreakable
     {
          private Animator _animController;
          private bool _isWorking;
          private bool _isBroken;
          private static readonly int Working1 = Animator.StringToHash("IsWorking");
          private static readonly int Broken1 = Animator.StringToHash("IsBroken");
          [SerializeField] private Transform viewPoint;
          public Transform PointToLook
          {
               get { return viewPoint; }
          }

          public bool IsBroken => _isBroken;

          public bool IsWorking => _isWorking;


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
               _isWorking = false;
               _isBroken = true;
               _animController.SetBool(Broken1,true);
          }

          public void Fix()
          {
               _isBroken = false;
          }
     }
}
