using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private enum Direction { Left, Right, Top, Bottom }
    private Direction _dir;
    private Camera _cam;
    [SerializeField] private float secondsUntilSpawn;
    
    protected Action onSpawn;
    private CountdownTimer _timer;

    protected virtual void Awake()
    {
        _cam = Camera.main;
        _timer = new CountdownTimer();
    }

    private void Start()
    {
        _timer.Set(secondsUntilSpawn, onSpawn);
    }

    private void Update()
    {
        _timer.Run();
            
        if(_timer.IsTimerComplete()) 
            _timer.Set(secondsUntilSpawn, onSpawn);
    }
    
    protected Vector3 RandomAngleInDirection()
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

    protected Vector2 RandomEdgePos()
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