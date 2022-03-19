using System.Collections.Generic;
using System.Linq;
using DuckHunt.Control.Targets;
using DuckHunt.Model;
using UnityEngine;
using UnityUtils.Extensions;

namespace DuckHunt.Control.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        private Camera _camRef;
        private Weapon[] _weapons;
        private int _iCurrentWeapon;
        private WeaponVariable _weaponVariable;
        private Weapon CurrentWeapon
        {
            get => _weaponVariable.Value;
            set => _weaponVariable.Value = value;
        }

        private ChargeView[] _chargeViews;

        private void Awake() 
            => _camRef = Camera.main;

        public void Init(Weapon[] weapons, WeaponVariable weaponVariable)
        {
            _weaponVariable = weaponVariable;
            _weapons = weapons;
            _iCurrentWeapon = 0;
            SetWeapon(weapons);
            InitChargeViews();
        }

        private void InitChargeViews()
        {
            var maxCharges = _weapons.Select(w => w.chargesPerShot).Max();
            _chargeViews = new ChargeView[maxCharges];
            for (int i = 0; i < maxCharges; i++)
            {
                _chargeViews[i] = ChargeView.New(transform, i, CurrentWeapon.chargeSprite);
            }
            DisableChargeViews();
        }

        private void DisableChargeViews()
        {
            for (int i = 0; i < _chargeViews.Length; i++)
            {
                _chargeViews[i].SetActive(false);
            }
        }

        private void SetWeapon(Weapon[] weapons)
        {
            CurrentWeapon = weapons[_iCurrentWeapon];
            // TODO notify view 
        }

        public List<ATarget> Shoot()
        {
            var charges = PrepareCharges();
            ShowCharges(charges);
            return ShootTargets(charges);
        }

        private Vector3[] PrepareCharges()
        {
            var charges = new Vector3[CurrentWeapon.chargesPerShot];
            Vector2 shotCenter = _camRef.ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < charges.Length; i++)
            {
                charges[i] = shotCenter + Random.insideUnitCircle * CurrentWeapon.rangeRadius;
            }

            return charges;
        }

        private void ShowCharges(Vector3[] charges)
        {
            StopAllCoroutines();
            DisableChargeViews();

            var scale = Vector3.one * CurrentWeapon.chargeRadius * 2;
            
            for (int i = 0; i < charges.Length; i++)
            {
                _chargeViews[i].Transform.localScale = scale;
                _chargeViews[i].Transform.position = charges[i];
                _chargeViews[i].SetActive(true);
            }
            
            this.DelayAction(CurrentWeapon.shotDuration, DisableChargeViews);
        }

        private List<ATarget> ShootTargets(Vector3[] charges)
        {
            var shotTargets = new HashSet<ATarget>();
            for (var iCharge = 0; iCharge < charges.Length; iCharge++)
            {
                var colliders = Physics2D.OverlapCircleAll(charges[iCharge], CurrentWeapon.chargeRadius);
                for (var iCollider = 0; iCollider < colliders.Length; iCollider++)
                {
                    if (ShotATarget(colliders[iCollider], out var target))
                    {
                        shotTargets.Add(target);
                    }
                }
            }

            Debug.Log($"Shot {shotTargets.Count} targets");
            return shotTargets.ToList();
        }

        private static bool ShotATarget(Collider2D collider, out ATarget target) 
            => collider.gameObject.TryGetComponent(out target);

        private class ChargeView
        {
            private readonly GameObject _object;
            public Transform Transform { get; private set; }
            public Sprite Sprite {
                set => _sprite.sprite = value;
            }
            private readonly SpriteRenderer _sprite;
            
            private ChargeView(GameObject obj, Transform transform, SpriteRenderer spriteRenderer, Sprite sprite)
            {
                _object = obj;
                Transform = transform;
                _sprite = spriteRenderer;
                Sprite = sprite;
            }

            public void SetActive(bool active) => _object.SetActive(active);

            public static ChargeView New(Transform parent, int index, Sprite sprite)
            {
                var charge = new GameObject($"Charge {index}");
                var spriteRenderer = charge.AddComponent<SpriteRenderer>();
                var transform = charge.transform;
                transform.parent = parent;
                
                return new ChargeView(charge, transform, spriteRenderer, sprite);
            }
        }
    }
}