using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : Spawner
{
    protected override void Awake()
    {
        base.Awake();
        onSpawn += SpawnAsteroid;
    }

    private void OnDestroy()
    {
        onSpawn -= SpawnAsteroid;
    }

    private void SpawnAsteroid()
    {
        ObjectPooler.inst.SpawnFromPool("Asteroid", RandomEdgePos(), RandomAngleInDirection());
    }
}
