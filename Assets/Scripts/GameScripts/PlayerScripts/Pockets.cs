using Interfaces;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    public class Pockets : MonoBehaviour
    {
        [Inject] private GameScreenUIView _gameScreenUIView;
        [SerializeField] private EmptyHand hand;
        [SerializeField] private Transform handPosition;
        [SerializeField] private float throwForce = 5;
        private IItem _currentItem;
        
        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            //_currentItem = hand.GetComponentInChildren<Smartphone>();
            _currentItem.Grab(handPosition);
            _gameScreenUIView.grabButton.onClick.RemoveListener(() => GrabItem(_currentItem));
            _gameScreenUIView.grabButton.gameObject.SetActive(false);
            _gameScreenUIView.throwButton.gameObject.SetActive(true);
            _gameScreenUIView.throwButton.onClick.AddListener(() => ThrowItem(_currentItem));
        }
        
        public void GrabItem(IItem item)
        {
            if (item != null)
            {
                if (_currentItem != null)
                {
                    ThrowItem(_currentItem);
                }
                _currentItem = item;
                _currentItem.Grab(handPosition);
                _gameScreenUIView.grabButton.onClick.RemoveListener(() => GrabItem(_currentItem));
                _gameScreenUIView.grabButton.gameObject.SetActive(false);
                _gameScreenUIView.throwButton.gameObject.SetActive(true);
                _gameScreenUIView.throwButton.onClick.AddListener(() => ThrowItem(_currentItem));
            }
            else
            {
                Debug.LogError("Предмет который мы пытаемся забрать - NULL");
            }
        }

        public void ThrowItem(IItem item)
        {
            _gameScreenUIView.throwButton.onClick.RemoveListener(() => ThrowItem(_currentItem));
            _gameScreenUIView.throwButton.gameObject.SetActive(false);
            _currentItem.Throw();
        }
    }
}