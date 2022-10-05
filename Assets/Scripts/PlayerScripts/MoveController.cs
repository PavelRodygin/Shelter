using UI;
using UnityEngine;
using Zenject;

namespace PlayerScripts
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider upBody;
        [SerializeField] private float maxWalkSpeed = 10;
        private float _currentWalkSpeed;
        [SerializeField] private float sensivity;
        [SerializeField] private float jumpPower = 2;
        [SerializeField] private float gravityAxeleration = 10;
        [Inject] private GameScreen _gameScreen;
        private CharacterController _characterController;
        private Joystick _joystick;
        [SerializeField] GameObject playerCamera;
        private Vector3 _moveDirection;
        private float _gravityForce;
        private int _rightFingerID;
        private float _halfScreenWidth;
        private Vector2 _lookInput;
        private float _cameraPitch;
        
        

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _halfScreenWidth = Screen.width / 2;
            _joystick = _gameScreen.walkJoystick;
            _rightFingerID = -1;
            _gameScreen.jumpButton.onClick.AddListener(Jump);
            _gameScreen.crouchButton.onClick.AddListener(Crouch);
            _gameScreen.getUpButton.onClick.AddListener(GetUp);
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
                _gravityForce -= gravityAxeleration * Time.deltaTime;
            }
            // else
            // {
            //     _gravityForce = -1f;
            // }
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
                playerCamera.transform.localPosition = Vector3.zero;
                _currentWalkSpeed = maxWalkSpeed / 2;
                _gameScreen.crouchButton.gameObject.SetActive(false);
                _gameScreen.getUpButton.gameObject.SetActive(true);
            }
        }
        
        private void GetUp()
        {
            if (_characterController.isGrounded)
            {
                upBody.enabled = true;
                playerCamera.transform.localPosition = new Vector3(0,1.6f,0);
                _currentWalkSpeed = maxWalkSpeed;
                _gameScreen.getUpButton.gameObject.SetActive(false);
                _gameScreen.crouchButton.gameObject.SetActive(true);
            }
        }
    }
}