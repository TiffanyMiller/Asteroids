using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupSpawner : Spawner
{
    public static bool powerupActive = false;
    [SerializeField] private ObjectPool powerupPool;
    
    protected override void Awake()
    {
        base.Awake();
        
        powerupPool.SetupPool();
        
        onSpawn += SpawnPowerup;
    }

    private void OnDestroy()
    {
        onSpawn -= SpawnPowerup;
    }

    private void SpawnPowerup()
    {
        var randomPoolIndex = Random.Range(0, powerupPool.pools.Count);
        var randomFromPool = powerupPool.pools[randomPoolIndex].tag;
        
        if(!powerupActive)
            powerupPool.SpawnFromPool(randomFromPool, RandomEdgePos(), RandomAngleInDirection());
    }
}
