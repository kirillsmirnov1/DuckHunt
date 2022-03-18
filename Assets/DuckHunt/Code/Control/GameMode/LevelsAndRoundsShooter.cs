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
            InitView();
            InitCam();
            SpawnTargets();
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

        // TODO Level Start / End 
        // TODO Round Start / End 
        
        public override void OnClick() // TODO extract into gun controller  
        {
            var collider = Physics2D.OverlapCircle(_camRef.ScreenToWorldPoint(Input.mousePosition), 1);
            if (collider != null && collider.gameObject.TryGetComponent<ATarget>(out var target))
            {
                Debug.Log("birb shot");
            }
        }

        public override bool ReadyToPlay 
            => targetPrefab != null;
    }
}