using UnityEngine;
using UnityEditor;

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
    
        if (attr.compareValue == null)
        {
            if (conditionProp.propertyType == SerializedPropertyType.Boolean)
            {
                return conditionProp.boolValue;
            }
        }
        else
        {
            switch (conditionProp.propertyType)
            {
                case SerializedPropertyType.Enum:
                    return conditionProp.enumValueIndex == (int)attr.compareValue;
                case SerializedPropertyType.Integer:
                    return conditionProp.intValue == (int)attr.compareValue; 
                case SerializedPropertyType.Float:
                    return conditionProp.floatValue == (float)attr.compareValue;
                case SerializedPropertyType.String:
                    return conditionProp.stringValue == (string)attr.compareValue;
            }
        }
        return true;
    }

    
}
