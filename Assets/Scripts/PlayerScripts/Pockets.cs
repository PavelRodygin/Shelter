using System.Collections.Generic;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace PlayerScripts
{
    public class Pockets : MonoBehaviour
    {
        private List<IItem> _items;
        private IItem _currentItem;
        [SerializeField] private Vector3 handPosition;


        // public void SwitchItem()
        // {
        //     _currentItem.gameObject.SetActive(false);
        //     _currentItem = _items[0];
        // }
        //
        //
        // public void GrabItem(IItem item)
        // {           
        //     _currentItem.GetComponent<Rigidbody>().useGravity = false;
        //     _currentItem.transform.position = handPosition;
        // }
        //
        // public void ThrowItem()
        // {
        //     _items.Remove(_currentItem);
        //     _currentItem.GetComponent<Rigidbody>().useGravity = true;
        // }
    }
}