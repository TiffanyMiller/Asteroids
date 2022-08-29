using System;
using System.Collections;
using Powerups;
using Ship;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(SpaceshipController))]
    public class ShipAttackHandler : MonoBehaviour
    {
        public static ShipAttackHandler inst;
        private SpaceshipController _shipController;

        [SerializeField] private ObjectPool projectilePool;
        [SerializeField] private Weapon startWeapon;

        private float ShipSpeed() => _shipController.GetComponent<Rigidbody2D>().velocity.magnitude;

        private void Awake()
        {
            inst = this;
            _shipController = GetComponent<SpaceshipController>();
            _shipController.onShoot = startWeapon.attack;
            
            projectilePool.SetupPool();
        }

        public void Shoot(Weapon weapon)
        {
            var tr = transform;
            var p = projectilePool.SpawnFromPool(weapon.projectile.name, tr.localPosition + (tr.up / 2), tr.localEulerAngles);

            var rb = p.GetComponent<Rigidbody2D>();
            rb.AddForce(tr.up * (weapon.fireSpeed + ShipSpeed()), ForceMode2D.Impulse);

            StartCoroutine(Deactivate(p));
        }

        private IEnumerator Deactivate(GameObject projectile)
        {
            yield return new WaitForSeconds(2);

            projectile.SetActive(false);
        }

        #region Attack Types

        private IEnumerator Burst(Weapon weapon)
        {
            for (var i = 0; i < weapon.bursts; i++)
            {
                Shoot(weapon);

                yield return new WaitForSeconds(weapon.fireRate);
            }
        }

        private void BurstAttack()
        {
            StartCoroutine(Burst(startWeapon));
        }

        #endregion
    }
}
