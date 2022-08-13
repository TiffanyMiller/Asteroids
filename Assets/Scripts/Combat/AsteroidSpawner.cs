using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private enum Direction { Left, Right, Top, Bottom }
        private Camera _cam;
        [SerializeField] private float secondsUntilSpawn;
        private Direction _dir;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Start()
        {
            CountdownTimer.Set(secondsUntilSpawn, SpawnAsteroid);
        }

        private void Update()
        {
            CountdownTimer.Run();
            
            if(CountdownTimer.IsTimerComplete()) 
                CountdownTimer.Set(secondsUntilSpawn, SpawnAsteroid);
        }

        private void SpawnAsteroid()
        {
            ObjectPooler.inst.SpawnFromPool("Asteroid", RandomEdgePos(), RandomAngleInDirection());
        }

        private Vector3 RandomAngleInDirection()
        {
            float angleDir;
                
            // Which way should the asteroid face?
            switch (_dir)
            {
                case Direction.Left: angleDir = -90;
                    break;
                case Direction.Right: angleDir = 90f;
                    break;
                case Direction.Top: angleDir = 180f;
                    break;
                default:
                case Direction.Bottom: angleDir = 0;
                    break;
            }

            var random = Random.Range(-45f, 45f);
            
            // Add randomness to direction
            var randAngle = new Vector3(0, 0, random + angleDir);

            return randAngle;
        }

        private Vector2 RandomEdgePos()
        {
            var randBool = Random.value > 0.5f;

            // Left/Right/Top/Bottom?
            var randEdge = Random.Range(0, 2);
            // Position on the edge
            var randPoint = Random.Range(0f, 1f);

            var randX = randBool ? randEdge : randPoint;
            var randY = randBool ? randPoint : randEdge;

            var randPos = new Vector2(randX, randY);

            // Add offset so that the position is just outside of bounds
            var xOffset = new Vector2(0.05f, 0);
            var yOffset = new Vector2(0, 0.05f);
            
            if (randX == 0) {
                randPos -= xOffset;
                _dir = Direction.Left;
            }
            if (randX >= 1) {
                randPos += xOffset;
                _dir = Direction.Right;
            }
            if (randY == 0) {
                randPos -= yOffset;
                _dir = Direction.Bottom;
            }
            if (randY >= 1) {
                randPos += yOffset;
                _dir = Direction.Top;
            }

            return _cam.ViewportToWorldPoint(randPos);
        }
    }
}
