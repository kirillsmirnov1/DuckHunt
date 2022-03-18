using DuckHunt.Control.GameMode;
using DuckHunt.Utils;
using MyBox;
using UnityEditor;
using UnityEngine;
using UnityUtils.Variables;

namespace DuckHunt.Model
{
    [CreateAssetMenu(menuName = "Modes/GameModesArray", fileName = "GameModesArray", order = 0)]
    public class GameModesArray : XArrayVariable<AGameMode>
    {
#if UNITY_EDITOR
        [ButtonMethod]
        private void GetAllModes()
        {
            Value = SOUtils.GetAllInstances<AGameMode>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}