using Core.AbstractClasses;
using GameScripts.Items;
using UIModules.GameScreen.Scripts;
using UnityEngine;

namespace GameScripts.PlayerScripts
{
    public class Pockets : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private Vector3 handPosition = new(0.3f, -0.2f, 0.5f);
        [SerializeField] private float throwForce = 5;
        private GameScreenUIView _gameScreenUIView;
        private Camera _camera;
        private Item _item;

        public void Initialize(GameScreenUIView gameScreenUIView, Camera camera)
        {
            _gameScreenUIView = gameScreenUIView;
            _camera = camera;                            
            hand.parent = _camera.transform;
            hand.localPosition = handPosition;
            _item = hand.GetComponentInChildren<Smartphone>();
            _item.Grab(hand);
            _gameScreenUIView.interactButton.gameObject.SetActive(false);
            _gameScreenUIView.dropButton.gameObject.SetActive(true);
            _gameScreenUIView.dropButton.onClick.AddListener(ThrowItem);
        }
        
        public void GrabItem(Item item)
        {
            if (item != null)
            {
                if (_item != null)
                    ThrowItem();
                _item = item;
                _item.Grab(hand);
                _gameScreenUIView.interactButton.onClick.RemoveAllListeners();
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _gameScreenUIView.dropButton.gameObject.SetActive(true);
                _gameScreenUIView.dropButton.onClick.AddListener(ThrowItem);
            }
        }

        public void ThrowItem()
        {
            _gameScreenUIView.dropButton.onClick.RemoveListener(ThrowItem);
            _gameScreenUIView.dropButton.gameObject.SetActive(false);
            _item.Throw(throwForce);
            _item = null;
        }
    }
}