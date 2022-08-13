using UnityEngine;

namespace Combat
{
    public class Asteroid : MonoBehaviour, IEnemy, IPooledObject
    {
        [SerializeField] private int stepsToDestroy = 3;
        [Tooltip("The number of pieces the asteroid splits into when it breaks")]
        [SerializeField] private int divisions = 2;

        [SerializeField] private int pointsPerFinalPieces = 10;
        public int Damage => stepsToDestroy;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Projectile"))
            {
                Destroy(col.gameObject);
                
                // If there are still stepsToDestroy, break the asteroid
                if (stepsToDestroy > 1)
                    Break();
                else
                {
                    // If this is the final piece, destroy it and update score
                    GameManager.inst.UpdateScore(pointsPerFinalPieces);
                    Destroy(gameObject);
                }
            }
        }

        public void OnObjectSpawn()
        {
            // set its position, rotation, and speed
        }

        private void Break()
        {
            var tr = transform;
            var dist = tr.localScale.x / divisions;
            var xPos = tr.position.x - dist;
            
            for (var i = 0; i < divisions; i++)
            {
                var broken = Instantiate(gameObject, new Vector2(xPos, transform.position.y), Quaternion.identity);
                
                xPos += dist * 2;
                broken.transform.localEulerAngles = RandomAngleZ();
                broken.transform.localScale = transform.localScale / divisions;

                var newAsteroid = broken.GetComponent<Asteroid>();
                newAsteroid.stepsToDestroy--;

                broken.GetComponent<MoveVelocity>().Setup(broken.transform.up, 1f, 10f);
            }
            
            // instead of destroying it, update its position and return it to queue
            Destroy(gameObject);
        }

        private Vector3 RandomAngleZ()
        {
            var random = Random.Range(-360, 360);
            return new Vector3(0, 0, random);
        }
    }
}
