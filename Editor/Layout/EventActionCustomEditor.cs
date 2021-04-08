using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace Maz.Unity.EventFramework
{

    [CustomEditor(typeof(EventAction<>), true)]
    public class EventActionGenericCustomEditor : UnityEditor.Editor
    {
		public override VisualElement CreateInspectorGUI()
		{
            IDebugEventAction debugEventAction = (IDebugEventAction)target;
            VisualElement customInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Event Framework/Editor/Layout/EventActionGenericCustomEditor.uxml");
            visualTree.CloneTree(customInspector);

            Button raiseBtn = customInspector.Query<Button>("RaiseBtn").First();
            raiseBtn.clicked += () => debugEventAction.Raise();
            raiseBtn.SetEnabled((Application.isPlaying || EventFrameworkSettings.AllowRaiseInEditMode));

            return customInspector; 
        }

		private void callback(SerializedPropertyChangeEvent evt)
		{
            Debug.Log("Ok");
		}
	}

    [CustomEditor(typeof(EventAction), true)]
    public class EventActionCustomEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            EventAction eventAction = (EventAction)target;

            VisualElement customInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Event Framework/Editor/Layout/EventActionCustomEditor.uxml");
            visualTree.CloneTree(customInspector);

            Button raiseBtn = customInspector.Query<Button>("RaiseBtn").First();
            raiseBtn.clicked += () => eventAction.Raise();
            raiseBtn.SetEnabled((Application.isPlaying || EventFrameworkSettings.AllowRaiseInEditMode));

            return customInspector;
        }

    }
}