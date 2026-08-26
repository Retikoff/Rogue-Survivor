using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class KnifesController : MonoBehaviour, IWeapon
{
    [HideInInspector] public WeaponData WeaponData{ get; set;}
    private const float X_OFFSET = 1f;
    private const float Y_OFFSET = 0.5f;

    private float timer;


    private void Update()
    {
        if (timer > WeaponData.cooldown)
        {
            ShootProjectiles();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }

    private void ShootProjectiles()
    {
        for (int i = 0; i < WeaponData.projectileAmount; i++)
        {
            GameObject newProjectile = Instantiate(WeaponData.projectile, transform.position, transform.rotation);
            Vector3 newPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(0, 0.5f) * X_OFFSET, transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f) * Y_OFFSET, transform.position.z);
            newProjectile.transform.position = newPosition;
            newProjectile.transform.localScale = new Vector3(WeaponData.area, WeaponData.area, WeaponData.area);
            newProjectile.GetComponent<KnifeProjectileController>().weaponData = WeaponData;
        }
    }
}
