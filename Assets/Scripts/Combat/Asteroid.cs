using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat
{
    public class Asteroid : MonoBehaviour, IEnemy, IPooledObject
    {
        private Rigidbody2D _rb;
        [SerializeField] private int stepsToDestroy = 3;
        [Tooltip("The number of pieces the asteroid splits into when it breaks")]
        [SerializeField] private int divisions = 2;

        [SerializeField] private int pointsPerFinalPieces = 10;
        [SerializeField] private float moveSpeed = 10f;

        public int Damage => stepsToDestroy;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Vector2.Distance(transform.position, Vector2.zero) > 25f)
            {
                ResetAsteroid();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Projectile"))
            {
                col.gameObject.SetActive(false);
                
                // If there are still stepsToDestroy, break the asteroid
                if (stepsToDestroy > 1)
                    Break();
                else
                {
                    // If this is the final piece, destroy it and update score
                    GameManager.inst.UpdateScore(pointsPerFinalPieces);
                    ResetAsteroid();
                    gameObject.SetActive(false);
                }
            }
        }

        public void OnObjectSpawn()
        {
            _rb.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }

        private void Break()
        {
            var tr = transform;
            var dist = tr.localScale.x / divisions;
            var xPos = tr.position.x - dist;
            stepsToDestroy--;
            
            for (var i = 0; i < divisions; i++)
            {
                var broken =
                    ObjectPooler.inst.SpawnFromPool("Asteroid", new Vector2(xPos, tr.position.y), RandomAngleZ());
                
                xPos += dist * 2;
                broken.transform.localScale = transform.localScale / divisions;
                broken.GetComponent<Asteroid>().stepsToDestroy = stepsToDestroy;
            }
            
            ResetAsteroid();
            gameObject.SetActive(false);
        }

        private void ResetAsteroid()
        {
            transform.localScale = new Vector3(2, 2, 2);
            stepsToDestroy = 3;
            _rb.velocity = Vector2.zero;
        }

        private Vector3 RandomAngleZ()
        {
            var random = Random.Range(-360, 360);
            return new Vector3(0, 0, random);
        }
    }
}
