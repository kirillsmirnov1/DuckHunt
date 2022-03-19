using System;
using DuckHunt.Model;
using UnityEngine;
using UnityEngine.UI;

namespace DuckHunt.View.GameMode.Shooter
{
    public class WeaponView : MonoBehaviour
    {
        public static event Action OnChangeWeaponRequest;
        
        [SerializeField] private Text bulletsText;
        [SerializeField] private Image weaponIcon;
        [SerializeField] private Button weaponButton;
        
        [Header("Data")]
        [SerializeField] private WeaponVariable weaponVariable;
        
        private void OnValidate()
        {
            bulletsText ??= GetComponentInChildren<Text>();
            weaponIcon ??= GetComponentInChildren<Image>();
            weaponButton ??= GetComponentInChildren<Button>();
        }

        private void Awake()
        {
            weaponVariable.OnChange += SetWeaponIcon;
            weaponButton.onClick.AddListener(() => OnChangeWeaponRequest?.Invoke());
        }

        private void Start()
        {
            SetWeaponIcon(weaponVariable);
        }

        private void OnDestroy()
        {
            weaponVariable.OnChange -= SetWeaponIcon;
        }

        private void SetWeaponIcon(Weapon weapon) 
            => weaponIcon.sprite = weapon.weaponIcon;

        public void UpdateBulletCount(int bullets) 
            => bulletsText.text = bullets.ToString();
    }
}
