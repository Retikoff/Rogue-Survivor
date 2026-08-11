#if UNITY_EDITOR
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUtils.Editor
{
    [CustomPropertyDrawer(typeof(Array2D<>))]
    public class Array2DDrawer : PropertyDrawer
    {
        private const float CELL_SIZE = 100f;
        private const float LABEL_WIDTH = 50f;
        private bool showArray = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var rowsProp = property.FindPropertyRelative("rows");
            var columnsProp = property.FindPropertyRelative("columns");
            var flatArrayProp = property.FindPropertyRelative("flatArray");

            if (rowsProp == null || columnsProp == null || flatArrayProp == null)
            {
                EditorGUI.LabelField(position, "Invalid Array2D property");
                return;
            }

            int rows = rowsProp.intValue;
            int columns = columnsProp.intValue;

            var foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            showArray = EditorGUI.Foldout(foldoutRect, showArray, $"{label.text} [{rows}x{columns}]", true);

            if (showArray && rows > 0 && columns > 0)
            {
                EditorGUI.indentLevel++;

                var sizeRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginChangeCheck();

                var halfWidth = (sizeRect.width - 10) / 2;
                var rowsRect = new Rect(sizeRect.x, sizeRect.y, halfWidth, sizeRect.height);
                var columnsRect = new Rect(sizeRect.x + halfWidth + 10, sizeRect.y, halfWidth, sizeRect.height);

                EditorGUIUtility.labelWidth = 40;

                int newRows = EditorGUI.IntField(rowsRect, "Rows", rows);
                int newColumns = EditorGUI.IntField(columnsRect, "Columns", columns);

                if (EditorGUI.EndChangeCheck())
                {
                    ResizeArray(property, newRows, newColumns);
                    flatArrayProp = property.FindPropertyRelative("flatArray");
                    rows = newRows;
                    columns = newColumns;
                }

                if (flatArrayProp.arraySize == rows * columns)
                {
                    var gridStartY = sizeRect.y + EditorGUIUtility.singleLineHeight + 4;

                    for (int j = 0; j < columns; j++)
                    {
                        var headerRect = new Rect(
                            position.x + LABEL_WIDTH + j * (CELL_SIZE + 2),
                            gridStartY,
                            CELL_SIZE,
                            EditorGUIUtility.singleLineHeight
                        );
                        EditorGUI.LabelField(headerRect, $"[{j}]", EditorStyles.boldLabel);
                    }

                    for (int i = 0; i < rows; i++)
                    {
                        var rowLabelRect = new Rect(
                            position.x,
                            gridStartY + (i + 1) * (EditorGUIUtility.singleLineHeight + 2),
                            LABEL_WIDTH,
                            EditorGUIUtility.singleLineHeight
                        );
                        EditorGUI.LabelField(rowLabelRect, $"[{i}]", EditorStyles.boldLabel);

                        for (int j = 0; j < columns; j++)
                        {
                            var cellRect = new Rect(
                                position.x + LABEL_WIDTH + j * (CELL_SIZE + 2),
                                gridStartY + (i + 1) * (EditorGUIUtility.singleLineHeight + 2),
                                CELL_SIZE,
                                EditorGUIUtility.singleLineHeight
                            );

                            int index = i * columns + j;
                            if (index < flatArrayProp.arraySize)
                            {
                                var element = flatArrayProp.GetArrayElementAtIndex(index);
                                if (element != null)
                                {
                                    EditorGUI.PropertyField(cellRect, element, GUIContent.none);
                                }
                            }
                        }
                    }
                }

                EditorGUI.indentLevel--;
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var rowsProp = property.FindPropertyRelative("rows");
            var columnsProp = property.FindPropertyRelative("columns");

            if (rowsProp == null || columnsProp == null)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            int rows = rowsProp.intValue;
            int columns = columnsProp.intValue;

            if (!showArray || rows <= 0 || columns <= 0)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = EditorGUIUtility.singleLineHeight * 2 + 6;
            height += (rows + 1) * (EditorGUIUtility.singleLineHeight + 2);
            return height;
        }

        private void ResizeArray(SerializedProperty property, int newRows, int newColumns)
        {
            var rowsProp = property.FindPropertyRelative("rows");
            var columnsProp = property.FindPropertyRelative("columns");
            var flatArrayProp = property.FindPropertyRelative("flatArray");

            int oldRows = rowsProp.intValue;
            int oldColumns = columnsProp.intValue;

            var oldData = new object[oldRows * oldColumns];
            for (int i = 0; i < Mathf.Min(oldRows * oldColumns, flatArrayProp.arraySize); i++)
            {
                var element = flatArrayProp.GetArrayElementAtIndex(i);
                if (element != null)
                {
                    switch (element.propertyType)
                    {
                        case SerializedPropertyType.Integer:
                            oldData[i] = element.intValue;
                            break;
                        case SerializedPropertyType.Float:
                            oldData[i] = element.floatValue;
                            break;
                        case SerializedPropertyType.Boolean:
                            oldData[i] = element.boolValue;
                            break;
                        case SerializedPropertyType.String:
                            oldData[i] = element.stringValue;
                            break;
                        default:
                            oldData[i] = null;
                            break;
                    }
                }
            }

            rowsProp.intValue = newRows;
            columnsProp.intValue = newColumns;
            flatArrayProp.arraySize = newRows * newColumns;

            for (int i = 0; i < Mathf.Min(oldRows, newRows); i++)
            {
                for (int j = 0; j < Mathf.Min(oldColumns, newColumns); j++)
                {
                    int oldIndex = i * oldColumns + j;
                    int newIndex = i * newColumns + j;

                    if (oldIndex < oldData.Length && newIndex < flatArrayProp.arraySize)
                    {
                        var element = flatArrayProp.GetArrayElementAtIndex(newIndex);
                        if (element != null && oldData[oldIndex] != null)
                        {
                            switch (element.propertyType)
                            {
                                case SerializedPropertyType.Integer:
                                    element.intValue = (int)oldData[oldIndex];
                                    break;
                                case SerializedPropertyType.Float:
                                    element.floatValue = (float)oldData[oldIndex];
                                    break;
                                case SerializedPropertyType.Boolean:
                                    element.boolValue = (bool)oldData[oldIndex];
                                    break;
                                case SerializedPropertyType.String:
                                    element.stringValue = (string)oldData[oldIndex];
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

}
#endif