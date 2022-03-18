using UnityEngine;

namespace DuckHunt.Control.Targets
{
    public abstract class ATarget : MonoBehaviour
    {
        public float Speed { get; set; } = 5;

        protected Vector2 Direction;

        protected virtual void Update() 
            => Move();

        protected abstract void Move();
    }
}
