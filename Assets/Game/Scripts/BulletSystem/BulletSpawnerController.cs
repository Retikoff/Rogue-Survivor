using UnityEngine;

public class BulletSpawnerController : MonoBehaviour
{
    public BulletSpawnerData Data = null;
    private GameObject spawnedBullet;
    private float timer = 0f;

    void Update()
    {
        if (Data == null) return;

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
            BulletController newBulletContoller = spawnedBullet.GetComponent<BulletController>();
            newBulletContoller.speed = Data.bulletSpeed;
            newBulletContoller.bulletLife = Data.bulletLifeTime;
            newBulletContoller.damage = Data.bulletDamage;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
