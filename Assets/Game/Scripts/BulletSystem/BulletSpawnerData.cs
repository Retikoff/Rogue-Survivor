using UnityEngine;

[CreateAssetMenu(fileName = "BulletSpawnerData", menuName = "Scriptable Objects/BulletSpawnerData")]
public class BulletSpawnerData : ScriptableObject
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletLifeTime;
    public float bulletSpeed;
    [Header("Spawner")]
    public SpawnerType spawnerType;
    public float firingRate;
}
