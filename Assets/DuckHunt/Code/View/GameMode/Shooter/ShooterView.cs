using DuckHunt.Control.GameMode;
using UnityEngine;

namespace DuckHunt.View.GameMode.Shooter
{
    public class ShooterView : MonoBehaviour
    {
        [SerializeField] private TargetCounter targetCounter;
        
        private LevelsAndRoundsShooter _mode;

        private void OnValidate()
        {
            targetCounter ??= GetComponentInChildren<TargetCounter>();
        }

        public void Init(LevelsAndRoundsShooter levelsAndRoundsShooter)
        {
            _mode = levelsAndRoundsShooter;
            targetCounter.Init(_mode.roundsPerLevel); // TODO pass icon 
        }

        public void OnLevelStart()
        {
            targetCounter.SetTagsMode(TargetCounter.TagMode.ToDo);
        }

        public void OnRoundStart(int round)
        {
            targetCounter.SetTagMode(round, TargetCounter.TagMode.Current);
        }

        public void OnRoundResult(int round, bool result)
        {
            targetCounter.SetTagMode(round, result ? TargetCounter.TagMode.Done : TargetCounter.TagMode.Failed);
        }
    }
}