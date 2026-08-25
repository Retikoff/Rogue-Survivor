using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject projectile;
    public int maxLevel = 9;
    public float baseDamage = 1;
    public float area = 1;
    public float speed = 50;
    public float projectileAmount = 1;
    public float duration = 1;
    public int pierce = 0; // пробивание (число врагов)
    public float cooldown;
    public float knockback; // сила отталкивания
    public float critMulti;
    public float critChance;
    public bool isRotatable = true;
    //public bool blockByWalls;
}
