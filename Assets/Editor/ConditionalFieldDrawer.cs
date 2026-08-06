using UnityEngine;
using UnityEditor;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System.Linq;

/** 
    Attribute ConditionalField для отображения полей в инспекторе в зависимости от значения других полей

    На момент создания нужны для ScriptableObjects ачивок 
    Например: Если ачивка для оружия то поля должны быть под Уровень оружия достигнутого игроком, 
              Если ачивка для персонажа то поля должны быть под Карту уровень игрока и тд.
**/

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (ShouldShow(property))
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        return 0;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (ShouldShow(property))
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    private bool ShouldShow(SerializedProperty property)
    {
        ConditionalFieldAttribute attr = (ConditionalFieldAttribute)attribute;
        SerializedProperty conditionProp = property.serializedObject.FindProperty(attr.conditionalField);

        if (conditionProp == null) return true;
    
        if (attr.compareValues == null || attr.compareValues.Length == 0)
        {
            if (conditionProp.propertyType == SerializedPropertyType.Boolean)
            {
                return conditionProp.boolValue;
            }
            return true;
        }

        if (conditionProp.propertyType == SerializedPropertyType.Enum)
        {
            int currentEnumValue = conditionProp.enumValueIndex;
            return attr.compareValues.Any(val => (int)val == currentEnumValue);
        }

        if (conditionProp.propertyType == SerializedPropertyType.Integer)
        {
            int currentInt = conditionProp.intValue;
            return attr.compareValues.Any(val => (int)val == currentInt);
        }

        if (conditionProp.propertyType == SerializedPropertyType.Float)
        {
            float currentFloat = conditionProp.floatValue;
            return attr.compareValues.Any(val => Mathf.Approximately((float)val, currentFloat));
        }

        if (conditionProp.propertyType == SerializedPropertyType.String)
        {
            string currentString = conditionProp.stringValue;
            return attr.compareValues.Any(val => (string)val == currentString);
        }

        return true;
    }

    
}
