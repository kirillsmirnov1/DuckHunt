using System.Collections.Generic;
using DuckHunt.Control.Targets;
using UnityEngine;

namespace DuckHunt.Control.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        private Camera _camRef;

        private void Awake() 
            => _camRef = Camera.main;

        public List<ATarget> Shoot()
        {
            // TODO extract into gun controller
            // TODO count targets
            var shotTargets = new List<ATarget>();
            
            var colliders = Physics2D.OverlapCircleAll(_camRef.ScreenToWorldPoint(Input.mousePosition), 1);
            for (int i = 0; i < colliders.Length; i++)
            {
                var c = colliders[i];
                if (c.gameObject.TryGetComponent<ATarget>(out var target))
                {
                    Debug.Log("birb shot");
                    shotTargets.Add(target);
                }
            }
            return shotTargets;
        }
    }
}