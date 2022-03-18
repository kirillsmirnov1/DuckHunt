using System;
using UnityEngine;

namespace DuckHunt.Control.Targets
{
    public abstract class ATarget : MonoBehaviour
    {
        public static event Action OnTargetHit;
        
        public float Speed { get; set; } = 10;

        protected Vector2 Direction;

        protected virtual void Update() 
            => Move();

        protected abstract void Move();

        private void OnMouseDown()
        {
            // TODO check with colliders/raycasts 
            HandleHit();
        }

        private void HandleHit() 
            => OnTargetHit?.Invoke();
    }
}
