using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Maz.Unity.EventFramework.Example
{
	[CustomPropertyDrawer(typeof(EventAction<>), true)]
	public class EventAcionEditor : PropertyDrawer
    {
		const float HEIGHT = 40;
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}
			return EditorGUI.GetPropertyHeight(property, label) + HEIGHT + 18f;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, label, true);
			var foldOut = position;
			foldOut.height = 18;
			property.isExpanded = EditorGUI.Foldout(foldOut, property.isExpanded, "", true);
			EditorGUI.BeginProperty(position, label, property);

			if (property.isExpanded)
			{
				position.y += 20f;

				var content = new GUIContent();
				var isPlaying = Application.isPlaying;

				var s = new SerializedObject(property.objectReferenceValue);
				SerializedProperty value = !isPlaying ? s.FindProperty("initialValue") : s.FindProperty("runtimeValue");

				content.text = !isPlaying ? "    Initial Value" : "    Runtime Value" ;

				s.UpdateIfRequiredOrScript();
				EditorGUI.PropertyField(position, value, content, true);
				s.ApplyModifiedProperties();


				//position.height = 20;
				position.height = 20f;
				float height = EditorGUI.GetPropertyHeight(property, label);

				position.y += height + 2f;

				GUI.enabled = Application.isPlaying;
				if (GUI.Button(position, "Raise"))
				{
					System.Type parentType = property.serializedObject.targetObject.GetType();
					System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);

					var o = (EventActionBase)fi.GetValue(property.serializedObject.targetObject);

					o.Raise();
				}
			}
			EditorGUI.EndProperty();
		}
	}
}