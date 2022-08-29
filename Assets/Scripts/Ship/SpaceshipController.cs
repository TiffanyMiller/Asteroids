using System;
using System.Collections;
using UnityEngine;

namespace Ship
{
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] internal Spaceship ship;

        internal Action onMove, onEndMove, onEndRotate, onShoot;
        internal Action<int> onRotate;

        [SerializeField] private KeyCode move, leftRotate, rightRotate, shoot;

        private bool AnyKeyPressed =>
            Input.GetKeyDown(shoot) || Input.GetKeyDown(leftRotate) || Input.GetKeyDown(rightRotate) ||
            Input.GetKeyDown(move);

        public static bool firstKeyPressed = false;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sprite = ship.sprite;

            // Collider is set up according to the sprite
            gameObject.AddComponent<PolygonCollider2D>();
        }

        private void Start()
        {
            StartCoroutine(WaitForKeyPress());
        }

        private IEnumerator WaitForKeyPress()
        {
            yield return new WaitUntil(() => AnyKeyPressed && CutsceneAnimation.cutsceneComplete);

            firstKeyPressed = true;
        }
        
        private void Update()
        {
            if (CutsceneAnimation.cutsceneComplete)
            {
                if (Input.GetKeyDown(shoot))
                    onShoot?.Invoke();

                if(Input.GetKey(move)) onMove?.Invoke();
                else onEndMove?.Invoke();
    
                if (Input.GetKey(leftRotate)) onRotate.Invoke(1);
    
                if (Input.GetKey(rightRotate)) onRotate.Invoke(-1);
    
                if (!Input.GetKey(leftRotate) && !Input.GetKey(rightRotate)) onEndRotate.Invoke();
            }
            
            if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }
}