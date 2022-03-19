using UnityEngine;
using UnityEngine.UI;

namespace DuckHunt.View.GameMode.Shooter
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Text bulletsText;
        [SerializeField] private Image weaponIcon;
        [SerializeField] private Button weaponButton;

        private void OnValidate()
        {
            bulletsText ??= GetComponentInChildren<Text>();
            weaponIcon ??= GetComponentInChildren<Image>();
            weaponButton ??= GetComponentInChildren<Button>();
        }
    }
}
