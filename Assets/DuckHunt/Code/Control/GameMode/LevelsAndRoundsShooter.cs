﻿using DuckHunt.View.GameMode.Shooter;
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
        
        [SerializeField] private GameObject targetPrefab;
        
        private ShooterView _view;
        
        public override void Start()
        {
            _view = Instantiate(modeCanvas).GetComponent<ShooterView>();
            _view.Init(this);
        }

        public override bool ReadyToPlay 
            => targetPrefab != null;
    }
}