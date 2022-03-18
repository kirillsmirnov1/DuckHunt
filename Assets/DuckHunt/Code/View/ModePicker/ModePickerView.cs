using System.Linq;
using DuckHunt.Control.GameMode;
using DuckHunt.Model;
using UnityEngine;
using UnityUtils.View;

namespace DuckHunt.View.ModePicker
{
    public class ModePickerView : ListView<AGameMode>
    {
        [SerializeField] private GameModesArray gameModes;

        private void OnEnable()
        {
            SetEntries(gameModes.Value.Where(mode => mode.ReadyToDisplay).ToList());
        }
    }
}
