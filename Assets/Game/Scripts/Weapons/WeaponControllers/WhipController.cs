using System.Collections;
using UnityEngine;

public class WhipController : MonoBehaviour, IWeapon
{
    [HideInInspector] public WeaponData WeaponData {get; set;}
    private const float PLAYER_OFFSET_X = 2f;
    private const float SCALE_RATIO= 0.5f;
    private const float DELAY_BETWEEN_PROJECTILES = 0.15f;
    private const float OFFSET_BETWEEN_PROJECTILES_Y = 0.15f;

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
            float isEvenCoeff = (i & 1) == 0 ? 1f : -1f;
            GameObject newProjectile = Instantiate(WeaponData.projectile, this.transform);
            newProjectile.transform.localPosition = new Vector3(isEvenCoeff * (transform.localPosition.x + PLAYER_OFFSET_X), transform.localPosition.y + OFFSET_BETWEEN_PROJECTILES_Y * i, transform.localPosition.z);
            
            newProjectile.transform.localScale = new Vector3(isEvenCoeff * SCALE_RATIO * WeaponData.area, SCALE_RATIO * WeaponData.area, SCALE_RATIO * WeaponData.area);

            SpriteRenderer projectileSpriteRenderer = newProjectile.GetComponent<SpriteRenderer>();
            newProjectile.GetComponent<WhipProjectileController>().weaponData = WeaponData;
            newProjectile.SetActive(false);
            StartCoroutine(FadeInHoldOut(projectileSpriteRenderer, 0.15f, 0.05f, 0.15f, DELAY_BETWEEN_PROJECTILES * ( i / 2f)));
        }
    }

    private IEnumerator FadeInHoldOut(SpriteRenderer spriteRenderer, float fadeInDuration, float holdDuration, float fadeOutDuration, float delay)
    {
        yield return new WaitForSeconds(delay);

        spriteRenderer.gameObject.SetActive(true);

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
