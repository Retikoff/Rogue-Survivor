using System.Collections;
using UnityEngine;

public class WhipController : MonoBehaviour, IWeapon
{
    [HideInInspector] public WeaponData WeaponData {get; set;}
    private const float PLAYER_OFFSET_X = 1f;

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
        GameObject newProjectile = Instantiate(WeaponData.projectile, this.transform);
        newProjectile.transform.position = new Vector3(transform.position.x + PLAYER_OFFSET_X, transform.position.y, transform.position.z);

        // rotate ?

        newProjectile.GetComponent<WhipProjectileController>().weaponData = WeaponData;
        SpriteRenderer projectileSpriteRenderer = newProjectile.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInHoldOut(projectileSpriteRenderer, 0.1f, 0f, 0.4f));
    }

    private IEnumerator FadeInHoldOut(SpriteRenderer spriteRenderer, float fadeInDuration, float holdDuration, float fadeOutDuration)
    {
        Color color = spriteRenderer.color;
        float totalDuration = fadeInDuration + holdDuration + fadeOutDuration;
        float elapsed = 0f;

        while (elapsed < totalDuration)
        {
            elapsed += Time.deltaTime;

            if (elapsed < fadeInDuration)
            {
                float progress = elapsed / fadeInDuration;
                color.a = Mathf.Lerp(0f, 1f, progress);
            }
            else if (elapsed < fadeInDuration + holdDuration)
            {
                color.a = 1f;
            }
            else
            {
                float progress = (elapsed - fadeInDuration - holdDuration) / fadeOutDuration;
                color.a = Mathf.Lerp(1f, 0f, progress);
            }

            spriteRenderer.color = color;
            yield return null;
        }

        color.a = 0f;
        spriteRenderer.color = color;
        Destroy(spriteRenderer.gameObject);
    }
}
