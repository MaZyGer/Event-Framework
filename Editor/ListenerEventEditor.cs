using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Maz.Unity.EventFramework
{

    //[CustomEditor(typeof(EventListener))]
    public class ListenerEventEditor : Editor
    {
		GUIContent m_IconToolbarMinus;
		GUIContent m_IconToolbarPlus;

		static bool isCollapsed = true;

		EventListener eventListenerTarget;

		string[] evenTypes = new string[] { "OnRaiseInt", "OnRaiseFloat", "OnRaiseDouble", "OnRaiseString" };

		EvenTypes selectedEvenType;

		private void OnEnable()
		{
			eventListenerTarget = target as EventListener;

			m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
			m_IconToolbarPlus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Plus"));
		}

		public override void OnInspectorGUI()
		{
			//base.OnInspectorGUI();
			
			var currentSelectionProperty = serializedObject.FindProperty("ListenType");
			var eventActionProperty = serializedObject.FindProperty("EventAction");

			EditorGUILayout.HelpBox("Note: All events will get invoked", MessageType.Info);
			EditorGUILayout.PropertyField(currentSelectionProperty);
			EditorGUILayout.PropertyField(eventActionProperty);
			EditorGUILayout.Space();

			selectedEvenType = eventListenerTarget.ListenType;
			switch (selectedEvenType)
			{
				case EvenTypes.IntEvent:
					var onRaisIntProperty = serializedObject.FindProperty("OnRaiseIntEvent");
					isCollapsed = DrawCustomFoldout(onRaisIntProperty, isCollapsed);
					break;
				case EvenTypes.FloatEvent:
					var onRaiseFloatProperty = serializedObject.FindProperty("OnRaiseFloatEvent");
					isCollapsed = DrawCustomFoldout(onRaiseFloatProperty, isCollapsed);
					break;
				case EvenTypes.DoubleEvent:
					var onRaiseDoubleProperty = serializedObject.FindProperty("OnRaiseDoubleEvent");
					isCollapsed = DrawCustomFoldout(onRaiseDoubleProperty, isCollapsed);
					break;
				case EvenTypes.StringEvent:
					var onRaiseStringProperty = serializedObject.FindProperty("OnRaiseStringEvent");
					isCollapsed = DrawCustomFoldout(onRaiseStringProperty, isCollapsed);
					break;
				case EvenTypes.GameEvent:
					var onRaiseGameEventProperty = serializedObject.FindProperty("OnRaiseGameEvent");
					isCollapsed = DrawCustomFoldout(onRaiseGameEventProperty, isCollapsed);
					break;
				default:
					break;
			}

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
