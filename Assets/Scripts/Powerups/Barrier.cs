using Ship;
using UnityEngine;

namespace Powerups
{
    public class Barrier : Powerup
    {
        private ShipHealthHandler _shipHealth;

        protected override void Awake()
        {
            base.Awake();

            _shipHealth = ShipHealthHandler.inst;

            onEffect = ActivateShield;
        }

        private void ActivateShield()
        {
            _shipHealth.SetShield(true);
        }
    }
}
