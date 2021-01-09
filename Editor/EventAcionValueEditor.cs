using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Maz.Unity.EventFramework.Example
{
    [CustomPropertyDrawer(typeof(EventActionValueBase), true)]
    public class EventAcionValueEditor : PropertyDrawer
    {
		const float HEIGHT = 40;
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}
			return EditorGUI.GetPropertyHeight(property, label) + HEIGHT;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, label, true);

			EditorGUI.BeginProperty(position, label, property);

			if(property.isExpanded)
			{
				//position.height = 20;
				position.height = 20f;
				float height = EditorGUI.GetPropertyHeight(property, label);

				position.y += height + 2f;

				GUI.enabled = Application.isPlaying;
				if (GUI.Button(position, "Raise"))
				{
					System.Type parentType = property.serializedObject.targetObject.GetType();
					System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);

					var o = (EventActionValueBase)fi.GetValue(property.serializedObject.targetObject);

					o.Raise();
				}
			}

			EditorGUI.EndProperty();
		}
	}
}