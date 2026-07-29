using UnityEngine;

public class ConditionalFieldAttribute : PropertyAttribute
{
    public string conditionalField;
    public object compareValue;

    public ConditionalFieldAttribute(string conditionalField, object compareValue = null)
    {
        this.conditionalField = conditionalField;
        this.compareValue = compareValue;
    }   
}
