using Ship;

namespace Powerups
{
    public class BarrierPowerup : Powerup
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
