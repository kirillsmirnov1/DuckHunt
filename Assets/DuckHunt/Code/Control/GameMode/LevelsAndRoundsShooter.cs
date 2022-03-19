using System;
using DuckHunt.Control.GameMode.ShooterStates;
using DuckHunt.Control.Targets;
using DuckHunt.Control.Weapons;
using DuckHunt.Model;
using DuckHunt.View.GameMode.Shooter;
using UnityEngine;
using UnityUtils.Extensions;
using Random = UnityEngine.Random;

namespace DuckHunt.Control.GameMode
{
    [CreateAssetMenu(menuName = "Modes/LevelsAndRoundsShooter", fileName = "LevelsAndRoundsShooter", order = 10)]
    public class LevelsAndRoundsShooter : AGameMode
    {
        [Header("Shooter")]
        [SerializeField] private int numberOfLevels = 10;
        [SerializeField] public int roundsPerLevel = 5;
        [SerializeField] private int allowedFailedRounds = 1;
        [SerializeField] private int bulletsPerRound = 3;
        [SerializeField] private int targetsPerRound = 1;
        [SerializeField] private float targetBaseSpeed = 5f;
        [SerializeField] private float targetSpeedMod = 1.2f;
        [SerializeField] private ATarget targetPrefab;
        [SerializeField] private Rect camRect = new Rect(0, .2f, 1, .8f);
        [SerializeField] private Weapon[] allowedWeapons;
        [SerializeField] private WeaponVariable currentWeaponVariable;

        public ShooterView View { get; private set; }

        private int _level;
        private int _round;
        private int _bullets;
        private int _targetsShot;
        private int _points;
        private int _failedRounds;
        
        private Camera _camRef;
        private WeaponHandler _weaponHandler;
        private ATarget[] _targets;
        private AShooterState _state;
        private bool _lastLevelWon;

        public override void Start()
        {
            InitView();
            InitCam();
            InitWeaponHandler();
            SpawnTargets();
            NextGame();
        }

        private void InitView()
        {
            View = Instantiate(modeCanvas).GetComponent<ShooterView>();
            View.Init(this);
        }

        private void InitCam()
        {
            _camRef = Camera.main;
            _camRef.rect = camRect;
        }

        private void SpawnTargets()
        {
            _targets = new ATarget[targetsPerRound];
            for (int i = 0; i < targetsPerRound; i++)
            {
                _targets[i] = Instantiate(targetPrefab);
                _targets[i].gameObject.SetActive(false);
            }
        }

        private void InitWeaponHandler()
        {
            _weaponHandler = new GameObject("WeaponHandler")
                .AddComponent<WeaponHandler>();
            _weaponHandler.Init(allowedWeapons, currentWeaponVariable);
        }

        private void NextGame()
        {
            _level = 0;
            _points = 0;
            UpdateTargetSpeed(targetBaseSpeed / targetSpeedMod);
            NextLevel();
            StartShootingState();
        }

        private void NextLevel()
        {
            _level++;
            if (_level > numberOfLevels)
            {
                OnGamePassed();
                return;
            }

            _failedRounds = 0;
            UpdateTargetSpeed(_targets[0].Speed * targetSpeedMod);
            _round = -1;
            View.OnLevelStart(_level);
            NextRound();
        }

        private void UpdateTargetSpeed(float speed)
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i].Speed = speed;
            }
        }

        private void NextRound()
        {
            _round++;
            if (_round >= roundsPerLevel)
            {
                OnLevelPassed();
                return;
            }

            _targetsShot = 0;
            _bullets = bulletsPerRound;
            View.UpdateBulletCount(_bullets);
            View.OnRoundStart(_round);
            StartShootingState();
        }

        public void ReleaseTargets()
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i].transform.position = new Vector2(Random.Range(-2, 2), -5);
                _targets[i].gameObject.SetActive(true);
            }
        }

        public override void OnClick() 
            => _state?.OnClick();

        public void Shoot()
        {
            var targetsShot = _weaponHandler.Shoot();
            for (int i = 0; i < targetsShot.Count; i++)
            {
                targetsShot[i].gameObject.SetActive(false);
            }

            _targetsShot += targetsShot.Count;
            _bullets--;
            View.UpdateBulletCount(_bullets);

            if (_targetsShot >= targetsPerRound)
            {
                OnRoundPassed(true);
            }
            else if (_bullets <= 0)
            {
                OnRoundPassed(false);
            }
        }

        private void OnRoundPassed(bool success)
        {
            if (!success) _failedRounds++;
            var roundPoints = _targetsShot * 100 + _bullets * 50; // TODO extract PointsSO 
            _points += roundPoints;  
            View.OnRoundResult(_round, success, _points);
            var roundText = $"Round {(success ? "won" : "lost")}\n\n score +{roundPoints}";
            StartPopupState(roundText, NextRound);
        }

        private void OnLevelPassed()
        {
            _lastLevelWon = _failedRounds <= allowedFailedRounds;
            StartPopupState($"Level {(_lastLevelWon ? "won" : "lost")}", _lastLevelWon ? (Action) NextLevel : OnGamePassed);
        }

        private void OnGamePassed()
        {
            var pointsStr = $"score : {_points}";
            StartPopupState(_lastLevelWon 
                ? $"You won the game!\n\n{pointsStr}" 
                : $"Game over\n\nYou lost\n\n{pointsStr}", null, false);
        }

        public override bool ReadyToPlay 
            => targetPrefab != null;

        private void StartPopupState(string roundText, Action endCallback, bool switchBack = true)
        {
            _state = null;
            View.DelayAction(.3f, () =>
                _state = new PopUp(this, roundText, endCallback, switchBack));
        }

        public void StartShootingState() 
            => _state = new Shooting(this);
    }
}