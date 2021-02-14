using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Maz.Unity.EventFramework.Example
{
	//[CustomPropertyDrawer(typeof(EventAction<>), true)]
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
			EditorGUI.PropertyField(position, property, label, true);

			if(property.objectReferenceValue != null)
			{
				var foldOut = position;
				foldOut.height = 18;
				property.isExpanded = EditorGUI.Foldout(foldOut, property.isExpanded, "", true);
				EditorGUI.BeginProperty(position, label, property);

				if (property.isExpanded)
				{
					
				}

				EditorGUI.EndProperty();
			} else {

			}

			
		}
	}
}