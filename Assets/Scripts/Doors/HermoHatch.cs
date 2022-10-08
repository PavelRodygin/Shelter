using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class HermoHatch : MonoBehaviour, IOpenClosable, IBreakable
{
    private Animator _animController;
    private bool isOpen = false;
    public bool IsOpen { get; private set; }
    public bool IsBroken { get; private set; }
    private bool _isAnimationCompleted = true;
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
        if (_isAnimationCompleted)
        {
            _animController.SetBool(Open1, true);
            IsOpen = true;
            //_isAnimationCompleted = false;
        }
    }

    public void Close()
    {
        if (_isAnimationCompleted)
        {
            _animController.SetBool(Open1, false);
            IsOpen = false;
            //_isAnimationCompleted = false;
        }
    }

    public void IndicateEndOfAnimation()
    {
        _isAnimationCompleted = true;
    }
        
    public void Break()
    {
        IsBroken = true;
        _animController.SetBool(Broken1, true);
    }

    public void Fix()
    {
        
    }
}
