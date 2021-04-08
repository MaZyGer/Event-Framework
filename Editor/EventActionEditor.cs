using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Maz.Unity.EventFramework
{
	[CustomPropertyDrawer(typeof(EventAction), true)]
	public class EventAcionEditor : PropertyDrawer
    {
		const float HEIGHT = 40;
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}

			return EditorGUI.GetPropertyHeight(property, label) + 40;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var propPosition = position;

			propPosition.width = Screen.width - 90f;
			EditorGUI.PropertyField(propPosition, property, label, true);

			propPosition.x = Screen.width - 65f;
			propPosition.width = 60f;
			GUI.enabled = Application.isPlaying || EventFrameworkSettings.AllowRaiseInEditMode;

			if (GUI.Button(propPosition, "Raise"))
			{
				System.Type parentType = property.serializedObject.targetObject.GetType();
				System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);

				var o = (EventAction)fi.GetValue(property.serializedObject.targetObject);

				o.Raise();
			}
			
			
		}
	}
}