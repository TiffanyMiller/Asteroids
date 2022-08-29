using System;
using UnityEngine;

namespace Powerups
{
    public class Powerup : MonoBehaviour, IPooledObject
    {
        private Rigidbody2D _rb;
        private Collider2D _col;
        private SpriteRenderer _rend;
        private Transform _child;
        
        [SerializeField] private float moveSpeed = 10f;
        protected Action onEffect;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<Collider2D>();
            _rend = GetComponent<SpriteRenderer>();
            _child = transform.GetChild(0);
        }

        public void OnObjectSpawn()
        {
            SetVisible(true);
            _rb.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Projectile") || col.gameObject.CompareTag("Player"))
            {
                onEffect?.Invoke();
                PowerupSpawner.powerupActive = true;
                SetVisible(false);
            }
        }

        private void SetVisible(bool isVisible)
        {
            _col.enabled = isVisible;
            _rend.enabled = isVisible;
            _child.gameObject.SetActive(isVisible);
        }
    }
}
