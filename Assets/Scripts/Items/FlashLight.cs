using UnityEngine;

namespace Items
{
    public class FlashLight : MonoBehaviour
    {
        [SerializeField] private Level level;
        private Rigidbody _rigidbody;
        public Transform Transform => transform;
        
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void Grab(Transform owner)
        {
            _rigidbody.isKinematic = true;
            gameObject.transform.position = owner.position;
            gameObject.transform.parent = owner;
        }
        
        public void Throw()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(0,0,20, ForceMode.Impulse);
            gameObject.transform.parent = level.transform;
        }
    }
}