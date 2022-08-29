using UnityEngine;

namespace Ship
{
    [RequireComponent(typeof(SpaceshipController))]
    public class ShipHealthHandler : MonoBehaviour
    {
        public static ShipHealthHandler inst;
        private Spaceship _ship;
        private int _health;

        [SerializeField] private Transform lifeHolder;
        [SerializeField] private GameObject life;
        [SerializeField] private GameObject shield;

        public bool ignoreDamage;
        
        private void Awake()
        {
            inst = this;
            
            _ship = GetComponent<SpaceshipController>().ship;
            
            _health = _ship.startHealth;

            for (var i = 0; i < _health; i++)
            {
                Instantiate(life, lifeHolder);
            }
            
            SetShield(false);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemy = col.gameObject.GetComponent<Combat.IEnemy>();
            
            if (enemy == null) return;

            if (_health > 0 && !ignoreDamage)
            {
                _health -= enemy.Damage;

                for (var i = 0; i < enemy.Damage; i++)
                {
                    if (lifeHolder.childCount > 0 && enemy.Damage <= lifeHolder.childCount)
                    {
                        Destroy(lifeHolder.GetChild(i).gameObject);
                    }
                    else if (enemy.Damage > lifeHolder.childCount)
                    {
                        // Destroy all lives if damage is greater than the number of lives
                        for (var j = 0; j < lifeHolder.childCount; j++)
                        {
                            Destroy(lifeHolder.GetChild(j).gameObject);
                        }
                    }
                }
            }

            if(_health <= 0) 
                GameManager.inst.GameOver();

            if (ignoreDamage)
            {
                SetShield(false);
                PowerupSpawner.powerupActive = false;
            }
        }

        public void SetShield(bool isShieldActive)
        {
            ignoreDamage = isShieldActive;
            shield.SetActive(isShieldActive);
        }
    }
}
