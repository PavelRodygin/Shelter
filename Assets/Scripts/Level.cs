using System.Collections.Generic;
using DefaultNamespace.UI;
using Interfaces;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    [Inject] private UIManager uiManager;
    [SerializeField] private List<IBreakable> breakables;
    private bool _allIsFine = true;
    private IBreakable _currentBroken;
    


    private void Update()
    {
        // if (_currentBroken || _currentBroken.)
        // {
        //     
        // }
        // if (allIsFine)
        // {
        //     MakeSomethingBad();
        // }
    }   

    private void MakeSomethingBad()
    {
        int index = UnityEngine.Random.Range(0, breakables.Count); 
        breakables[index].Break();
    }
}
