using Core.AbstractClasses;
using Core.GameInterfaces;
using Interfaces;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    public class PlayerInteractModule : MonoBehaviour
    {
        [Inject] Camera _camera;
        private GameScreenUIView _gameScreenUIView;
        private IOpenClosable _currentDoor; 
        private IInteractable _currentInteractable;
        private Item _currentItem;
        private Pockets _pockets;
        private bool _interactButtonShowed;
        
        public void Initialize(GameScreenUIView gameScreenUIView)
        {
            _camera = Camera.main;  //TODO tododoodod!!!!!!!!
            _gameScreenUIView = gameScreenUIView;
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

        private void OpenClose()
        {
            if(!_interactButtonShowed && Vector3.Dot(_camera.transform.forward, 
                   _currentDoor.PointToLook.position - _camera.transform.position) > 0.8)               
            {
                if (_currentDoor.IsOpen)
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _interactButtonShowed = true;
                }
                else 
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _interactButtonShowed = true;
                }
            }
            else if (Vector3.Dot(_camera.transform.forward, 
                         _currentDoor.PointToLook.position - transform.position) < 0.8) 
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(false);  
                _interactButtonShowed = false;
            }
        }
        
        private void Interact()
        {
            if (Vector3.Dot(_camera.transform.forward,
                    _currentInteractable.PointToLook.position - transform.position) > 0.8 )
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _interactButtonShowed = false;
            }
            else if(Vector3.Dot(_camera.transform.forward, 
                        _currentInteractable.PointToLook.position - transform.position) < 0.8) 
            {
                _interactButtonShowed = false;
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
            }
        }

        private void FindItem()
        {
            if(Vector3.Dot(_camera.transform.forward, _currentItem.Transform.position - transform.position) > 0.8 && !_interactButtonShowed)
            {
                _interactButtonShowed = true; 
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
            }
            else if(Vector3.Dot(_camera.transform.forward, _currentItem.Transform.position - transform.position) < 0.8)
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _interactButtonShowed = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out _currentDoor))
            {
                _gameScreenUIView.interactButton.onClick.AddListener(_currentDoor.OpenClose);
            }
            else if (other.TryGetComponent(out _currentInteractable))
            {
                _gameScreenUIView.interactButton.onClick.AddListener(_currentInteractable.Interact);
            }
            else if (other.transform.parent.TryGetComponent(out _currentItem))
            {   
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _gameScreenUIView.interactButton.onClick.AddListener(() => _pockets.GrabItem(_currentItem)); 
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IOpenClosable>() != null) 
            {
                if (_currentDoor != null)
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _gameScreenUIView.interactButton.onClick.RemoveAllListeners();
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
            else if (other.GetComponentInParent<IItem>() != null)
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _gameScreenUIView.interactButton.onClick.RemoveListener(() => _pockets.GrabItem(_currentItem));
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _currentItem = null;
            }
        }
    }
}