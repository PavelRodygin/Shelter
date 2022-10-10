using System;
using DefaultNamespace;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace PlayerScripts
{
    public class MoveController : MonoBehaviour
    {
        [Inject] private GameScreen _gameScreen;
        [SerializeField] private CapsuleCollider upBody;
        [SerializeField] private float maxWalkSpeed = 10;
        [SerializeField] private float sensivity = 5;
        [SerializeField] private float jumpPower = 300;
        private GameObject _playerBody;
        private float _currentWalkSpeed;
        private bool _isGrounded;
        private Rigidbody _rigidbody;
        private Joystick _joystick;
        private Vector3 _moveDirection;
        private int _rightFingerID;
        private float _halfScreenWidth;
        private Vector2 _lookInput;
        private float _cameraPitch;
        private Quaternion _currentRotation;
        

        private void Awake()
        {
            _playerBody = transform.parent.gameObject;
            _rigidbody = GetComponentInParent<Rigidbody>();
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
            if (_rightFingerID != -1)
            {
                LookAround();
            }
            Move();
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
            _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, -90f, 90f);
            transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
            Debug.Log(_lookInput.x);   
            transform.Rotate(transform.up,_lookInput.x);
        }

        private void Move()
        {
            _moveDirection = Vector3.zero;
            _moveDirection.x = _joystick.Horizontal;
            _moveDirection.z = _joystick.Vertical;
            _moveDirection = _playerBody.transform.right * _moveDirection.x  + _playerBody.transform.forward * _moveDirection.z +
                             _playerBody.transform.up * _moveDirection.y;
            _playerBody.transform.position += (_moveDirection * (_currentWalkSpeed * Time.deltaTime));
           Debug.Log(_moveDirection);    // не мешает
        }

        private void Jump()
        {
            if (_isGrounded && upBody.enabled)
            {
                _rigidbody.AddForce(0,-jumpPower, 0);
            }
        }

        private void Crouch()
        {
            if (_isGrounded)
            {
                upBody.enabled = false;
                transform.localPosition = new Vector3(0,2.6f, 0);
                _currentWalkSpeed = maxWalkSpeed / 2;
                _gameScreen.crouchButton.gameObject.SetActive(false);
                _gameScreen.getUpButton.gameObject.SetActive(true);
            }
        }
        
        private void GetUp()
        {
            if (_isGrounded)
            {
                upBody.enabled = true;
                transform.localPosition = new Vector3(0,4f,0);
                _currentWalkSpeed = maxWalkSpeed;
                _gameScreen.getUpButton.gameObject.SetActive(false);
                _gameScreen.crouchButton.gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.GetComponent<Floor>() != null)
            {
                _isGrounded = true;
                _rigidbody.velocity = Vector3.zero;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<Floor>()!= null)
            {
                _isGrounded = true;
            }
        }
    }
}