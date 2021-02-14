using System.Collections;
using System.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Maz.Unity.EventFramework.Example
{
	[CustomPropertyDrawer(typeof(EventActionValue), true)]
	public class EventActionValueEditor : PropertyDrawer
	{
		bool searchMode = false;
		int selectionIndex = 0;

		bool loaded = false;
		List<string> list = new List<string>();
		List<UnityEngine.Object> assets = new List<UnityEngine.Object>();

		float extendHeight = 0;
		const bool enableSearch = true;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}

			return EditorGUI.GetPropertyHeight(property, label) + 60 + extendHeight;
		}

		public override bool CanCacheInspectorGUI(SerializedProperty property)
		{
			return base.CanCacheInspectorGUI(property);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			extendHeight = 0;

			var genericType = GetGenericType(property);
			var generic = typeof(EventAction<>).MakeGenericType(genericType);

			if(!loaded)
			{
				loaded = true;

				if(enableSearch)
				{
					var filter = string.Format("t:{0}", typeof(EventActionBase));
					string[] guids = AssetDatabase.FindAssets(filter);
					for (int i = 0; i < guids.Length; i++)
					{
						string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
						var asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(EventActionBase));

						if (asset.GetType().IsSubclassOf(generic))
						{
							list.Add(asset.name);
							assets.Add(asset);
						}
					}
				}
			}


			position.height = 18;
			EditorGUI.PropertyField(position, property, label, false);
			if (property.isExpanded)
			{
				position.y += 18;
				position.width = position.width - 15f;
				position.x += 15f;

				float originalX = position.x;
				float originalW = position.width;

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

				if (enableSearch)
				{
					if (searchMode)
					{
						
						position.width = originalW * 0.5f;
						position.x = originalX;
						position.y += 20;
						extendHeight += 20;
						if(list.Count > 0)
						{
							selectionIndex = EditorGUI.Popup(position, selectionIndex, list.ToArray());

							position.x = originalX + originalW * 0.5f * 1f;
							position.width = originalW * 0.25f;
							if (GUI.Button(position, "Confirm"))
							{
								searchMode = false;

								if (assets.Count > 0)
									eventActionProp.objectReferenceValue = assets[selectionIndex];
							}
						} 
						else
						{
							EditorGUI.LabelField(position, $"No type of {genericType.Name} found");
						}



						position.width = originalW * 0.25f;
						position.x = originalX + originalW * 0.5f * 1.5f;
						if (GUI.Button(position, "Cancel"))
						{
							searchMode = false;
						}
						position.width = originalW;
					} 
					else 
					{
						position.y += 20;
						if (GUI.Button(position, "Select Event"))
						{
							searchMode = true;
						}
					}

				}



				position.width = originalW;
				position.x = originalX;

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
	}
}