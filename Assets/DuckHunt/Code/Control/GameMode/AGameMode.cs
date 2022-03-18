using UnityEngine;

namespace DuckHunt.Control.GameMode
{
    public abstract class AGameMode : ScriptableObject
    {
        [SerializeField] protected string flowName;
        
        public abstract void Start();
        public virtual bool ReadyToDisplay => !string.IsNullOrWhiteSpace(flowName);
        public abstract bool ReadyToPlay { get; }
    }
}