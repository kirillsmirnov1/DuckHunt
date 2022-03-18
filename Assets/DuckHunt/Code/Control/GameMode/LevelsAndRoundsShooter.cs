using DuckHunt.Control.Targets;
using DuckHunt.View.GameMode.Shooter;
using UnityEngine;

namespace DuckHunt.Control.GameMode
{
    [CreateAssetMenu(menuName = "Modes/LevelsAndRoundsShooter", fileName = "LevelsAndRoundsShooter", order = 10)]
    public class LevelsAndRoundsShooter : AGameMode
    {
        [Header("Shooter")]
        [SerializeField] private int numberOfLevels = 10;
        [SerializeField] public int roundsPerLevel = 5;
        [SerializeField] private int bulletsPerRound = 3;
        [SerializeField] private int targetsPerRound = 1;
        
        [SerializeField] private ATarget targetPrefab;
        [SerializeField] private Rect camRect = new Rect(0, .2f, 1, .8f);
        
        private ShooterView _view;
        
        public override void Start()
        {
            _view = Instantiate(modeCanvas).GetComponent<ShooterView>();
            _view.Init(this);
            Camera.main.rect = camRect;
        }
        
        // TODO Level Start / End 
        // TODO Round Start / End 

        public override bool ReadyToPlay 
            => targetPrefab != null;
    }
}