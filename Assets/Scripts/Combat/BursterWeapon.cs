using System.Collections;
using UnityEngine;

namespace Combat
{
    public class BursterWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        
        private IEnumerator Burst(Weapon weapon)
        {
            for (var i = 0; i < weapon.bursts; i++)
            {
                ShipAttackHandler.inst.Shoot(weapon);

                yield return new WaitForSeconds(weapon.fireRate);
            }
        }

        private void BurstAttack()
        {
            StartCoroutine(Burst(weapon));
        }
    }
}
