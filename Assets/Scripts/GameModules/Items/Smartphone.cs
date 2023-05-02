using Interfaces;
using UnityEngine;

namespace GameModules.Items
{
    public class Smartphone : MonoBehaviour, IItem
    {
        private Level _level;
        [SerializeField] private new Rigidbody rigidbody;
        public Transform Transform => transform;
        
        
        private void Awake()
        {
            _level = FindObjectOfType<Level>();
        }
        
        public void Grab(Transform owner)
        {
            rigidbody.isKinematic = true;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            var transform1 = transform;
            transform1.rotation = owner.rotation;
            transform1.parent = owner;
            transform1.localPosition = Vector3.zero;
        }
        
        public void Throw()
        {
            rigidbody.isKinematic = false;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = true;
            gameObject.transform.parent = _level.transform;
        }
    }
}