using System;
using UnityEngine;

namespace Ship
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] internal Spaceship ship;

        internal Action onMove, onEndMove, onEndRotate, onShoot;
        internal Action<int> onRotate;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sprite = ship.sprite;

            // Collider is set up according to the sprite
            gameObject.AddComponent<PolygonCollider2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                GameManager.inst.startGame = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
                onShoot?.Invoke();

            if(Input.GetKey(KeyCode.W)) onMove?.Invoke();
            else onEndMove?.Invoke();
        
            if (Input.GetKey(KeyCode.A)) onRotate.Invoke(1);
        
            if (Input.GetKey(KeyCode.D)) onRotate.Invoke(-1);
        
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) onEndRotate.Invoke();
        }
    }
}