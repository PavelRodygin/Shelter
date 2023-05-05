using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    public class MoveController : MonoBehaviour
    {
        private const float PlayerHeigth = 4.5f;
        [SerializeField] private CapsuleCollider upBody;
        [SerializeField] private float maxWalkSpeed = 10;
        private float _currentWalkSpeed;
        [SerializeField] private float sensivity = 5;
        [SerializeField] private float jumpPower = 2;
        [SerializeField] private float gravityAxceleration = 10;
        [Inject] private GameScreenUIView _gameScreenUIView;
        private CharacterController _characterController;
        private Joystick _joystick;
        [SerializeField] GameObject playerCamera;
        private Vector3 _moveDirection;
        private float _gravityForce;
        private int _rightFingerID;
        private float _halfScreenWidth;
        private Vector2 _lookInput;
        private float _cameraPitch;
        public bool IsAlive;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _halfScreenWidth = Screen.width / 2;
            _joystick = _gameScreenUIView.walkJoystick;
            _rightFingerID = -1;
            _gameScreenUIView.jumpButton.onClick.AddListener(Jump);
            _gameScreenUIView.crouchButton.onClick.AddListener(Crouch);
            _gameScreenUIView.getUpButton.onClick.AddListener(GetUp);
            _currentWalkSpeed = maxWalkSpeed;
        }

        private void Update()
        {
            GetTouchInput();
        }

        private void FixedUpdate()
        {
            WalkAndLookAround();
            Gravity();
            if (_rightFingerID != -1)
            {
                LookAround();
            }
        }
        
        private void GetTouchInput()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        if (t.position.x > _halfScreenWidth && _rightFingerID == -1)
                        {
                            _rightFingerID = t.fingerId;
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (t.fingerId == _rightFingerID)
                        {
                            _rightFingerID = -1;
                        }
                        break;
                    case TouchPhase.Moved:
                        if (t.fingerId == _rightFingerID)
                        {
                            _lookInput = t.deltaPosition * (sensivity * Time.deltaTime);
                        }
                        break;
                    case TouchPhase.Stationary:
                        if (t.fingerId == _rightFingerID)
                        {
                            _lookInput = Vector2.zero;
                        }
                        break;
                }
            }
        }

        private void LookAround()
        {
            _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, -90, 90);
            playerCamera.transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
            transform.Rotate(transform.up,_lookInput.x);
        }

        private void WalkAndLookAround()
        {
            _moveDirection = Vector3.zero;
            _moveDirection.x = _joystick.Horizontal;
            _moveDirection.z = _joystick.Vertical;
            _moveDirection.y = _gravityForce;
            var transform1 = transform;
            _moveDirection = transform1.right * _moveDirection.x + transform1.forward * _moveDirection.z +
                             transform1.up * _moveDirection.y;
            _characterController.Move(_moveDirection * (_currentWalkSpeed * Time.deltaTime));
        }

        private void Gravity()
        {
            if (!_characterController.isGrounded)
            {
                _gravityForce -= gravityAxceleration * Time.deltaTime;
            }
            else
            {
                _gravityForce = -1f;
            }
        }

        private void Jump()
        {
            if (_characterController.isGrounded && upBody.enabled)
            {
                _gravityForce = jumpPower;
            }
        }

        private void Crouch()
        {
            if (_characterController.isGrounded)
            {
                upBody.enabled = false;
                playerCamera.transform.localPosition = new Vector3(0,PlayerHeigth/2,0);
                _currentWalkSpeed = maxWalkSpeed / 2;
                _gameScreenUIView.crouchButton.gameObject.SetActive(false);
                _gameScreenUIView.getUpButton.gameObject.SetActive(true);
            }
        }
        
        private void GetUp()
        {
            RaycastHit hit;
            bool wallUpHead = false;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, PlayerHeigth))
                //wallUpHead = true;
            
            if (_characterController.isGrounded && !wallUpHead)
            {
                upBody.enabled = true;
                playerCamera.transform.localPosition = new Vector3(0,PlayerHeigth,0);
                _currentWalkSpeed = maxWalkSpeed;
                _gameScreenUIView.getUpButton.gameObject.SetActive(false);
                _gameScreenUIView.crouchButton.gameObject.SetActive(true);
            }
        }
    }
}