using System;
using System.Collections.Generic;
using Interfaces;
using Items;
using UI;
using UnityEngine;
using Zenject;

namespace PlayerScripts
{
    public class Pockets : MonoBehaviour
    {
        [Inject] private GameScreen _gameScreen;
        private IItem _currentItem;
        [SerializeField] private Hand hand;
        [SerializeField] private Transform handPosition;
        [SerializeField] private float throwForce = 5;

        
        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            _currentItem = hand.GetComponentInChildren<Smartphone>();
            _currentItem.Grab(handPosition);
            _gameScreen.grabButton.onClick.RemoveListener(() => GrabItem(_currentItem));
            _gameScreen.grabButton.gameObject.SetActive(false);
            _gameScreen.throwButton.gameObject.SetActive(true);
            _gameScreen.throwButton.onClick.AddListener(() => ThrowItem(_currentItem));
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
                _gameScreen.grabButton.onClick.RemoveListener(() => GrabItem(_currentItem));
                _gameScreen.grabButton.gameObject.SetActive(false);
                _gameScreen.throwButton.gameObject.SetActive(true);
                _gameScreen.throwButton.onClick.AddListener(() => ThrowItem(_currentItem));
            }
            else
            {
                Debug.LogError("Предмет который мы пытаемся забрать - NULL");
            }
        }

        public void ThrowItem(IItem item)
        {
            _gameScreen.throwButton.onClick.RemoveListener(() => ThrowItem(_currentItem));
            _gameScreen.throwButton.gameObject.SetActive(false);
            _currentItem.Throw();
        }
    }
}