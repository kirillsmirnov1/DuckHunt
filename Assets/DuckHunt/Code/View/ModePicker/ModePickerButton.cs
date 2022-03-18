using System;
using DuckHunt.Control.GameMode;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using UnityUtils.View;

namespace DuckHunt.View.ModePicker
{
    [RequireComponent(typeof(Button))]
    public class ModePickerButton : ListViewEntry<AGameMode>
    {
        public static event Action<AGameMode> OnModePicked; 

        [SerializeField] private Text modeNameText;
        [SerializeField] private Button button;
        
        private AGameMode _mode;

        private void OnValidate()
        {
            modeNameText ??= GetComponentInChildren<Text>();
            button ??= GetComponent<Button>();
            this.CheckNullFields();
        }

        public override void Fill(AGameMode data)
        {
            _mode = data;
            
            modeNameText.text = data.modeName;
            button.interactable = data.ReadyToPlay;
            button.onClick.AddListener(NotifyOnPickedMode);
            base.Fill(data);
        }

        private void NotifyOnPickedMode() 
            => OnModePicked?.Invoke(_mode);
    }
}
