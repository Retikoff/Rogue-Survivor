using UnityEngine;

public class ConditionalFieldAttribute : PropertyAttribute
{
    public string conditionalField;
    public object[] compareValues;

    public ConditionalFieldAttribute(string conditionalField, params object[] compareValues)
    {
        this.conditionalField = conditionalField;
        this.compareValues = compareValues;
    }   
}
