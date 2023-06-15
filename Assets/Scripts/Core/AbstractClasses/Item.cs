using GameScripts;
using UnityEngine;
using Zenject;

namespace Core.AbstractClasses
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        [Inject] private GameplayModule _gameplayModule;
        [SerializeField] private Rigidbody rigidBody;
        public Transform Transform => transform;

        public virtual void Grab(Transform owner)                         
        {
            rigidBody.isKinematic = true;
            gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
            gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            var transform1 = transform;
            transform1.rotation = owner.rotation;
            transform1.parent = owner;
            transform1.localPosition = Vector3.zero;
        }
        
        public virtual void Throw() 
        {
            if (_gameplayModule == null)
                Debug.Log("ZENJECT НЕ РАБОТАЕТ");
            rigidBody.isKinematic = false;
            GetComponentInChildren<SphereCollider>().enabled = true;
            GetComponentInChildren<MeshCollider>().enabled = true;
            transform.parent = _gameplayModule.transform;
        }
    }
}