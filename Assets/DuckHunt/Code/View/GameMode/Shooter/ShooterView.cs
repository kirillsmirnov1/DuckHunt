using DuckHunt.Control.GameMode;
using UnityEngine;
using UnityEngine.UI;

namespace DuckHunt.View.GameMode.Shooter
{
    public class ShooterView : MonoBehaviour
    {
        [SerializeField] private TargetCounter targetCounter;
        [SerializeField] private WeaponView weaponView;
        [SerializeField] private GameObject popUpPanel;
        
        [SerializeField] private Text modeText;
        [SerializeField] private Text pointsText;
        [SerializeField] private Text popUpText;
        
        private LevelsAndRoundsShooter _mode;

        private void OnValidate()
        {
            targetCounter ??= GetComponentInChildren<TargetCounter>();
            weaponView ??= GetComponentInChildren<WeaponView>();
        }

        public void Init(LevelsAndRoundsShooter levelsAndRoundsShooter)
        {
            _mode = levelsAndRoundsShooter;
            targetCounter.Init(_mode.roundsPerLevel); // TODO pass icon 
            HidePopUp();
            SetModeText(0);
            SetPointsText(0);
        }

        public void OnLevelStart(int level)
        {
            SetModeText(level);
            targetCounter.SetTagsMode(TargetCounter.TagMode.ToDo);
        }

        public void OnRoundStart(int round)
        {
            targetCounter.SetTagMode(round, TargetCounter.TagMode.Current);
        }

        public void OnRoundResult(int round, bool result, int points)
        {
            targetCounter.SetTagMode(round, result ? TargetCounter.TagMode.Done : TargetCounter.TagMode.Failed);
            SetPointsText(points);
        }

        private void SetModeText(int round)
        {
            modeText.text = $"{_mode.modeName} : {round}";
        }

        private void SetPointsText(int points)
        {
            pointsText.text = $"Score : {points}";
        }

        public void UpdateBulletCount(int bullets) 
            => weaponView.UpdateBulletCount(bullets);

        public void ShowPopup(string popupText)
        {
            popUpText.text = popupText;
            popUpPanel.SetActive(true);
        }

        public void HidePopUp() 
            => popUpPanel.SetActive(false);
    }
}