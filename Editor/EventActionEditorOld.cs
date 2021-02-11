using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Maz.Unity.EventFramework
{

    //[CustomEditor(typeof(EventAction))]
    public class EventActionEditor : Editor
    {
		GUIContent m_IconToolbarMinus;
		GUIContent m_IconToolbarPlus;

		//EventAction eventListenerTarget;

		SerializedProperty isIntEventCollapsedProperty;
		SerializedProperty isFloatEventCollapsedProperty;
		SerializedProperty isDoubleEventCollapsedProperty;
		SerializedProperty isStringEventCollapsedProperty;
		SerializedProperty isGameEventCollapsedProperty;

		private void OnEnable()
		{
			//eventListenerTarget = target as EventAction;

			m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
			m_IconToolbarPlus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Plus"));

			isIntEventCollapsedProperty = serializedObject.FindProperty("isIntEventCollapsed");
			isFloatEventCollapsedProperty = serializedObject.FindProperty("isFloatEventCollapsed");
			isDoubleEventCollapsedProperty = serializedObject.FindProperty("isDoubleEventCollapsed");
			isStringEventCollapsedProperty = serializedObject.FindProperty("isStringEventCollapsed");
			isGameEventCollapsedProperty = serializedObject.FindProperty("isGameEventCollapsed");
		}

		public override void OnInspectorGUI()
		{

			var IntEventProperty = serializedObject.FindProperty("IntEvent");
			isIntEventCollapsedProperty.boolValue = DrawCustomFoldout(IntEventProperty, isIntEventCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			var FloatEventProperty = serializedObject.FindProperty("FloatEvent");
			isFloatEventCollapsedProperty.boolValue = DrawCustomFoldout(FloatEventProperty, isFloatEventCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			var DoubleEventProperty = serializedObject.FindProperty("DoubleEvent");
			isDoubleEventCollapsedProperty.boolValue = DrawCustomFoldout(DoubleEventProperty, isDoubleEventCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			var StringEventProperty = serializedObject.FindProperty("StringEvent");
			isStringEventCollapsedProperty.boolValue = DrawCustomFoldout(StringEventProperty, isStringEventCollapsedProperty.boolValue);

			EditorGUILayout.Space();

			var GameEventProperty = serializedObject.FindProperty("GameEvent");
			isGameEventCollapsedProperty.boolValue = DrawCustomFoldout(GameEventProperty, isGameEventCollapsedProperty.boolValue);

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
				var type = property.serializedObject.targetObject.GetFieldValue(property.propertyPath).GetType();
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
