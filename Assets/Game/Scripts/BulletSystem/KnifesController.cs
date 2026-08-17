using UnityEngine;

public class KnifesController : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [HideInInspector] public WeaponData Data;
    [SerializeField] private GameObject projectilePrefab;

    private float timer;

    private void Awake()
    {
        Data = Instantiate(data);
    }

    private void Update()
    {
        if (timer > Data.cooldown)
        {
            ShootProjectile();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }

    private void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newProjectile.transform.localScale = new Vector3(Data.area, Data.area, Data.area);
        newProjectile.GetComponent<KnifeProjectileController>().weaponController = this;
    }
}
