using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Foo))]
public class FooDrawer : PropertyDrawer
{
	private static
	(bool, bool) State(in SerializedProperty a, in SerializedProperty b)
	{
		if (a.objectReferenceValue as Object) return (true, false);
		if (b.objectReferenceValue as Object) return (false, true);
		return (true, true);
	}

	private static Rect NextPosition(in Rect position, in float diff)
	{
		return new Rect(
			position.x,
			position.y + diff,
			position.width,
			position.height
		);
	}

	public override
	float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true) * 2 / 3;
	}

	public override
	void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty obj = property.FindPropertyRelative("gameObject");
		SerializedProperty handle = property.FindPropertyRelative("handle");
		int id = GUIUtility.GetControlID(FocusType.Passive);
		(bool enableObj, bool enableHandle) = FooDrawer.State(obj, handle);

		position = EditorGUI.PrefixLabel(position, id,label);
		GUI.enabled = enableObj;
		EditorGUI.PropertyField(position, obj, GUIContent.none, true);
		position = FooDrawer.NextPosition(position, position.height / 2);
		GUI.enabled = enableHandle;
		EditorGUI.PropertyField(position, handle, GUIContent.none, true);
		GUI.enabled = true;
	}
}
