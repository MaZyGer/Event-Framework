using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Maz.Unity.EventFramework
{

    [CustomEditor(typeof(ConvertToEvent))]
    public class ToStringConverterListenerEditor : Editor
    {
		GUIContent m_IconToolbarMinus;
		GUIContent m_IconToolbarPlus;

		SerializedProperty isOnRaisIntCollapsedProperty;
		SerializedProperty isOnRaiseFloatCollapsedProperty;
		SerializedProperty isOnRaiseDoubleCollapsedProperty;
		SerializedProperty isOnRaiseStringCollapsedProperty;

		ConvertToEvent eventListenerTarget;

		private void OnEnable()
		{ 
			eventListenerTarget = target as ConvertToEvent;

			m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
			m_IconToolbarPlus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Plus"));

			isOnRaisIntCollapsedProperty = serializedObject.FindProperty("isOnRaisIntCollapsed");
			isOnRaiseFloatCollapsedProperty = serializedObject.FindProperty("isOnRaiseFloatCollapsed");
			isOnRaiseDoubleCollapsedProperty = serializedObject.FindProperty("isOnRaiseDoubleCollapsed");
			isOnRaiseStringCollapsedProperty = serializedObject.FindProperty("isOnRaiseStringCollapsed");
		}

		private void OnDisable()
		{

		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.HelpBox("Drag this to EventListener and choose the function. Use these events to forward value.", MessageType.None);
			EditorGUILayout.Space();

			var onRaisIntProperty = serializedObject.FindProperty("OnIntEvent");
			isOnRaisIntCollapsedProperty.boolValue = DrawCustomFoldout(onRaisIntProperty, isOnRaisIntCollapsedProperty.boolValue);

			EditorGUILayout.Space(); 

			var onRaiseFloatProperty = serializedObject.FindProperty("OnFloatEvent");
			isOnRaiseFloatCollapsedProperty.boolValue = DrawCustomFoldout(onRaiseFloatProperty, isOnRaiseFloatCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			var onRaiseDoubleProperty = serializedObject.FindProperty("OnDoubleEvent");
			isOnRaiseDoubleCollapsedProperty.boolValue = DrawCustomFoldout(onRaiseDoubleProperty, isOnRaiseDoubleCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			var onRaiseStringProperty = serializedObject.FindProperty("OnStringEvent");
			isOnRaiseStringCollapsedProperty.boolValue = DrawCustomFoldout(onRaiseStringProperty, isOnRaiseStringCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			serializedObject.ApplyModifiedProperties();
		}

		bool DrawCustomFoldout(SerializedProperty property, bool show)
		{

			GUIStyle originBoxStyle = GUI.skin.box;
			originBoxStyle.alignment = TextAnchor.MiddleLeft;
			GUIStyle style = GUI.skin.FindStyle("RL Header");

			System.Type[] typeArguments = default;
			if (property.propertyType == SerializedPropertyType.Generic)
			{
				var type = property.GetValue().GetType();
				if(type.IsGenericType)
				{
					typeArguments = type.GetGenericArguments();

				}
				//var genericType = type.GetGenericTypeDefinition();
			}
			GUIContent c = show ? m_IconToolbarMinus : m_IconToolbarPlus;

			if(show)
			{
				EditorGUILayout.PropertyField(property);

			} else {
				GUILayout.BeginHorizontal(style);
				var displayName = property.displayName;
				GUILayout.Label(ReflectionHelper.GenerateGenericTypesAsString(property), GUILayout.ExpandWidth(true));
				GUILayout.EndHorizontal();
			}

			Vector2 buttonIcon = GUIStyle.none.CalcSize(c);

			Rect callbackRect = GUILayoutUtility.GetLastRect();
			Rect buttonIconPos = new Rect(callbackRect.xMax - buttonIcon.x - 8, callbackRect.y + 1, buttonIcon.x, buttonIcon.y);
			if (GUI.Button(buttonIconPos, c, GUIStyle.none))
			{
				show = !show;
			}

			return show;
		}
	}
}
