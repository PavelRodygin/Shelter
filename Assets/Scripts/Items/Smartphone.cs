using System.Buffers;
using Interfaces;
using UnityEngine;

namespace Items
{
    public class Smartphone : MonoBehaviour, IItem
    {
        private Level _level;
        [SerializeField] private Rigidbody _rigidbody;
        public Transform Transform => transform;
        
        
        
        private void Awake()
        {
            _level = FindObjectOfType<Level>();
        }
        
        public void Grab(Transform owner)
        {
            _rigidbody.isKinematic = true;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            transform.rotation = owner.rotation;
            transform.parent = owner;
            transform.localPosition = Vector3.zero;
        }
        
        public void Throw()
        {
            _rigidbody.isKinematic = false;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = true;
            gameObject.transform.parent = _level.transform;
        }
    }
}