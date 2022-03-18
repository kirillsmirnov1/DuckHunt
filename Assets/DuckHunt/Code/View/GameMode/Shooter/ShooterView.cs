using DuckHunt.Control.GameMode;
using UnityEngine;
using UnityEngine.UI;

namespace DuckHunt.View.GameMode.Shooter
{
    public class ShooterView : MonoBehaviour
    {
        [SerializeField] private TargetCounter targetCounter;
        [SerializeField] private Text modeText;
        [SerializeField] private Text pointsText;
        
        private LevelsAndRoundsShooter _mode;

        private void OnValidate()
        {
            targetCounter ??= GetComponentInChildren<TargetCounter>();
        }

        public void Init(LevelsAndRoundsShooter levelsAndRoundsShooter)
        {
            _mode = levelsAndRoundsShooter;
            targetCounter.Init(_mode.roundsPerLevel); // TODO pass icon 
            SetModeText(0);
            SetPointsText(0);
        }

        public void OnLevelStart()
        {
            targetCounter.SetTagsMode(TargetCounter.TagMode.ToDo);
        }

        public void OnRoundStart(int round)
        {
            SetModeText(round);
            targetCounter.SetTagMode(round, TargetCounter.TagMode.Current);
        }

        public void OnRoundResult(int round, bool result)
        {
            targetCounter.SetTagMode(round, result ? TargetCounter.TagMode.Done : TargetCounter.TagMode.Failed);
        }

        private void SetModeText(int round)
        {
            modeText.text = $"{_mode.modeName} : {round}";
        }

        private void SetPointsText(int points)
        {
            pointsText.text = $"Score : {points}";
        }
    }
}