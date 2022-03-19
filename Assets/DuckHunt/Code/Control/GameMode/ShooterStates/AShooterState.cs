namespace DuckHunt.Control.GameMode.ShooterStates
{
    public abstract class AShooterState
    {
        protected readonly LevelsAndRoundsShooter Shooter;

        protected AShooterState(LevelsAndRoundsShooter shooter) 
            => Shooter = shooter;

        public virtual void OnClick(){ }
    }
}