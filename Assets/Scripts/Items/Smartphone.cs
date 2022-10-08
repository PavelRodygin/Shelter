using System.Buffers;
using Interfaces;
using UnityEngine;

namespace Items
{
    public class Smartphone : MonoBehaviour, IItem
    {
        private Level _level;
        private Rigidbody _rigidbody;
        public Transform Transform => transform;
        
        
        
        private void Awake()
        {
            _level = FindObjectOfType<Level>();
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void Grab(Transform owner)
        {
            _rigidbody.isKinematic = true;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
            gameObject.transform.position = owner.position;
            gameObject.transform.rotation = owner.rotation;
            gameObject.transform.parent = owner;
        }
        
        public void Throw()
        {
            _rigidbody.isKinematic = false;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            gameObject.transform.parent = _level.transform;
        }
    }
}