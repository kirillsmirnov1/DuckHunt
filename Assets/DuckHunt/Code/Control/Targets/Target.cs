using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DuckHunt.Control.Targets
{
    public class Target : MonoBehaviour
    {
        public static event Action OnTargetHit;
        
        [SerializeField] private float dirRadius = 3;
        
        public float Speed { get; set; } = 10;

        private Vector2 _direction;
        private Transform _camTransform;

        private void Awake() 
            => _camTransform = Camera.main.transform;

        private void OnEnable() 
            => RandomizeDirection();

        private void Update() 
            => Move();

        private void OnBecameInvisible() 
            => RandomizeDirection();

        private void RandomizeDirection() 
            => _direction = ((Vector2) (_camTransform.position - transform.position) + Random.insideUnitCircle * dirRadius).normalized;

        private void Move() 
            => transform.Translate(_direction * Time.deltaTime * Speed);

        private void OnMouseDown()
        {
            // TODO check with colliders/raycasts 
            HandleHit();
        }

        private void HandleHit() 
            => OnTargetHit?.Invoke();
    }
}
