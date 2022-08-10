using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private int stepsToDestroy = 2;
        [Tooltip("The number of pieces the asteroid splits into when it breaks")]
        [SerializeField] private int breakPieces = 2;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Projectile"))
            {
                Destroy(col.gameObject);
                
                // If there are still stepsToDestroy, break the asteroid
                if (stepsToDestroy > 1)
                    Break();
                else Destroy(gameObject);
            }
        }

        private void Break()
        {
            for (var i = 0; i < breakPieces; i++)
            {
                var broken = Instantiate(gameObject, transform.position, Quaternion.identity);
                
                broken.transform.position = RandomPosAroundTransform();
                broken.transform.localEulerAngles = RandomAngleZ();
                broken.transform.localScale = transform.localScale / breakPieces;
                
                broken.GetComponent<Asteroid>().stepsToDestroy--;
                
                broken.GetComponent<MoveVelocity>().Setup(broken.transform.up, 1f, 10f);
                
                print(broken.name + broken.transform.position);
            }
            
            Destroy(gameObject);
        }

        private Vector3 RandomAngleZ()
        {
            var random = Random.Range(-360, 360);
            return new Vector3(0, 0, random);
        }
        
        private Vector2 RandomPosAroundTransform()
        {
            var t = transform;
            var pos = t.position;
            
            var random = new Vector3(Mathf.Sin(Random.Range(0,360)*Mathf.Deg2Rad), 0, Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad));
            var randPos = pos + random;
            var dir = (pos - randPos).normalized;
            var radius = t.localScale.x;
            
            return pos + (dir * radius);
        }
    }
}
