using System;
using UnityEngine;

[Serializable]
public class KnifesController : MonoBehaviour, IWeapon
{
    [HideInInspector] public WeaponData WeaponData{ get; set;}

    private float timer;


    private void Update()
    {
        if (timer > WeaponData.cooldown)
        {
            ShootProjectile();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }

    private void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(WeaponData.projectile, transform.position, transform.rotation);
        newProjectile.transform.localScale = new Vector3(WeaponData.area, WeaponData.area, WeaponData.area);
        newProjectile.GetComponent<KnifeProjectileController>().weaponController = this;
    }
}
