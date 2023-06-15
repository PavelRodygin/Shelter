using Core.AbstractClasses;
using GameScripts.Items;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    public class Pockets : MonoBehaviour
    {
        private GameScreenUIView _gameScreenUIView;
        [Inject] private Camera _camera;
        //[SerializeField] private EmptyHand hand;
        [SerializeField] private Transform handPosition;
        private float _throwForce = 5;
        private Item _currentItem;

        public void Initialize(GameScreenUIView gameScreenUIView)
        {
            _gameScreenUIView = gameScreenUIView;
            _camera = Camera.main;                                  //TODO Камера!!!!!!!!!
            handPosition.transform.parent = _camera.transform;
            _currentItem = handPosition.GetComponentInChildren<Smartphone>();
            _currentItem.Grab(handPosition);
            // _gameScreenUIView.grabButton.onClick.RemoveListener(() => GrabItem(_currentItem));
            // _gameScreenUIView.grabButton.gameObject.SetActive(false);
            _gameScreenUIView.dropButton.gameObject.SetActive(true);
            _gameScreenUIView.dropButton.onClick.AddListener(() => ThrowItem(_currentItem));
        }
        
        public void GrabItem(Item item)
        {
            if (item != null)
            {
                if (_currentItem != null)
                    ThrowItem(_currentItem);
                _currentItem = item;
                _currentItem.Grab(handPosition);
                _gameScreenUIView.interactButton.onClick.RemoveListener(() => GrabItem(_currentItem));
                _gameScreenUIView.interactButton.gameObject.SetActive(false);
                _gameScreenUIView.dropButton.gameObject.SetActive(true);
                _gameScreenUIView.dropButton.onClick.AddListener(() => ThrowItem(_currentItem));
            }
        }

        public void ThrowItem(Item item)
        {
            _gameScreenUIView.dropButton.onClick.RemoveListener(() => ThrowItem(_currentItem));
            _gameScreenUIView.dropButton.gameObject.SetActive(false);
            _currentItem.Throw();
        }
    }
}