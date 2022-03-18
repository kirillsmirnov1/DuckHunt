using UnityEngine;

namespace DuckHunt.Control.GameMode
{
    public abstract class AGameMode : ScriptableObject
    {
        [SerializeField] public string modeName;
        
        public abstract void Start();
        public virtual bool ReadyToDisplay => !string.IsNullOrWhiteSpace(modeName);
        public abstract bool ReadyToPlay { get; }
    }
}