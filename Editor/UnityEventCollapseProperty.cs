using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{

	[CustomPropertyDrawer(typeof(UnityEventCollapseAttribute), true)]
    public class UnityEventCollapseProperty : UnityEventDrawer
    {
        GUIContent m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
        GUIContent m_IconToolbarPlus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Plus"));

		GUIStyle header = new GUIStyle(GUI.skin.FindStyle("RL Header"));
		GUIStyle styleLabel = new GUIStyle(GUI.skin.FindStyle("label"));

		bool isUnityEvent = false;

		float controlHeight = 0;
		const float ExtandedHeight = 40;
		const float MessageBoxHeight = 30;

		public override bool CanCacheInspectorGUI(SerializedProperty property)
		{
			return base.CanCacheInspectorGUI(property);
		}

		public override float GetPropertyHeight(SerializedProperty property,GUIContent label)
		{
			if (isUnityEvent)
			{
				if (property.isExpanded)
				{
					return controlHeight = base.GetPropertyHeight(property, label);
				}
				else
				{
					return controlHeight = 22f;
				}
			} else {
				return controlHeight = EditorGUI.GetPropertyHeight(property, label) + ExtandedHeight;
			}
				
			
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			System.Type parentType = property.serializedObject.targetObject.GetType();
			System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);

			if(fi != null)
			{
				isUnityEvent = fi.FieldType.IsSubclassOf(typeof(UnityEventBase));
			} else {
				isUnityEvent = false;
			}

			if (isUnityEvent) 
			{
				// Unity Event has complicated in build PropertyDrawer
				DrawUnityEvent(this, position, property, label);
			} else {
				Draw(position, property, label);
				
			}
		}



		bool Draw(Rect position, SerializedProperty property, GUIContent label)
		{
			var positionOrginal = position;


			position.height = positionOrginal.height - ExtandedHeight + MessageBoxHeight / 2f;
			EditorGUI.HelpBox(position, typeof(UnityEventCollapseAttribute).Name + " works only with UnityEvents", MessageType.Warning);

			position.y = positionOrginal.y + ExtandedHeight;
			position.height = positionOrginal.height - ExtandedHeight;
			EditorGUI.PropertyField(position, property, label);

			return property.isExpanded;
		}

		bool DrawUnityEvent(UnityEventDrawer drawer, Rect position, SerializedProperty property, GUIContent label)
		{
			styleLabel.alignment = TextAnchor.MiddleLeft;

			styleLabel.fixedHeight = header.fixedHeight;

			System.Type[] typeArguments = default;
			if (property.propertyType == SerializedPropertyType.Generic)
			{
				var type = property.GetValue().GetType();
				if (type.IsGenericType)
				{
					typeArguments = type.GetGenericArguments();

				}
				//var genericType = type.GetGenericTypeDefinition();
			}
			

			if (property.isExpanded)
			{
				base.OnGUI(position, property, label);
				position.xMin += 6;
				//position.y += 1;
			}
			else
			{
				if (Event.current.type == EventType.Repaint)
				{
					header.Draw(position, false, false, false, false);
					position.xMin += 6;
					//position.y += 1;

					EditorGUI.LabelField(position, ReflectionHelper.GenerateGenericTypesAsString(property), styleLabel);
				}

			}

			GUIContent buttonContent = property.isExpanded ? m_IconToolbarMinus : m_IconToolbarPlus;
			Vector2 buttonIcon = GUIStyle.none.CalcSize(buttonContent);

			//Rect callbackRect = GUILayoutUtility.GetLastRect();
			Rect callbackRect = position;
			Rect buttonIconPos = new Rect(callbackRect.xMax - buttonIcon.x - 8, callbackRect.y + 1, buttonIcon.x, buttonIcon.y);
			if (GUI.Button(buttonIconPos, buttonContent, GUIStyle.none))
			{
				property.isExpanded = !property.isExpanded;
			}

			return property.isExpanded;
		}
	}
}