using DuckHunt.Model;
using UnityEngine;

namespace DuckHunt.Control.ModePicker
{
    public class ModeHolder : MonoBehaviour
    {
        [SerializeField] private GameModeVariable modVar;

        private void Awake() 
            => modVar.Value.Start();
    }
}
