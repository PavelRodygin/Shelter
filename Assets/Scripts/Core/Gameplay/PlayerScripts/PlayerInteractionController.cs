using Core.GameInterfaces;
using Core.Gameplay.AbstractClasses;
using Cysharp.Threading.Tasks;
using Modules.GameScreen.Scripts;
using StarterAssets;
using UnityEngine;

namespace Core.Gameplay.PlayerScripts
{
    //TODO Убрать Работу с кнопками
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs starterAssetsInputs;
        [SerializeField] private float minLookCosine = 0.9f; // D(cos) = [-1;1]
        private Camera _camera;
        private GameScreenView _gameScreenView;
        private PlayerItemManager _playerItemManager;
        private IOpenClosable _currentDoor; 
        private IInteractable _currentInteractable;
        private Item _currentItem;
        private bool _canInteract;

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
            // Проверяем, смотрит ли игрок на дверь и может ли взаимодействовать
            if (CheckLookToObject(_currentDoor.PointToLook))
            {
                // Отображаем кнопку взаимодействия
                _gameScreenView.interactButton.gameObject.SetActive(true);

                // Ожидаем, пока дверь станет интерактивной
                while (_currentDoor is { IsInteractable: false })
                {
                    await UniTask.Yield();
                }

                // Если игрок нажал кнопку взаимодействия
                if (starterAssetsInputs.interact)
                {
                    _currentDoor?.OpenClose(); // Выполняем действие
                    starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                }

                _canInteract = true; // Разрешаем дальнейшее взаимодействие
            }
            else
            {
                // Если игрок больше не смотрит на дверь, скрываем кнопку взаимодействия
                _gameScreenView.interactButton.gameObject.SetActive(false);
                starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                _canInteract = false; // Запрещаем дальнейшее взаимодействие
            }
        }

        
        private void Interact()
        {
            // Проверяем, смотрит ли игрок на объект
            if (CheckLookToObject(_currentInteractable.PointToLook))
            {
                // Отображаем кнопку взаимодействия
                _gameScreenView.interactButton.gameObject.SetActive(true);

                // Если игрок нажал кнопку взаимодействия
                if (starterAssetsInputs.interact)
                {
                    _currentInteractable.Interact(); // Выполняем действие
                    starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                }

                _canInteract = true; // Разрешаем дальнейшее взаимодействие
            }
            else
            {
                // Если игрок больше не смотрит на объект, скрываем кнопку взаимодействия
                _gameScreenView.interactButton.gameObject.SetActive(false);
                starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                _canInteract = false; // Запрещаем дальнейшее взаимодействие
            }
        }


        private void FindItem()
        {
            if(CheckLookToObject(_currentItem.transform))
            {
                _canInteract = true; 
                _gameScreenView.interactButton.gameObject.SetActive(true);
            }
            else if(!CheckLookToObject(_currentItem.transform))
            {
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _canInteract = false;
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
            {
                // _gameScreenView.interactButton.onClick.AddListener(_currentDoor.OpenClose);
            }

            else if (other.transform.parent.TryGetComponent(out _currentInteractable))
            {
                // _gameScreenView.interactButton.onClick.AddListener(_currentInteractable.Interact);
            }

            else if (other.TryGetComponent(out _currentItem))
            {
                // _gameScreenView.interactButton.gameObject.SetActive(true);
                // _gameScreenView.interactButton.onClick.AddListener(OnItemFound); 
                // _gameScreenView.interactButton.gameObject.SetActive(false);
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
                // _gameScreenView.interactButton.gameObject.SetActive(true);
                // _gameScreenView.interactButton.onClick.RemoveListener(_currentDoor.OpenClose);
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _currentDoor = null;
                _canInteract = false;
            }
            else if (other.GetComponentInParent<IInteractable>() != null)
            {
                if (_currentInteractable == null) return;
                // _gameScreenView.interactButton.gameObject.SetActive(true);
                // _gameScreenView.interactButton.onClick.RemoveListener(_currentInteractable.Interact);
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _currentInteractable = null;
                _canInteract = false;
            }
            else if (other.GetComponent<IItem>() != null)
            {
                // _gameScreenView.interactButton.gameObject.SetActive(true);
                // _gameScreenView.interactButton.onClick.RemoveListener(OnItemFound);
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _currentItem = null;
            }
        }
    }
}