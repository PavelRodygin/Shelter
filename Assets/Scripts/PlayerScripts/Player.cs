using System;
using DefaultNamespace;
using Interfaces;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        private bool _isAlive;
        public bool IsAlive { get; private set; }
        public event Action SurvivalStarted;
        private bool _survivalStarted;

        
        
        private void Die()
        {
            _isAlive = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!_survivalStarted && other.GetComponent<ShelterZone>())
            {
                _survivalStarted = true;
                SurvivalStarted?.Invoke();
            }
        }
    }
}
