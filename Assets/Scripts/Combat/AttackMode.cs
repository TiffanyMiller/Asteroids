using System;
using System.Collections;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Ship.SpaceshipController))]
    public class AttackMode : MonoBehaviour
    {
        private Transform ship;
        private float shipSpeed;

        protected Vector2 Dir() => ship.up;
        protected Vector3 Rot() => ship.localEulerAngles;

        protected void Shoot(Weapon weapon, Vector2 dir, Vector3 rot)
        {
            var p = ObjectPooler.inst.SpawnFromPool(weapon.projectile.name, ship.localPosition + (ship.up / 2), rot);
            //var shipSpeed = _shipController.GetComponent<Rigidbody2D>().velocity.magnitude;
            
            var rb = p.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * (weapon.fireSpeed + shipSpeed), ForceMode2D.Impulse);

            StartCoroutine(Deactivate(p));
        }

        public virtual void Attack()
        {
        }

        private IEnumerator Deactivate(GameObject projectile)
        {
            yield return new WaitForSeconds(2);
            
            projectile.SetActive(false);
        }
    }
}
