using Core.GameInterfaces;
using Core.Gameplay.AbstractClasses;
using Core.Gameplay.Items;
using Modules.GameScreen.Scripts;
using StarterAssets;
using UnityEngine;

namespace Core.Gameplay.PlayerScripts
{
    public class PlayerItemManager : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private float throwForce = 5;
        [SerializeField] private StarterAssetsInputs _starterAssetsInputs;

        private GameScreenView _gameScreenView;
        private Camera _camera;
        private IItem _item;

        public void Initialize(GameScreenView gameScreenView, Camera camera)
        {
            _gameScreenView = gameScreenView;
            _camera = camera;

            // Привязываем руку к камере
            var handStockPos = hand.transform.localPosition;
            hand.parent = _camera.transform;
            hand.transform.rotation = _camera.transform.rotation;

            // Убедимся, что на старте предмет в руке корректно обработан
            if (hand.GetComponentInChildren<IItem>() != null)
            {
                _item = hand.GetComponentInChildren<IItem>();
                _item.GetGrabbed(hand);
            }
        }

        private void Update()
        {
            // Проверяем ввод на выброс предмета
            if (_starterAssetsInputs.dropItem)
            {
                ThrowItem();
                _starterAssetsInputs.dropItem = false; // Сбрасываем состояние ввода
            }
        }

        public void GrabItem(IItem item)
        {
            if (item != null)
            {
                // Если уже есть предмет, выбрасываем его
                if (_item != null)
                {
                    ThrowItem();
                }

                // Поднимаем новый предмет
                _item = item;
                _item.GetGrabbed(hand);

                Debug.Log("Предмет успешно поднят!");
            }
        }

        public void ThrowItem()
        {
            if (_item != null)
            {
                // Выбрасываем предмет с заданной силой в направлении камеры
                _item.GetThrown(_camera.transform.forward.normalized * throwForce);
                _item = null;

                Debug.Log("Предмет выброшен!");
            }
            else
            {
                Debug.Log("Нет предмета для выброса.");
            }
        }
    }
}
