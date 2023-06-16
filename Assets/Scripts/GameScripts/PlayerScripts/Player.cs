using UIModules.GameScreen.Scripts;
using UnityEngine;

namespace GameScripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerMoveModule))]
    [RequireComponent(typeof(PlayerInteractModule))]
    [RequireComponent(typeof(Pockets))]
    public class Player : MonoBehaviour
    {
        private readonly Vector3 _cameraStandPosition = new(0, 1.6f, 0f);
        private PlayerMoveModule _moveModule;
        private PlayerInteractModule _interactModule;
        private Pockets _pockets;   // Controls item's taking and dropping;
        private Camera _camera;
        private Vector3 _stockCameraPosition;
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

        public void Initialize(GameScreenUIView gameScreenUIView, Camera camera)
        {
            _moveModule = GetComponent<PlayerMoveModule>();
            _interactModule = GetComponent<PlayerInteractModule>();
            _pockets = GetComponent<Pockets>();
            _moveModule.Initialize(gameScreenUIView, camera);
            _interactModule.Initialize(gameScreenUIView, camera);
            _pockets.Initialize(gameScreenUIView, camera);
            _camera = camera;
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