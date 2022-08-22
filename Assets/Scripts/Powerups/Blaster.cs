using Combat;
using UnityEngine;

namespace Powerups
{
    public class Blaster : Powerup
    {
        private CountdownTimer _timer;
        [SerializeField] private Weapon blasterWeapon;
        private ShipAttackHandler _shipAttacker;
        
        protected override void Awake()
        {
            base.Awake();

            _shipAttacker = ShipAttackHandler.inst;
            
            _timer = new CountdownTimer();
            
            onEffect = BlastForSeconds;
        }

        private void Update()
        {
            _timer.Run();
        }
        
        private void ShootBlaster()
        {
            _shipAttacker.Shoot(blasterWeapon);
        }

        private void StopShooting()
        {
            CancelInvoke(nameof(ShootBlaster));
            gameObject.SetActive(false);
        }
        
        private void BlastForSeconds()
        {
            _timer.Set(blasterWeapon.duration, StopShooting);
            InvokeRepeating(nameof(ShootBlaster), blasterWeapon.fireRate, blasterWeapon.fireRate);
        }
    }
}
