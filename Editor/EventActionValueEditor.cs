using System.Collections;
using System.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Maz.Unity.EventFramework.Example
{
	//public class GenericItemDrawer<T> : OdinValueDrawer<T>
	//{

	//}

	[CustomPropertyDrawer(typeof(EventActionValue), true)]
	public class EventActionValueEditor : PropertyDrawer
	{
		UnityEngine.Object testObject = null;
		const float HEIGHT = 40;
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}

			return EditorGUI.GetPropertyHeight(property, label) + 22;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{

			var genericType = GetGenericType(property);
			position.height = 18;
			EditorGUI.PropertyField(position, property, label, false);
			if (property.isExpanded)
			{


				position.y += 18;
				EditorGUI.BeginProperty(position, label, property);

				var eventActionProp = property.FindPropertyRelative("EventAction");
				 
				if(eventActionProp != null)
				{
					eventActionProp.objectReferenceValue = EditorGUI.ObjectField(position, new GUIContent($"Event ({genericType?.Name ?? ""})"),
													   eventActionProp.objectReferenceValue,
													   typeof(EventActionBase),
													   true);
				}

				position.y += 20;

				var valueProp = property.FindPropertyRelative("value");
				EditorGUI.PropertyField(position, valueProp, new GUIContent("Value"), true);

				position.height = 20f;

				position.y += 20f;

				GUI.enabled = Application.isPlaying;
				if (GUI.Button(position, "Raise"))
				{
					System.Type parentType = property.serializedObject.targetObject.GetType();
					System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);

					var o = (EventActionValue)fi.GetValue(property.serializedObject.targetObject);

					o.Raise();
				}

				EditorGUI.EndProperty();
			}
		}

		public Type GetGenericType(SerializedProperty property)
		{
			System.Type parentType = property.serializedObject.targetObject.GetType();
			System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
			if (fi != null)
			{
				if(fi.FieldType.IsGenericType)
				{
					return fi.FieldType.GetGenericArguments()[0];
				}
			}

			return null;
		}

		static T ConvertValue<T>(string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
	}
}