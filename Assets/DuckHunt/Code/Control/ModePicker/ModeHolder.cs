using DuckHunt.Control.GameMode;
using DuckHunt.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckHunt.Control.ModePicker
{
    public class ModeHolder : MonoBehaviour
    {
        [SerializeField] private GameModeVariable modVar;

        private AGameMode _mode;
        
        private void Awake()
        {
            _mode = modVar;
            _mode.Start();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(UiClick()) return; 
                _mode.OnClick();
            }
        }

        private static bool UiClick() 
            => EventSystem.current.IsPointerOverGameObject();
    }
}
