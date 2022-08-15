using System.Collections;
using Ship;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(SpaceshipController))]
    public class ShipAttackHandler : MonoBehaviour
    {
        private SpaceshipController _shipController;
        [SerializeField] private Weapon starter, blaster;

        private void Awake()
        {
            _shipController = GetComponent<SpaceshipController>();
            _shipController.onShoot = StartAttack;
        }
        
        private void Shoot(Weapon weapon, Vector2 dir, Vector3 rot)
        {
            var p = ObjectPooler.inst.SpawnFromPool(weapon.projectile.name, transform.localPosition + (transform.up / 2), rot);
            var shipSpeed = _shipController.GetComponent<Rigidbody2D>().velocity.magnitude;
            
            var rb = p.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * (weapon.fireSpeed + shipSpeed), ForceMode2D.Impulse);

            StartCoroutine(Deactivate(p));
        }

        private IEnumerator Deactivate(GameObject projectile)
        {
            yield return new WaitForSeconds(2);
            
            projectile.SetActive(false);
        }

        private IEnumerator Burst(Weapon weapon)
        {
            var tr = transform;
            var dir = tr.up;
            var rot = tr.localEulerAngles;
            
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
