using UnityEngine;

public class Spawner : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.inst;
    }

    private void FixedUpdate()
    {
        _objectPooler.SpawnFromPool("Asteroid", transform.position, Quaternion.identity);
    }
}
