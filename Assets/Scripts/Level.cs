using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace.UI;
using Interfaces;
using PlayerScripts;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Level : MonoBehaviour
{
    [Inject] private GameScreen _gameScreen;
    [SerializeField] private Player _player;
    [SerializeField] private List<IBreakable> breakables;
    [SerializeField] private float breakingTime = 20000f;
    [SerializeField] private float breakTimeMultiplier = 0.9f;
    private bool surviveStarted;
    private bool allIsFine = true;
    private IBreakable _currentBroken;



    private void Awake()
    {
        _player.SurvivalStarted += StartSurvive;
        _gameScreen.ShowGameMessage("That's was a typical day", 4000);
    }

    private void Update()
    {
        // if (_currentBroken && _currentBroken.IsBroken)
        // {
        //     
        // }
        // if (allIsFine)
        // {
        //     BreakSomething();
        // }
    }   

    private async void BreakSomething()
    {
        while (_player.IsAlive)
        {
            int index = Random.Range(0, breakables.Count); 
            breakables[index].Break();
            await UniTask.Delay((int)(breakingTime * breakTimeMultiplier));
        }
    }

    private void StartSurvive()
    {
        surviveStarted = true;
        _gameScreen.ShowGameMessage("Welcome to the bomb shelter", 4000);
    }
}
