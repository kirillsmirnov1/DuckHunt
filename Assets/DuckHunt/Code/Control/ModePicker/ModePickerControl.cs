using DuckHunt.Control.GameMode;
using DuckHunt.Model;
using DuckHunt.View.ModePicker;
using MyBox;
using UnityEngine;

namespace DuckHunt.Control.ModePicker
{
    public class ModePickerControl : MonoBehaviour
    {
        [SerializeField] private GameModeVariable gameModeVariable;
        [SerializeField] private SceneReference sceneRef;
           
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
            gameModeVariable.Value = mode;
            sceneRef.LoadScene();
        }
    }
}