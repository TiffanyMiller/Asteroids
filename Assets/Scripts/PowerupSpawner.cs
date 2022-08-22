using Random = UnityEngine.Random;

public class PowerupSpawner : Spawner
{
    protected override void Awake()
    {
        base.Awake();
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
