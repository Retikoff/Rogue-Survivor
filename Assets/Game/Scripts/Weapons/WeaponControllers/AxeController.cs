using UnityEngine;

public class AxeController : MonoBehaviour, IWeapon
{
    [HideInInspector] public WeaponData WeaponData {get; set;}
    private const float FORCE_MULTIPLIER = 5f;

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
            newProjectile.transform.localScale = new Vector3(WeaponData.area, WeaponData.area, WeaponData.area);
            Rigidbody2D newProjRb = newProjectile.GetComponent<Rigidbody2D>();
            newProjRb.AddForce(FORCE_MULTIPLIER * (Vector2.right * UnityEngine.Random.Range(-0.5f , 0.5f) + Vector2.up * UnityEngine.Random.Range(1f, 1.75f)), ForceMode2D.Impulse);
            newProjRb.gravityScale = WeaponData.speed;

            newProjectile.GetComponent<AxeProjectileController>().weaponData = WeaponData;
        }
    }
}
