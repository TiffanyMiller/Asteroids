using System.Collections;
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
        [SerializeField] private WeaponType startWeapon;

        private float ShipSpeed() => _shipController.GetComponent<Rigidbody2D>().velocity.magnitude;

        private void Awake()
        {
            inst = this;
            _shipController = GetComponent<SpaceshipController>();
            _shipController.onShoot = startWeapon.GetComponent<IWeapon>().Attack;
            
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
    }
}
