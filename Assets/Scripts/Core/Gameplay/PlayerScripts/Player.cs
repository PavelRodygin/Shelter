using Cysharp.Threading.Tasks;
using Modules.GameScreen.Scripts;
using Services;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.PlayerScripts
{
    [RequireComponent(typeof(PlayerMoveController))]
    [RequireComponent(typeof(PlayerInteractionController))]
    [RequireComponent(typeof(PlayerItemManager))]
    public class Player : MonoBehaviour
    {
        [Inject] private GameScreenView _gameScreenView;
        [Inject] private IInputService _inputService;
        [Inject] private Camera _camera;
        
        [SerializeField] private PlayerMoveController moveController;
        [SerializeField] private PlayerInteractionController interactionController;
        [SerializeField] private PlayerItemManager pockets;
        private Vector3 _stockCameraPosition = Vector3.zero;
        private bool _isAlive;

        public void Initialize( float sensitivity)
        {
            moveController.Initialize(_gameScreenView, _inputService, GetComponent<Camera>(), sensitivity);
            interactionController.Initialize(_gameScreenView, GetComponent<Camera>());
            pockets.Initialize(_gameScreenView, GetComponent<Camera>());
        }
        
        public async UniTask Show()    //TODO EyesClosing or smth beatiful
        {
            gameObject.SetActive(true);
            _camera.orthographic = false;
            _camera.transform.parent = transform;
        }

        public async UniTask Hide()
        {
            _camera.orthographic = false;
            _camera.transform.parent = null;
            _camera.gameObject.transform.position = _stockCameraPosition;
            
            gameObject.SetActive(false);
        }
    }
}