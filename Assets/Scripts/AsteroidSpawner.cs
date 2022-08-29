using UnityEngine;

public class AsteroidSpawner : Spawner
{
    [SerializeField] private ObjectPool asteroidPool;
    
    protected override void Awake()
    {
        base.Awake();
        
        asteroidPool.SetupPool();
        
        onSpawn += SpawnAsteroid;
    }

    private void OnDestroy()
    {
        onSpawn -= SpawnAsteroid;
    }

    private void SpawnAsteroid()
    {
        asteroidPool.SpawnFromPool("Asteroid", RandomEdgePos(), RandomAngleInDirection());
    }
}
