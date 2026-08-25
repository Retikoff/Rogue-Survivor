using System;
using UnityEngine;

[Serializable]
public class GarlicController : MonoBehaviour, IWeapon
{
    [HideInInspector] public WeaponData WeaponData {get; set;}
    private const float RADIUS_RATIO = 1f;
    private const float VISUAL_RADIUS_RATIO = 2f;
    private const float Y_OFFSET = -0.4f;
    private float timer = 0f;

    private void Start()
    {
        CircleCollider2D newCollider = gameObject.AddComponent<CircleCollider2D>();
        newCollider.isTrigger = true;
        newCollider.offset = new Vector2(0f, Y_OFFSET);
        newCollider.radius = RADIUS_RATIO * WeaponData.area;
        //visual
        AddVisual();
    }

    private void AddVisual()
    {
        GameObject visual = Instantiate(WeaponData.projectile, this.transform);
        visual.transform.localPosition = new Vector3(0f, Y_OFFSET, 0f);
        visual.transform.localScale = new Vector3(VISUAL_RADIUS_RATIO * WeaponData.area, VISUAL_RADIUS_RATIO * WeaponData.area, VISUAL_RADIUS_RATIO * WeaponData.area);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timer == 0)
        {
            collision.SendMessage("TakeDamage", WeaponData.baseDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    // optimization???
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyManager>() != null)
        {
            if (timer >= WeaponData.cooldown)
            {
                float dealDamage = WeaponData.baseDamage;
                if (UnityEngine.Random.Range(0, 100) < WeaponData.critChance)
                {
                    dealDamage *= WeaponData.critMulti;
                }

                collision.SendMessage("TakeDamage", dealDamage);
                timer = 0f;
            }

            timer += Time.deltaTime;
        }

    }
}
