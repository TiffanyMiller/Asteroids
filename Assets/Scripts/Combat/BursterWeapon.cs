using System.Collections;
using UnityEngine;

namespace Combat
{
    public class BursterWeapon : WeaponType, IWeapon
    {
        private IEnumerator Burst()
        {
            for (var i = 0; i < weaponStats.bursts; i++)
            {
                ShipAttackHandler.inst.Shoot(weaponStats);

                yield return new WaitForSeconds(weaponStats.fireRate);
            }
        }

        public void Attack()
        {
            StartCoroutine(Burst());
        }
    }
}
