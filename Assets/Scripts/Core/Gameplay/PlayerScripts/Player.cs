using Cysharp.Threading.Tasks;
using Modules.GameScreen.Scripts;
using Services;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.PlayerScripts
{
    [RequireComponent(typeof(FirstPersonController))]
    [RequireComponent(typeof(PlayerInteractionController))]
    [RequireComponent(typeof(PlayerItemManager))]
    public class Player : MonoBehaviour
    {
        [Inject] private GameScreenView _gameScreenView;
        [Inject] private IInputService _inputService;
        [Inject] private Camera _camera;
        
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PlayerInteractionController interactionController;
        [SerializeField] private PlayerItemManager pockets;
        private bool _isAlive;

        public void Initialize( float sensitivity)
        {
            interactionController.Initialize(_gameScreenView, _camera);
            pockets.Initialize(_gameScreenView, _camera);
        }
        
        public async UniTask Show()    //TODO EyesClosing or smth beatiful
        {
            gameObject.SetActive(true);
        }

        public async UniTask Hide()
        {
            gameObject.SetActive(false);
        }
    }
}