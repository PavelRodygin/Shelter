using DefaultNamespace.Interfaces;
using Interfaces;
using UI;
using UnityEngine;
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
        }

        private void OpenClose()
        {
            if (Vector3.Dot(camera.transform.forward, 
                    _currentOpenClosable.PointToLook.position - transform.position) < 0.8)
            {
                _gameScreen.openButton.gameObject.SetActive(false);
                _gameScreen.closeButton.gameObject.SetActive(false);
                _buttonsShowed = false;
            }
            else  if(!_buttonsShowed && Vector3.Dot(camera.transform.forward, 
                         _currentOpenClosable.PointToLook.position - transform.position) > 0.8)               
            {
                bool isOpen = _currentOpenClosable.IsOpen;
                if (isOpen)
                {
                    _gameScreen.openButton.gameObject.SetActive(false);
                    _gameScreen.closeButton.gameObject.SetActive(true);
                    _buttonsShowed = true;
                }
                else if (!isOpen)
                {
                    _gameScreen.closeButton.gameObject.SetActive(false);
                    _gameScreen.openButton.gameObject.SetActive(true);
                    _buttonsShowed = true;
                }
            }
        }
        
        private void Interact()
        {
            if (Vector3.Dot(camera.transform.forward,
                    _currentInteractable.PointToLook.position - transform.position) > 0.95 )
            {
                _gameScreen.interactButton.gameObject.SetActive(false);
                _buttonsShowed = false;
            }
            else if(Vector3.Dot(camera.transform.forward, _currentInteractable.PointToLook.position - transform.position) < 0.95 && !_buttonsShowed)
            {
                _gameScreen.interactButton.gameObject.SetActive(false);
                _gameScreen.interactButton.gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out IOpenClosable openClosable))
            {
                _currentOpenClosable = openClosable;
                _gameScreen.openButton.onClick.AddListener(_currentOpenClosable.Open);
                _gameScreen.closeButton.onClick.AddListener(_currentOpenClosable.Close);
                Debug.Log(_currentOpenClosable.PointToLook.position);
            }
            else if (other.transform.TryGetComponent(out IInteractable interactable))
            {
                _currentInteractable = interactable;
                _gameScreen.interactButton.onClick.AddListener(_currentInteractable.Interact);
                Debug.Log(_currentInteractable.PointToLook.position);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IOpenClosable>() != null) 
            {
                _gameScreen.openButton.onClick.RemoveListener(_currentOpenClosable.Open);
                _gameScreen.closeButton.onClick.RemoveListener(_currentOpenClosable.Close);
                _gameScreen.openButton.gameObject.SetActive(false);
                _gameScreen.closeButton.gameObject.SetActive(false);
                _currentOpenClosable = null;
                _buttonsShowed = false;  
            }
            else if (other.GetComponentInParent<IInteractable>() != null)
            {
                _gameScreen.interactButton.onClick.RemoveListener(_currentInteractable.Interact);
                _gameScreen.interactButton.gameObject.SetActive(false);
                _currentInteractable = null;
                _buttonsShowed = false;
            }
        }
    }
}