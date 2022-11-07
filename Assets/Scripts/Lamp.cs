using UnityEngine;

namespace DefaultNamespace
{
    public class Lamp : MonoBehaviour
    {
        [SerializeField] private GameObject light;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            light.gameObject.SetActive(false);
        }
        
        public void TurnOn()
        {
            light.gameObject.SetActive(true);
        }
        public void TurnOff()
        {
            light.gameObject.SetActive(false);
        }
    }
}