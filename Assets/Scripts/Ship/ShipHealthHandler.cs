using UnityEngine;

namespace Ship
{
    [RequireComponent(typeof(SpaceshipController))]
    public class ShipHealthHandler : MonoBehaviour
    {
        private Spaceship _ship;
        private int _health;
        [SerializeField] private Transform lives;
        [SerializeField] private GameObject life;
        
        private void Awake()
        {
            _ship = GetComponent<SpaceshipController>().ship;
            _health = _ship.startHealth;

            for (var i = 0; i < _health; i++)
            {
                Instantiate(life, lives);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemy = col.gameObject.GetComponent<Combat.IEnemy>();
            
            if (enemy == null) return;

            if (_health > 0)
            {
                _health -= enemy.Damage;

                for (var i = 0; i < enemy.Damage; i++)
                {
                    Destroy(lives.GetChild(i).gameObject);
                }
            }

            if(_health <= 0) 
                GameManager.inst.GameOver();
        }
    }
}
