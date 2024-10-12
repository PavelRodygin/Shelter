using Core.Gameplay.AbstractClasses;
using Core.Gameplay.Items;
using Modules.GameScreen.Scripts;
using UnityEngine;

namespace Core.Gameplay.PlayerScripts
{
    public class PlayerItemManager : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private float throwForce = 5;
        private GameScreenView _gameScreenView;
        private Camera _camera;
        private Item _item;

        public void Initialize(GameScreenView gameScreenView, Camera camera)
        {
            _gameScreenView = gameScreenView;
            _camera = camera;
            var handStockPos = hand.transform.localPosition;
            hand.parent = _camera.transform;
            hand.localPosition = handStockPos;
            _item = hand.GetComponentInChildren<Smartphone>();
            _item.Grab(hand);
            _gameScreenView.interactButton.gameObject.SetActive(false);
            _gameScreenView.dropButton.gameObject.SetActive(true);
            _gameScreenView.dropButton.onClick.AddListener(ThrowItem);
        }
        
        public void GrabItem(Item item)
        {
            if (item != null)
            {
                if (_item != null)
                    ThrowItem();
                _item = item;
                _item.Grab(hand);
                _gameScreenView.interactButton.onClick.RemoveAllListeners();
                _gameScreenView.interactButton.gameObject.SetActive(false);
                _gameScreenView.dropButton.gameObject.SetActive(true);
                _gameScreenView.dropButton.onClick.AddListener(ThrowItem);
            }
        }

        public void ThrowItem()
        {
            _gameScreenView.dropButton.onClick.RemoveListener(ThrowItem);
            _gameScreenView.dropButton.gameObject.SetActive(false);
            _item.Throw(_camera.transform.forward.normalized * throwForce);
            _item = null;
        }
    }
}