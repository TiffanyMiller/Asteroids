using System;
using UnityEngine;

namespace Powerups
{
    public class Powerup : MonoBehaviour, IPooledObject
    {
        private Rigidbody2D _rb;
        [SerializeField] private float moveSpeed = 10f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnObjectSpawn()
        {
            _rb.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Projectile"))
            {
                col.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
