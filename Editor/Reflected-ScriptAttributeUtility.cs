using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Reflection;

namespace Maz.Unity.EventFramework
{

	public static class ScriptAttributeUtility
	{
		public static class DrawerKeySet
		{
			static Type s_Type;


			public static Type Type
			{
				get
				{
					return s_Type ?? (s_Type = ScriptAttributeUtility.Type.GetNestedType("DrawerKeySet", BindingFlags.NonPublic));
				}
			}


			public static object Create<TType, TDrawer>()
				where TType : class
				where TDrawer : PropertyDrawer
			{
				object instance = Activator.CreateInstance(Type);

				Type.GetField("drawer").SetValue(instance, typeof(TDrawer));
				Type.GetField("type").SetValue(instance, typeof(TType));

				return instance;
			}
		}


		static Type s_Type;
		static FieldInfo s_DrawerTypeForTypeField;
		static object s_DrawerTypeForType;


		public static Type Type
		{
			get
			{
				return s_Type ?? (s_Type = typeof(EditorWindow).Assembly.
					GetType("UnityEditor.ScriptAttributeUtility", throwOnError: true, ignoreCase: false));
			}
		}


		public static FieldInfo DrawerTypeForTypeField
		{
			get
			{
				return s_DrawerTypeForTypeField ?? (s_DrawerTypeForTypeField = Type.
					GetField("s_DrawerTypeForType", BindingFlags.Static | BindingFlags.NonPublic));
			}
		}


		public static object DrawerTypeForType
		{
			get
			{
				if ((s_DrawerTypeForType ?? (s_DrawerTypeForType = DrawerTypeForTypeField.GetValue(null))) != null)
				{
					return s_DrawerTypeForType;
				}

				Type.GetMethod("BuildDrawerTypeForTypeDictionary", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);

				return s_DrawerTypeForType = DrawerTypeForTypeField.GetValue(null);
			}
		}
	} 
}