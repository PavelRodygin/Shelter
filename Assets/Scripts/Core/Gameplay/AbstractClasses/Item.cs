using Core.GameInterfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Gameplay.AbstractClasses
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour, IItem
    {
        private const int LevelsToMoveUp = 3;

        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Collider collisionCollider;
        [SerializeField] private SphereCollider triggerCollider;
        
        public Transform Transform => transform;

        public virtual void GetGrabbed(Transform owner)                         
        {
            rigidBody.isKinematic = true;
            triggerCollider.enabled = false;
            collisionCollider.enabled = false;
            var transform1 = transform;
            transform1.rotation = owner.rotation;
            transform1.parent = owner;
            transform1.localPosition = Vector3.zero;
        }

        public virtual async void GetThrown(Vector3 force) 
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            collisionCollider.enabled = true;
            var parent = transform.parent;
            for (var i = 0; i < LevelsToMoveUp && parent != null; i++)
                parent = parent.parent;
            transform.SetParent(parent);
            rigidBody.AddForce(force, ForceMode.Impulse);
            await UniTask.Delay(500);
            triggerCollider.enabled = true;
        }
    }
}