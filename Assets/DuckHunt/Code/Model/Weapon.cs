using UnityEngine;

namespace DuckHunt.Model
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Data/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [Header("Data")]
        public int chargesPerShot = 1;
        public float chargeRadius = .5f;
        public float rangeRadius = 0f;

        [Header("Sprites")]
        public Sprite weaponIcon;
        public Sprite chargeSprite;
    }
}