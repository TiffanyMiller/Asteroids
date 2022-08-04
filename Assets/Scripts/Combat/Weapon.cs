using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] internal GameObject projectile;
        [SerializeField] internal float fireSpeed = 10;
        [SerializeField] internal float projectileLifetime = 2;
        [SerializeField] internal float bursts = 3;
        [SerializeField] internal float fireRate = 0.08f;
    }
}
