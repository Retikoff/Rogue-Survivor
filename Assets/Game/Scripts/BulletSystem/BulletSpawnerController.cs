using UnityEngine;

public class BulletSpawnerController : MonoBehaviour
{

    [SerializeField] private BulletSpawnerData Data;
    private GameObject spawnedBullet;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (Data.spawnerType == SpawnerType.Spin)
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }

        if (timer >= Data.firingRate)
        {
            Fire();
            timer = 0f;
        }
    }

    private void Fire()
    {
        if (Data.bulletPrefab)
        {
            spawnedBullet = Instantiate(Data.bulletPrefab, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<BulletController>().speed = Data.bulletSpeed;
            spawnedBullet.GetComponent<BulletController>().bulletLife = Data.bulletLifeTime;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
