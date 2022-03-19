namespace DuckHunt.Control.GameMode.ShooterStates
{
    public class Shooting : AShooterState
    {
        public Shooting(LevelsAndRoundsShooter shooter) : base(shooter) 
            => Shooter.ReleaseTargets();

        public override void OnClick() 
            => Shooter.Shoot();
    }
}