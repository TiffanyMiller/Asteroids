using System.Collections;
using UnityEngine;

namespace Combat
{
    public class AttackHandler : MonoBehaviour
    {
        private SpaceshipController _shipController;
        [SerializeField] private Weapon starter, blaster;

        private void Awake()
        {
            _shipController = GetComponent<SpaceshipController>();
            _shipController.onShoot = StartAttack;
        }
        
        private void Shoot(Weapon weapon, Vector2 dir, Quaternion rot)
        {
            var p = Instantiate(weapon.projectile, transform.position, rot);
            p.GetComponent<Projectile>().Setup(dir, weapon.fireSpeed, weapon.projectileLifetime);
        }

        private IEnumerator Burst(Weapon weapon)
        {
            var tr = transform;
            var dir = tr.up;
            var rot = tr.rotation;
            
            for (var i = 0; i < weapon.bursts; i++)
            {
                Shoot(weapon, dir, rot);

                yield return new WaitForSeconds(weapon.fireRate);
            }
        }

        private void StartAttack()
        {
            StartCoroutine(Burst(starter));
        }

        private void BlastAttack()
        {
        
        }
    }
}
