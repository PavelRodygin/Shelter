using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerMoveModule))]
    [RequireComponent(typeof(PlayerInteractModule))]
    [RequireComponent(typeof(Pockets))]
    public class Player : MonoBehaviour
    {
        [Inject] private Camera _camera;
        private PlayerMoveModule _moveModule;
        private PlayerInteractModule _interactModule;
        private Pockets _pockets;
        private Vector3 _stockCameraPosition;
        private Vector3 _cameraStandPosition = new Vector3(0, 1.6f, 0f);
        private bool _isAlive;
        
        public bool IsAlive { get; private set; }
        public Pockets Pockets
        {
            set => _pockets = value;
        }
        public PlayerMoveModule MoveModule
        {
            get => _moveModule;
            set => _moveModule = value;
        }
        public PlayerInteractModule InteractModule
        {
            get => _interactModule;
            set => _interactModule = value;
        }

        public void Initialize(GameScreenUIView gameScreenUIView)
        {
            _moveModule = GetComponent<PlayerMoveModule>();
            _interactModule = GetComponent<PlayerInteractModule>();
            _pockets = GetComponent<Pockets>();
            _moveModule.Initialize(gameScreenUIView);
            _interactModule.Initialize(gameScreenUIView);
            _pockets.Initialize(gameScreenUIView);
            Debug.Log("Здесь говно момент");
            _camera = Camera.main;
            _camera.orthographic = false;
            _camera.transform.parent = transform;
            _stockCameraPosition = _camera.gameObject.transform.position;
            _camera.transform.position = _cameraStandPosition;
        }

        public void Hide()
        {
            _camera.orthographic = false;
            _camera.transform.parent = null;
            _camera.gameObject.transform.position = _stockCameraPosition;
        }
    }
}