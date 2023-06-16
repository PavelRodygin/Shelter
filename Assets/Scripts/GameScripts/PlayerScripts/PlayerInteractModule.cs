using Core.AbstractClasses;
using Core.GameInterfaces;
using Cysharp.Threading.Tasks;
using UIModules.GameScreen.Scripts;
using UnityEngine;

namespace GameScripts.PlayerScripts
{
    public class PlayerInteractModule : MonoBehaviour
    {
        [SerializeField] private float minLookCosine = 0.9f; // D(cos) = [-1;1]
        private Camera _camera;
        private GameScreenUIView _gameScreenUIView;
        private Pockets _pockets;   //Items manager
        private IOpenClosable _currentDoor; 
        private IInteractable _currentInteractable;
        private Item _currentItem;
        private bool _interactButtonShowed;

        public void Initialize(GameScreenUIView gameScreenUIView, Camera camera)
        {
            _camera = camera;
            _gameScreenUIView = gameScreenUIView;
            _pockets = GetComponent<Pockets>();
        }
        
        private void Update()
        {
            if (_currentDoor != null)
            {
                OpenClose();
            }
            else if (_currentInteractable != null)
            {
                Interact();
            }
            else if (_currentItem != null)
            {
                FindItem();
            }
        }

        private async void OpenClose()
        {
            if(!_interactButtonShowed && CheckLookToObject(_currentDoor.PointToLook))               
            {
                while (!_currentDoor.IsInteractable)
                    await UniTask.Yield();
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _interactButtonShowed = true;
                
            }
            else if (!CheckLookToObject(_currentDoor.PointToLook)) 
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(false);  
                _interactButtonShowed = false;
            }
        }
        
        private void Interact()
        {
            if (CheckLookToObject(_currentInteractable.PointToLook))
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _interactButtonShowed = false;
            }
            else if(!CheckLookToObject(_currentInteractable.PointToLook)) 
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _interactButtonShowed = false;
            }
        }

        private void FindItem()
        {
            if(CheckLookToObject(_currentItem.transform))
            {
                _interactButtonShowed = true; 
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
            }
            else if(!CheckLookToObject(_currentItem.transform))
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _interactButtonShowed = false;
            }
        }

        private bool CheckLookToObject(Transform objectToLook)
        {
            if (Vector3.Dot(_camera.transform.forward.normalized,   
                    (objectToLook.position - _camera.transform.position).normalized) > minLookCosine)
                return true;
            
            return false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out _currentDoor))
            {
                _gameScreenUIView.interactButton.onClick.AddListener(_currentDoor.OpenClose);
            }
            else if (other.transform.parent.TryGetComponent(out _currentInteractable))
            {
                _gameScreenUIView.interactButton.onClick.AddListener(_currentInteractable.Interact);
            }
            else if (other.TryGetComponent(out _currentItem))
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _gameScreenUIView.interactButton.onClick.AddListener(OnItemFound); 
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
            }
        }

        private void OnItemFound()
        {
            _pockets.GrabItem(_currentItem);
            _currentItem = null;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IOpenClosable>() != null) 
            {
                if (_currentDoor != null)
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _gameScreenUIView.interactButton.onClick.RemoveListener(_currentDoor.OpenClose);
                    _gameScreenUIView.interactButton.gameObject.SetActive(false);
                    _currentDoor = null;
                    _interactButtonShowed = false;  
                }
            }
            else if (other.GetComponentInParent<IInteractable>() != null)
            {
                if (_currentInteractable != null)
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _gameScreenUIView.interactButton.onClick.RemoveListener(_currentInteractable.Interact);
                    _gameScreenUIView.interactButton.gameObject.SetActive(false);
                    _currentInteractable = null;
                    _interactButtonShowed = false;
                }
            }
            else if (other.GetComponent<IItem>() != null)
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _gameScreenUIView.interactButton.onClick.RemoveListener(OnItemFound);
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _currentItem = null;
            }
        }
    }
}