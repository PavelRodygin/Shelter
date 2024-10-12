using System;
using DG.Tweening;
using Modules.GameScreen.Scripts;
using Services;
using UnityEngine;

namespace Core.Gameplay.PlayerScripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float playerStandHeight = 1.8f;
        [SerializeField] private float playerCrouchHeight = 0.4f;
        [SerializeField] private float maxWalkSpeed = 3;
        private IInputService _inputService;
        private GameScreenView _gameScreenView;
        private Camera _camera;
        private Vector3 _moveDirection;
        private Vector3 _velocity;
        private Vector2 _lookInput;
        private Vector3 _cameraStandPosition;
        private Vector3 _cameraCrouchPosition;
        private float _sensitivity;
        private bool _isGrounded;
        private const float GroundDistance = 0.4f;
        private const float GravityForce = 10f;
        private bool _isCrouching;
        private float _currentWalkSpeed;
        private float _cameraPitch;
        private int _rightFingerID;
        public event Action OnPlayerInsideShelter;
        
        public void Initialize(GameScreenView gameScreenView, IInputService inputService, Camera camera, float sensitivity)
        {
            _inputService = inputService;
            _camera = camera;
            _cameraStandPosition = new Vector3(0f, characterController.height - 0.1f, 0f);
            _cameraCrouchPosition = new Vector3(0f, playerCrouchHeight, 0f);
            _camera.transform.localPosition = _cameraStandPosition;
            _gameScreenView = gameScreenView;
            _sensitivity = sensitivity;
            _rightFingerID = -1;
            _currentWalkSpeed = maxWalkSpeed;
            _gameScreenView.crouchButton.onClick.AddListener(CrouchGetUp);
        }

        private void Update()
        {
            GetTouchInput();
            if (_rightFingerID != -1)
                LookAround();
        }

        private void FixedUpdate()
        {
            Walk();
            Gravity();
        }

        private void GetTouchInput()
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var t = Input.GetTouch(i);
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        if (t.position.x > Screen.width / 2f && _rightFingerID == -1)
                            _rightFingerID = t.fingerId;
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (t.fingerId == _rightFingerID)
                        {
                            //Stop tracking the left finger
                            _rightFingerID = -1;
                        }

                        break;
                    case TouchPhase.Moved:
                        if (t.fingerId == _rightFingerID) 
                            _lookInput = t.deltaPosition * (_sensitivity * Time.deltaTime);

                        break;
                    case TouchPhase.Stationary:
                        if (t.fingerId == _rightFingerID) 
                            _lookInput = Vector2.zero;
                        break;
                }
            }
        }

        private void LookAround()
        {
            _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, -90, 90);
            _camera.transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
            transform.Rotate(transform.up, _lookInput.x);
        }

        private void Gravity()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, groundMask);
            if (_isGrounded)
                _velocity.y = -0.2f;
            else
                _velocity.y -= GravityForce * Time.deltaTime * Time.deltaTime;
            characterController.Move(_velocity);
        }
        
        private void Walk()
        {
            _moveDirection = Vector3.zero;

            if (_inputService.Axis.LengthSquared() > Constants.Epsilon)
            {
                
            }
            _moveDirection.x = _inputService.Axis.X;
            _moveDirection.z = _inputService.Axis.Y;  
            var transform1 = transform;
            _moveDirection = transform1.right * _moveDirection.x + transform1.forward * _moveDirection.z +
                             transform1.up * _moveDirection.y;
            
            characterController.Move(_moveDirection * (_currentWalkSpeed * Time.deltaTime));
        }

        private void CrouchGetUp()
        {
            if (characterController.isGrounded && !_isCrouching)
            {
                _isCrouching = true;
                characterController.height = playerCrouchHeight;
                characterController.center = new Vector3(0f, playerCrouchHeight/2, 0f);
                _camera.transform.DOLocalMove(_cameraCrouchPosition, 0.25f);
                _currentWalkSpeed = maxWalkSpeed / 2;
            }
            else if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), playerStandHeight))
            {
                _isCrouching = false;
                characterController.height = playerStandHeight;
                characterController.center = new Vector3(0f, playerStandHeight/2, 0f);
                _camera.transform.DOLocalMove(_cameraStandPosition, 0.25f);
                _currentWalkSpeed = maxWalkSpeed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.GetComponent<ShelterZone>()) 
                OnPlayerInsideShelter?.Invoke();
        }

        private void OnDisable()
        {
            _gameScreenView.crouchButton.onClick.RemoveListener(CrouchGetUp);
        }
    }
}