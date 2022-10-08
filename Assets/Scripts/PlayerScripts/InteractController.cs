using DefaultNamespace.Interfaces;
using Interfaces;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace PlayerScripts
{
    public class InteractController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [Inject] private GameScreen _gameScreen;
        private IOpenClosable _currentOpenClosable;
        private bool _buttonsShowed;
        private IInteractable _currentInteractable;
        private IItem _currentItem;
        private Pockets _pockets;


        private void Awake()
        {
            _pockets = gameObject.GetComponent<Pockets>();
        }
        
        private void Update()
        {
            if (_currentOpenClosable != null)
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
            if(!_buttonsShowed && Vector3.Dot(camera.transform.forward, 
                   _currentOpenClosable.PointToLook.position - transform.position) > 0.8)               
            {
                
                if (_currentOpenClosable.IsOpen)
                {
                    _gameScreen.closeButton.gameObject.SetActive(true);
                    _buttonsShowed = true;
                }
                else 
                {
                    _gameScreen.openButton.gameObject.SetActive(true);
                    _buttonsShowed = true;
                }
            }
            else if (Vector3.Dot(camera.transform.forward, 
                         _currentOpenClosable.PointToLook.position - transform.position) < 0.8) 
            {
                _gameScreen.openButton.gameObject.SetActive(false);  
                _gameScreen.closeButton.gameObject.SetActive(false);
                _buttonsShowed = false;
            }
        }
        
        private void Interact()
        {
            if (Vector3.Dot(camera.transform.forward,
                    _currentInteractable.PointToLook.position - transform.position) > 0.8 )
            {
                _gameScreen.interactButton.gameObject.SetActive(true);
                _buttonsShowed = false;
            }
            else if(Vector3.Dot(camera.transform.forward, 
                        _currentInteractable.PointToLook.position - transform.position) < 0.8) 
            {
                _buttonsShowed = false;
                _gameScreen.interactButton.gameObject.SetActive(false);
            }
        }

        private void FindItem()
        {
            if(Vector3.Dot(camera.transform.forward, _currentItem.Transform.position - transform.position) > 0.8 && !_buttonsShowed)
            {
                _buttonsShowed = true;
                _gameScreen.grabButton.gameObject.SetActive(true);
            }
            else if(Vector3.Dot(camera.transform.forward, _currentItem.Transform.position - transform.position) < 0.8)
            {
                _gameScreen.grabButton.gameObject.SetActive(false);
                _buttonsShowed = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out IOpenClosable openClosable))
            {
                _currentOpenClosable = openClosable;
                _gameScreen.openButton.onClick.AddListener(_currentOpenClosable.Open);
                _gameScreen.closeButton.onClick.AddListener(_currentOpenClosable.Close);
            }
            else if (other.TryGetComponent(out IInteractable interactable))
            {
                _currentInteractable = interactable;
                _gameScreen.interactButton.onClick.AddListener(_currentInteractable.Interact);
            }
            else if (other.transform.parent.TryGetComponent(out IItem item))
            {   
                _currentItem = other.GetComponentInChildren<IItem>();
                _gameScreen.grabButton.gameObject.SetActive(true);
                _gameScreen.grabButton.onClick.AddListener(() => _pockets.GrabItem(_currentItem)); 
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IOpenClosable>() != null) 
            {
                if (_currentOpenClosable != null)
                {
                    _gameScreen.openButton.gameObject.SetActive(true);
                    _gameScreen.closeButton.gameObject.SetActive(true);    //TODO Нельзя ремувать все
                    _gameScreen.openButton.onClick.RemoveListener(_currentOpenClosable.Open);
                    _gameScreen.closeButton.onClick.RemoveListener(_currentOpenClosable.Close);
                    _gameScreen.openButton.gameObject.SetActive(false);
                    _gameScreen.closeButton.gameObject.SetActive(false);
                    _currentOpenClosable = null;
                    _buttonsShowed = false;  
                }
            }
            else if (other.GetComponentInParent<IInteractable>() != null)
            {
                if (_currentInteractable != null)
                {
                    _gameScreen.interactButton.gameObject.SetActive(true);
                    _gameScreen.interactButton.onClick.RemoveListener(_currentInteractable.Interact);
                    _gameScreen.interactButton.gameObject.SetActive(false);
                    _currentInteractable = null;
                    _buttonsShowed = false;
                }
            }
            else if (other.GetComponentInParent<IItem>() != null)
            {
                _gameScreen.grabButton.gameObject.SetActive(true);
                _gameScreen.grabButton.onClick.RemoveListener(() => _pockets.GrabItem(_currentItem));
                _gameScreen.grabButton.gameObject.SetActive(false);
                _currentItem = null;
            }
        }
    }
}