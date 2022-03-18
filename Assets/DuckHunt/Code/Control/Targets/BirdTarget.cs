using UnityEngine;

namespace DuckHunt.Control.Targets
{
    public class BirdTarget : ATarget
    {
        [SerializeField] private float dirRadius = 3;
        private Transform _camTransform;

        private void Awake() 
            => _camTransform = Camera.main.transform;

        private void OnEnable() 
            => RandomizeDirection();

        private void OnBecameInvisible() 
            => RandomizeDirection();
        
        protected override void Move() 
            => transform.Translate(Direction * Time.deltaTime * Speed);
        
        private void RandomizeDirection() 
            => Direction = ((Vector2) (_camTransform.position - transform.position) + Random.insideUnitCircle * dirRadius).normalized;
    }
}