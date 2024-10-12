using Core.GameInterfaces;
using Core.Gameplay.AbstractClasses;
using Cysharp.Threading.Tasks;
using Modules.GameScreen.Scripts;
using UnityEngine;

namespace Core.Gameplay.PlayerScripts
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private float minLookCosine = 0.9f; // D(cos) = [-1;1]
        private Camera _camera;
        private GameScreenView _gameScreenView;
        private PlayerItemManager _playerItemManager;
        private IOpenClosable _currentDoor; 
        private IInteractable _currentInteractable;
        private Item _currentItem;
        private bool _interactButtonShowed;

        public void Initialize(GameScreenView gameScreenView, Camera camera)
        {
            _camera = camera;
            _gameScreenView = gameScreenView;
            _playerItemManager = GetComponent<PlayerItemManager>();
        }
        
        private void Update()
        {
            if (_currentDoor != null) OpenClose();
            else if (_currentInteractable != null) Interact();
            else if (_currentItem != null) FindItem();
        }

        private async void OpenClose()
        {
            if(!_interactButtonShowed && CheckLookToObject(_currentDoor.PointToLook))               
            {
                while (_currentDoor is { IsInteractable: false })
                    await UniTask.Yield();
                _gameScreenView.interactButton.gameObject.SetActive(true);
                _interactButtonShowed = true;
            }
            else if (!CheckLookToObject(_currentDoor.PointToLook)) 
            {
                _gameScreenView.interactButton.gameObject.SetActive(false);  
                _interactButtonShowed = false;
            }
        }
        
        private void Interact()
        {
            if (CheckLookToObject(_currentInteractable.PointToLook))
            {
                _gameScreenView.interactButton.gameObject.SetActive(true);
                _interactButtonShowed = false;
            }
            else if(!CheckLookToObject(_currentInteractable.PointToLook)) 
            {
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _interactButtonShowed = false;
            }
        }

        private void FindItem()
        {
            if(CheckLookToObject(_currentItem.transform))
            {
                _interactButtonShowed = true; 
                _gameScreenView.interactButton.gameObject.SetActive(true);
            }
            else if(!CheckLookToObject(_currentItem.transform))
            {
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _interactButtonShowed = false;
            }
        }

        private bool CheckLookToObject(Transform objectToLook)
        {
            var transform1 = _camera.transform;
            return Vector3.Dot(transform1.forward.normalized,   
                (objectToLook.position - transform1.position).normalized) > minLookCosine;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out _currentDoor))
                _gameScreenView.interactButton.onClick.AddListener(_currentDoor.OpenClose);
            
            else if (other.transform.parent.TryGetComponent(out _currentInteractable))
                _gameScreenView.interactButton.onClick.AddListener(_currentInteractable.Interact);
            else if (other.TryGetComponent(out _currentItem))
            {
                _gameScreenView.interactButton.gameObject.SetActive(true);
                _gameScreenView.interactButton.onClick.AddListener(OnItemFound); 
                _gameScreenView.interactButton.gameObject.SetActive(false);
            }
        }

        private void OnItemFound()
        {
            _playerItemManager.GrabItem(_currentItem);
            _currentItem = null;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IOpenClosable>() != null)
            {
                if (_currentDoor == null) return;
                _gameScreenView.interactButton.gameObject.SetActive(true);
                _gameScreenView.interactButton.onClick.RemoveListener(_currentDoor.OpenClose);
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _currentDoor = null;
                _interactButtonShowed = false;
            }
            else if (other.GetComponentInParent<IInteractable>() != null)
            {
                if (_currentInteractable == null) return;
                _gameScreenView.interactButton.gameObject.SetActive(true);
                _gameScreenView.interactButton.onClick.RemoveListener(_currentInteractable.Interact);
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _currentInteractable = null;
                _interactButtonShowed = false;
            }
            else if (other.GetComponent<IItem>() != null)
            {
                _gameScreenView.interactButton.gameObject.SetActive(true);
                _gameScreenView.interactButton.onClick.RemoveListener(OnItemFound);
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _currentItem = null;
            }
        }
    }
}