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

        private int _level;
        private int _round;
        private int _bullets;
        private int _targetsShot;
        
        private Camera _camRef;
        private ATarget[] _targets;

        public override void Start()
        {
            InitView();
            InitCam();
            SpawnTargets();
            _level = 0;
            NextLevel();
        }

        private void InitView()
        {
            _view = Instantiate(modeCanvas).GetComponent<ShooterView>();
            _view.Init(this);
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

        private void NextLevel()
        {
            // TODO increase bird speed 
            _level++;
            if (_level > numberOfLevels)
            {
                OnModePassed();
                return;
            }

            _round = -1;
            _view.OnLevelStart(_level);
            NextRound();
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
            _view.OnRoundStart(_round);
            ReleaseTargets();
        }

        private void ReleaseTargets()
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i].transform.position = new Vector2(Random.Range(-2, 2), -5);
                _targets[i].gameObject.SetActive(true);
            }
        }

        private void OnLevelPassed()
        {
            // TODO show popup
            NextLevel();
        }

        private void OnModePassed()
        {
            // TODO 
            Debug.Log("Mode passed");
        }

        // TODO Level Start / End 
        // TODO Round Start / End 
        
        public override void OnClick()   
        {
            var targetsShot = Shoot();
            _targetsShot += targetsShot;
            _bullets--;
            
            Debug.Log($"bullets left : {_bullets}");
            
            if (_targetsShot >= targetsPerRound)
            {
                EndRound(true);
            }
            else if (_bullets <= 0)
            {
                EndRound(false);
            }
        }

        private void EndRound(bool success)
        {
            _view.OnRoundResult(_round, success);
            NextRound();
        }

        private int Shoot()
        {
            // TODO extract into gun controller
            // TODO count targets
            var collider = Physics2D.OverlapCircle(_camRef.ScreenToWorldPoint(Input.mousePosition), 1);
            if (collider != null && collider.gameObject.TryGetComponent<ATarget>(out var target))
            {
                Debug.Log("birb shot");
                return 1;
            }
            return 0;
        }

        public override bool ReadyToPlay 
            => targetPrefab != null;
    }
}