using UnityEngine;

namespace DuckHunt.Control.GameMode
{
    [CreateAssetMenu(menuName = "Modes/LevelsAndRoundsShooter", fileName = "LevelsAndRoundsShooter", order = 1)]
    public class LevelsAndRoundsShooter : AGameMode
    {
        [SerializeField] private int numberOfLevels = 10;
        [SerializeField] private int roundsPerLevel = 5;
        [SerializeField] private int bulletsPerRound = 3;
        [SerializeField] private int targetsPerRound = 1;
        
        [SerializeField] private GameObject targetPrefab;

        public override void Start()
        {
            // TODO 
        }

        public override bool ReadyToPlay 
            => targetPrefab != null;
    }
}