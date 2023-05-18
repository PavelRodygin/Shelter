using UnityEditor.PackageManager.Requests;
using UnityEngine;
using Zenject;

namespace Core.AbstractClasses
{
    public abstract class Item : MonoBehaviour
    {
        [Inject] private GameScripts.Level.Level _level;
        [SerializeField] private Rigidbody _rigidbody;
        public Transform Transform => transform;

        public virtual void Grab(Transform owner)                         
        {
            _rigidbody.isKinematic = true;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            var transform1 = transform;
            transform1.rotation = owner.rotation;
            transform1.parent = owner;
            transform1.localPosition = Vector3.zero;
        }
        
        public virtual void Throw() 
        {
            _rigidbody.isKinematic = false;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = true;
            gameObject.transform.parent = _level.transform;
        }
    }
}