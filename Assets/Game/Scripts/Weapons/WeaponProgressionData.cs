using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponProgressionData", menuName = "Scriptable Objects/WeaponProgressionData")]
public class WeaponProgressionData : ScriptableObject
{
    [SerializeField] private List<WeaponStatValueRow> rows = new List<WeaponStatValueRow>();

    public IReadOnlyList<WeaponStatValueRow> Rows => rows;

    public float GetValue(int rowIndex, WeaponStats stat, float defaultValue = 0f)
    {
        if (rowIndex < 0 || rowIndex >= rows.Count)
            return defaultValue;

        var row = rows[rowIndex];
        for (int i = 0; i < row.Pairs.Count; i++)
        {
            if (row.Pairs[i].Stat == stat)
                return row.Pairs[i].Value;
        }
        return defaultValue;
    }

    public WeaponStatValuePair GetValue(int rowIndex, int colIndex)
    {
        if (rowIndex < 0 || rowIndex >= rows.Count)
            return null;
        
        var row = rows[rowIndex];
        return row.Pairs[colIndex];
    }

    public void SetValue(int rowIndex, WeaponStats stat, float value)
    {
        if (rowIndex < 0) return;

        while (rows.Count <= rowIndex)
            rows.Add(new WeaponStatValueRow());

        var row = rows[rowIndex];
        for (int i = 0; i < row.Pairs.Count; i++)
        {
            if (row.Pairs[i].Stat == stat)
            {
                row.Pairs[i].Value = value;
                return;
            }
        }

        row.Pairs.Add(new WeaponStatValuePair { Stat = stat, Value = value });
    }
}
