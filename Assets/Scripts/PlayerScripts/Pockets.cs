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
        private List<IItem> _items;
        private IItem _currentItem;
        [SerializeField] private Hand hand;
        [SerializeField] private Transform handPosition;
        [SerializeField] private float throwForce = 5;
        private int _index;


        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            _items = new List<IItem>();
            _items.Add(hand);
            _currentItem = hand.GetComponentInChildren<Smartphone>();
            GrabItem(_currentItem);
            _gameScreen.throwButton.gameObject.SetActive(true);
            _gameScreen.throwButton.onClick.AddListener(()=>ThrowItem(_currentItem));
            _gameScreen.switchButton.gameObject.SetActive(true);
            _gameScreen.switchButton.onClick.AddListener(SwitchItem);
        }
        
        public void SwitchItem()
        {
            if (_index < _items.Count)
            {
                _index++;
            }
            else
            {
                _index = 0;
            }
            _currentItem.Transform.gameObject.SetActive(false);
            _currentItem = _items[_index];
            _currentItem.Transform.gameObject.SetActive(true);
            _gameScreen.throwButton.onClick.AddListener(() => ThrowItem(_currentItem));
            _gameScreen.switchButton.gameObject.SetActive(true);
        }


        public void GrabItem(IItem item)
        {
            _currentItem.Grab(handPosition);
            _items.Add(_currentItem);
            _gameScreen.grabButton.onClick.RemoveListener(() => GrabItem(_currentItem));
            _gameScreen.grabButton.gameObject.SetActive(false);
            _gameScreen.throwButton.gameObject.SetActive(true);
            _gameScreen.throwButton.onClick.AddListener(() => ThrowItem(_currentItem));
            _gameScreen.switchButton.gameObject.SetActive(true);
            Debug.Log("Карманы взяли телефон");
        }

        public void ThrowItem(IItem item)
        {
            if (_currentItem != hand)
            {
                if(_items.Count == 2) //осталась только рука
                {
                    _gameScreen.switchButton.gameObject.SetActive(false);
                    _gameScreen.throwButton.onClick.RemoveListener(() => ThrowItem(_currentItem));
                    _gameScreen.throwButton.gameObject.SetActive(false);
                    _items.Remove(item);
                    _currentItem.Throw();
                    _currentItem = _items[0];
                    _currentItem.Transform.gameObject.SetActive(true);
                    GrabItem(_currentItem);
                    _gameScreen.throwButton.gameObject.SetActive(false);
                }
                else
                {
                    _items.Remove(item);
                    _currentItem.Throw();
                    _currentItem.Transform.gameObject.SetActive(true);
                    _gameScreen.throwButton.onClick.RemoveListener(() => ThrowItem(_currentItem));
                    _index--;
                    SwitchItem();
                }
               
            }
            else
            {
                Debug.Log("Нельзя выбросить руку");
            }
   
        }
    }
}