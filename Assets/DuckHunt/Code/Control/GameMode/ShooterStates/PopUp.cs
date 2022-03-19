using System;
using UnityUtils.Extensions;

namespace DuckHunt.Control.GameMode.ShooterStates
{
    public class PopUp : AShooterState
    {
        public PopUp(LevelsAndRoundsShooter shooter, string roundText, Action switchBackCallback, bool switchBack) : base(shooter)
        {
            Shooter.View.ShowPopup(roundText);
            if (switchBack)
            {
                Shooter.View.DelayAction(1.5f, () =>
                {
                    Shooter.View.HidePopUp();
                    switchBackCallback?.Invoke();
                });
            }
        }
    }
}