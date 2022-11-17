using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner_Old : MonoBehaviour
{
    [SerializeField]
    private Bullet_Old Prefab;

    [SerializeField]
    private BoxCollider SpawnArea;

    [SerializeField]
    private int BulletsPerSecond = 10;

    [SerializeField]
    private float Speed = 5f;

    [SerializeField]
    private bool UseObjectPool = false;

    private float LastSpawnTime;

    private void Update()
    {
        float delay = 1f / BulletsPerSecond;
        if (LastSpawnTime + delay < Time.time)
        {
            int bulletsToSpawnInFrame = Mathf.CeilToInt(Time.deltaTime / delay);
            while (bulletsToSpawnInFrame > 0)
            {
                Bullet_Old instance =
                    Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                instance.transform.SetParent(transform, true);

                SpawnBullet (instance);

                bulletsToSpawnInFrame--;
            }

            LastSpawnTime = Time.time;
        }
    }

    private void SpawnBullet(Bullet_Old Instance)
    {
        Vector3 spawnLocation =
            new Vector3(SpawnArea.transform.position.x +
                SpawnArea.center.x +
                Random
                    .Range(-1 * SpawnArea.bounds.extents.x,
                    SpawnArea.bounds.extents.x),
                SpawnArea.transform.position.y +
                SpawnArea.center.y +
                Random
                    .Range(-1 * SpawnArea.bounds.extents.y,
                    SpawnArea.bounds.extents.y),
                SpawnArea.transform.position.z +
                SpawnArea.center.z +
                Random
                    .Range(-1 * SpawnArea.bounds.extents.z,
                    SpawnArea.bounds.extents.z));

        Instance.transform.position = spawnLocation;

        Instance.Shoot(spawnLocation, SpawnArea.transform.right, Speed);
    }
}
