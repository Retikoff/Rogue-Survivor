using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ExpShardController : MonoBehaviour
{
    [SerializeField] private float expValue;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            Managers.ExperienceManager.GetExp(expValue);
            Destroy(this.gameObject);
        }
    }
}
