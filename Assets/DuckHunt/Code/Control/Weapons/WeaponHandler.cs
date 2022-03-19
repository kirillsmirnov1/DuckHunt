using System.Collections.Generic;
using DuckHunt.Control.Targets;
using DuckHunt.Model;
using UnityEngine;

namespace DuckHunt.Control.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        private Camera _camRef;
        private Weapon[] _weapons;
        private int _iCurrentWeapon;
        private Weapon _currentWeapon;

        private void Awake() 
            => _camRef = Camera.main;

        public void Init(Weapon[] weapons)
        {
            _weapons = weapons;
            _iCurrentWeapon = 0;
            
            SetWeapon(weapons);
        }

        private void SetWeapon(Weapon[] weapons)
        {
            _currentWeapon = weapons[_iCurrentWeapon];
            // TODO notify view 
        }

        public List<ATarget> Shoot()
        {
            var charges = PrepareCharges();
            // TODO show charges
            return ShootTargets(charges);
        }

        private Vector3[] PrepareCharges()
        {
            var charges = new Vector3[_currentWeapon.chargesPerShot];
            Vector2 shotCenter = _camRef.ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < charges.Length; i++)
            {
                charges[i] = shotCenter + Random.insideUnitCircle * _currentWeapon.rangeRadius;
            }

            return charges;
        }

        private List<ATarget> ShootTargets(Vector3[] charges)
        {
            var shotTargets = new List<ATarget>();
            for (var iCharge = 0; iCharge < charges.Length; iCharge++)
            {
                var colliders = Physics2D.OverlapCircleAll(charges[iCharge], _currentWeapon.chargeRadius);
                for (var iCollider = 0; iCollider < colliders.Length; iCollider++)
                {
                    if (ShotATarget(colliders[iCollider], out var target))
                    {
                        Debug.Log("birb shot");
                        shotTargets.Add(target);
                    }
                }
            }
            return shotTargets;
        }

        private static bool ShotATarget(Collider2D collider, out ATarget target) 
            => collider.gameObject.TryGetComponent(out target);
    }
}