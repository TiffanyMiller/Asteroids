using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupSpawner : Spawner
{
    private Camera _cam;
    
    protected override void Awake()
    {
        base.Awake();
        _cam = Camera.main;
        onSpawn += SpawnPowerup;
    }

    private void OnDestroy()
    {
        onSpawn -= SpawnPowerup;
    }

    private void SpawnPowerup()
    {
        var randBool = Random.value > 0.5f;
        var randPowerup = randBool ? "Barrier" : "Blaster";
        ObjectPooler.inst.SpawnFromPool(randPowerup, RandomEdgePos(), RandomAngleInDirection());
    }
}
