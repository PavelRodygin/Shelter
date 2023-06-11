using Core.AbstractClasses;
using Core.GameInterfaces;
using Interfaces;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    public class PlayerInteractController : MonoBehaviour
    {
        [Inject] private GameScreenUIView _gameScreenUIView;
        [Inject] Camera _playerCamera;
        private IOpenClosable _currentDoor; 
        private IInteractable _currentInteractable;
        private Item _currentItem;
        private Pockets _pockets;
        private bool _buttonsShowed;

        private void FixedUpdate()
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
            if(!_buttonsShowed && Vector3.Dot(_playerCamera.transform.forward, 
                   _currentDoor.PointToLook.position - transform.position) > 0.8)               
            {
                if (_currentDoor.IsOpen)
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _buttonsShowed = true;
                }
                else 
                {
                    _gameScreenUIView.interactButton.gameObject.SetActive(true);
                    _buttonsShowed = true;
                }
            }
            else if (Vector3.Dot(_playerCamera.transform.forward, 
                         _currentDoor.PointToLook.position - transform.position) < 0.8) 
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(false);  
                _buttonsShowed = false;
            }
        }
        
        private void Interact()
        {
            if (Vector3.Dot(_playerCamera.transform.forward,
                    _currentInteractable.PointToLook.position - transform.position) > 0.8 )
            {
                _gameScreenUIView.interactButton.gameObject.SetActive(true);
                _buttonsShowed = false;
            }
            else if(Vector3.Dot(_playerCamera.transform.forward, 
                        _currentInteractable.PointToLook.position - transform.position) < 0.8) 
            {
                _buttonsShowed = false;
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
            }
        }

        private void FindItem()
        {
            if(Vector3.Dot(_playerCamera.transform.forward, _currentItem.Transform.position - transform.position) > 0.8 && !_buttonsShowed)
            {
                _buttonsShowed = true;
                _gameScreenUIView.grabButton.gameObject.SetActive(true);
            }
            else if(Vector3.Dot(_playerCamera.transform.forward, _currentItem.Transform.position - transform.position) < 0.8)
            {
                _gameScreenUIView.grabButton.gameObject.SetActive(false);
                _buttonsShowed = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out IOpenClosable openClosable))
            {
                _currentDoor = openClosable;
                if (_currentDoor.IsOpen)
                    _gameScreenUIView.interactButton.onClick.AddListener(_currentDoor.OpenClose);
            }
            else if (other.TryGetComponent(out IInteractable interactable))
            {
                _currentInteractable = interactable;
                _gameScreenUIView.interactButton.onClick.AddListener(_currentInteractable.Interact);
            }
            else if (other.transform.parent.TryGetComponent(out IItem item))
            {   
                _currentItem = other.GetComponentInChildren<Item>();
                _gameScreenUIView.grabButton.gameObject.SetActive(true);
                _gameScreenUIView.grabButton.onClick.AddListener(() => _pockets.GrabItem(_currentItem)); 
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
                    _buttonsShowed = false;  
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
                    _buttonsShowed = false;
                }
            }
            else if (other.GetComponentInParent<IItem>() != null)
            {
                _gameScreenUIView.grabButton.gameObject.SetActive(true);
                _gameScreenUIView.grabButton.onClick.RemoveListener(() => _pockets.GrabItem(_currentItem));
                _gameScreenUIView.grabButton.gameObject.SetActive(false);
                _currentItem = null;
            }
        }
    }
}