using DuckHunt.Control.GameMode;
using DuckHunt.View.ModePicker;
using UnityEngine;

namespace DuckHunt.Control.ModePicker
{
    public class ModePickerControl : MonoBehaviour
    {
        private void Awake()
        {
            ModePickerButton.OnModePicked += OnModePicked;
        }

        private void OnDestroy()
        {
            ModePickerButton.OnModePicked -= OnModePicked;
        }

        private void OnModePicked(AGameMode mode)
        {
            Debug.Log($"Picked mode: {mode.modeName}");
            // TODO 
        }
    }
}