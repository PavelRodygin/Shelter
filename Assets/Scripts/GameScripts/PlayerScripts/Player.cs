using System;
using UnityEngine;
using Zenject;

namespace GameScripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerMoveController))]
    [RequireComponent(typeof(PlayerInteractController))]
    public class Player : MonoBehaviour
    {
        [Inject] private Camera _camera;
        public PlayerMoveController moveController;
        public PlayerInteractController interactController;
        private Vector3 _stockCameraPosition;
        private bool _isAlive;
        public bool IsAlive { get; private set; }
        public PlayerMoveController MoveController => moveController;
        public PlayerInteractController InteractController => interactController;

        private void OnEnable()
        {
            _camera.orthographic = false;
            _camera.transform.parent = gameObject.transform;
            _stockCameraPosition = _camera.gameObject.transform.position;
            _camera.transform.position = gameObject.transform.position + new Vector3(0, 1.8f, 0);
        }
        
        private void Die()
        {
            
        }

        private void OnDisable()
        {
            _camera.orthographic = false;
            _camera.transform.parent = null;
            _camera.gameObject.transform.position = _stockCameraPosition;
        }
    }
}