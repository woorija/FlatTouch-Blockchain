using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[CustomPropertyDrawer(typeof(ScreenFaderBehaviour))]
public class ScreenFaderDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 2;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty colorProp = property.FindPropertyRelative("color");
        SerializedProperty saturationProp = property.FindPropertyRelative("saturation");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, colorProp);

        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, saturationProp);
    }
}
