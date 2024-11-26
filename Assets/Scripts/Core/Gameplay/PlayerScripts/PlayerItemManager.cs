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
        [SerializeField] private StarterAssetsInputs starterAssetsInputs;
        
        private GameScreenView _gameScreenView;
        private Camera _camera;
        private Item _item;

        public void Initialize(GameScreenView gameScreenView, Camera camera)
        {
            _gameScreenView = gameScreenView;
            _camera = camera;

            // Привязываем руку к камере
            var handStockPos = hand.transform.localPosition;
            hand.parent = _camera.transform;
            hand.localPosition = handStockPos;

            // Изначально берем предмет, если он есть в руке
            _item = hand.GetComponentInChildren<Smartphone>();
            if (_item != null)
            {
                _item.Grab(hand);
            }
        }
        
        // private void Update()
        // {
        //     // Проверяем ввод игрока
        //     if (starterAssetsInputs.interact)
        //     {
        //         TryGrabItem();
        //         starterAssetsInputs.interact = false; // Сбрасываем флаг
        //     }
        //
        //     if (starterAssetsInputs.dropItem)
        //     {
        //         ThrowItem();
        //         starterAssetsInputs.dropItem = false; // Сбрасываем флаг
        //     }
        // }

        // private void TryGrabItem()
        // {
        //     // Логика для взятия предмета (можно добавить проверку на предмет перед игроком)
        //     if (_item == null)
        //     {
        //         Debug.Log("Нет предмета для взаимодействия.");
        //         return;
        //     }
        //
        //     GrabItem(_item);
        // }

        public void GrabItem(Item item)
        {
            if (item != null)
            {
                if (_item != null)
                {
                    ThrowItem();
                }

                _item = item;
                _item.Grab(hand);
                
                Debug.Log("Предмет успешно поднят!");
            }
        }

        public void ThrowItem()
        {
            if (_item != null)
            {
                _item.Throw(_camera.transform.forward.normalized * throwForce);
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
