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
        // [SerializeField] private StarterAssetsInputs starterAssetsInputs;
        [SerializeField] private float minLookCosine = 0.9f; // D(cos) = [-1;1]
        private Camera _camera;
        private GameScreenView _gameScreenView;
        // private PlayerItemManager _playerItemManager;
        private IOpenClosable _currentDoor; 
        private IInteractable _currentInteractable;
        private IItem _currentItem;
        private bool _canInteract;

        public void Initialize(GameScreenView gameScreenView, Camera camera)
        {
            _camera = camera;
            _gameScreenView = gameScreenView;
            // _playerItemManager = GetComponent<PlayerItemManager>();
        }
        
        private void Update()
        {
            if (_currentDoor != null) OpenClose();
            if (_currentItem != null) FindItem();
            if (_currentInteractable != null) Interact();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out _currentDoor))
            {
                //Here may be a logic for UI buttons
            }

            if (other.transform.parent.TryGetComponent(out _currentInteractable))
            {
                //Here may be a logic for UI buttons
            }

            if (other.transform.parent.TryGetComponent(out _currentItem))
            {
                Debug.Log(_currentItem);
                //Here may be a logic for UI buttons
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<IOpenClosable>() != null)
            {
                if (_currentDoor == null) return;
           
                _gameScreenView.interactTipView.gameObject.SetActive(false);
                _currentDoor = null;
                _canInteract = false;
            }
            else if (other.GetComponentInParent<IInteractable>() != null)
            {
                if (_currentInteractable == null) return;
              
                _gameScreenView.interactTipView.gameObject.SetActive(false);
                _currentInteractable = null;
                _canInteract = false;
            }
            else if (other.GetComponentInParent<IItem>() != null)
            {
                _gameScreenView.interactTipView.gameObject.SetActive(false);
                _currentItem = null;
            }
        }

        private async void OpenClose()
        {
            // Проверяем, смотрит ли игрок на дверь и может ли взаимодействовать
            if (CheckLookToObject(_currentDoor.PointToLook))
            {
                // Отображаем кнопку взаимодействия
                _gameScreenView.interactTipView.gameObject.SetActive(true);

                // Ожидаем, пока дверь станет интерактивной
                while (_currentDoor is { IsInteractable: false })
                {
                    await UniTask.Yield();
                }

                // Если игрок нажал кнопку взаимодействия
                // if (starterAssetsInputs.interact)
                // {
                //     _currentDoor?.OpenClose(); // Выполняем действие
                //     starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                // }

                _canInteract = true; // Разрешаем дальнейшее взаимодействие
            }
            else
            {
                // Если игрок больше не смотрит на дверь, скрываем кнопку взаимодействия
                _gameScreenView.interactTipView.gameObject.SetActive(false);
                // starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                _canInteract = false; // Запрещаем дальнейшее взаимодействие
            }
        }
        
        private void Interact()
        {
            // Проверяем, смотрит ли игрок на объект
            if (CheckLookToObject(_currentInteractable.PointToLook))
            {
                // Отображаем кнопку взаимодействия
                _gameScreenView.interactTipView.gameObject.SetActive(true);

                // Если игрок нажал кнопку взаимодействия
                // if (starterAssetsInputs.interact)
                // {
                //     _currentInteractable.Interact(); // Выполняем действие
                //     starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                // }

                _canInteract = true; // Разрешаем дальнейшее взаимодействие
            }
            else
            {
                // Если игрок больше не смотрит на объект, скрываем кнопку взаимодействия
                _gameScreenView.interactTipView.gameObject.SetActive(false);
                // starterAssetsInputs.interact = false; // Сбрасываем флаг взаимодействия
                _canInteract = false; // Запрещаем дальнейшее взаимодействие
            }
        }
        
        private void FindItem()
        {
            Debug.Log(_currentItem + "метод FindItem началася");

            if (CheckLookToObject(_currentItem.Transform))
            {
                _canInteract = true;
                _gameScreenView.interactTipView.gameObject.SetActive(true);

                // Взаимодействие с предметом
                // if (starterAssetsInputs.interact)
                // {
                //     _playerItemManager.GrabItem(_currentItem);
                //     // starterAssetsInputs.interact = false;
                //     _currentItem = null; // Убираем текущий предмет
                // }
            }
            else
            {
                _gameScreenView.interactTipView.gameObject.SetActive(false);
                _canInteract = false;
            }
        }



        private bool CheckLookToObject(Transform objectToLook)
        {
            var transform1 = _camera.transform;
            return Vector3.Dot(transform1.forward.normalized,   
                (objectToLook.position - transform1.position).normalized) > minLookCosine;
        }
    }
}