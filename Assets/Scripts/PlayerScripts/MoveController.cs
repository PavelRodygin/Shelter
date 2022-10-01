using UI;
using UnityEngine;
using Zenject;

namespace PlayerScripts
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float maxWalkSpeed;
        [SerializeField] private float sensivity;
        [SerializeField] private float jumpPower;
        [Inject] private GameScreen _gameScreen;
        private CharacterController _characterController;
        private Joystick _joystick;
        private Transform _cameraTransform;
        private Vector3 _moveDirection;
        private int _rightFingerID;
        private float _halfScreenWidth;
        private Vector2 _lookInput;
        private float _cameraPitch;
        
        

        private void Start()
        {
            _cameraTransform = GetComponentInChildren<Camera>().transform;
            _characterController = GetComponent<CharacterController>();
            _rightFingerID = -1;
            _halfScreenWidth = Screen.width / 2;
            _joystick = _gameScreen.walkJoystick;
        }

        private void Update()
        {
            GetTouch();
        }

        private void FixedUpdate()
        {
            if (_rightFingerID != -1)
            {
                LookAround();
            }
            MovePlayer();
        }
        
        private void GetTouch()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                switch (t.phase)
                {
                    case TouchPhase.Began :
                        if (t.position.x > _halfScreenWidth && _rightFingerID == -1)
                        {
                            _rightFingerID = t.fingerId;
                        }
                        break;
                    case TouchPhase.Ended:
                    // case TouchPhase.Canceled:
                    //     if (t.fingerId == _rightFingerID)
                    //     {
                    //         _rightFingerID = -1;
                    //     }
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
            _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
            _cameraTransform.Rotate(transform.up,_lookInput.x);
        }

        private void MovePlayer()
        {
            _moveDirection = Vector3.zero;
            _moveDirection.x = _joystick.Horizontal;
            _moveDirection.z = _joystick.Vertical;
            var transform1 = transform;
            _moveDirection = transform1.right * _moveDirection.x + transform1.forward * _moveDirection.z +
                             transform1.up * _moveDirection.y;
            _characterController.Move(_moveDirection * (maxWalkSpeed * Time.deltaTime));
        }
    }
}