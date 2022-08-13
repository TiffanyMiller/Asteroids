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
        
        private void Shoot(Weapon weapon, Vector2 dir, Quaternion rot)
        {
            var p = Instantiate(weapon.projectile, transform.localPosition + (transform.up / 2), rot);
            var shipSpeed = _shipController.GetComponent<Rigidbody2D>().velocity.magnitude;
            
            p.GetComponent<MoveVelocity>().Setup(dir, weapon.fireSpeed + shipSpeed);
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
