using DuckHunt.Control.GameMode;
using UnityEngine;
using UnityUtils.Variables;

namespace DuckHunt.Model
{
    [CreateAssetMenu(menuName = "Modes/GameModeVariable", fileName = "GameModeVariable", order = 1)]
    public class GameModeVariable : XVariable<AGameMode>
    {
        
    }
}