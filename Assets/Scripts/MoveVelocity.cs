using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
    internal void Setup(Vector2 moveDir, float moveSpeed)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(moveDir * moveSpeed, ForceMode2D.Impulse);
    }
}
