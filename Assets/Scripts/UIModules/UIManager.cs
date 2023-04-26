using UI;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class UIManager : MonoBehaviour
    {
        [Inject] private GameScreen gameScreen;
        [Inject] private SettingsScreen settingsScreen;
        [Inject] private MainScreen mainScreen;

        private void Awake()
        {
            
        }
    }
}