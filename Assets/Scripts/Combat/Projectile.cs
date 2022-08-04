using UnityEngine;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        public void Setup(Vector3 shootDir, float moveSpeed, float destroyTime)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);
            
            Destroy(gameObject, destroyTime);
        }
    }
}
