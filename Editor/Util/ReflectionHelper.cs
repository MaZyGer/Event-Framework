using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Maz.Unity.EventFramework
{
    public static class ReflectionHelper
    {
		public static string GenerateGenericTypesAsString(SerializedProperty property)
		{
			System.Type[] typeArguments = default;
			if (property.propertyType == SerializedPropertyType.Generic)
			{
				var type = property.serializedObject.targetObject.GetFieldValue(property.propertyPath).GetType();
				if (type.IsGenericType)
				{
					typeArguments = type.GetGenericArguments();
					string result = property.displayName + " (";
					for (int i = 0; i < typeArguments.Length; i++)
					{
						var t = typeArguments[i];
						result += $"{t.Name}";
						if (i < typeArguments.Length - 1)
						{
							result += ", ";
						}
					}
					result += ")";
					return result;
				}
			}

			return $"{property.displayName} ()";
		}


		public static object GetFieldValue(this object obj, string varName)
		{
			System.Type parentType = obj.GetType();
			FieldInfo fi = parentType.GetField(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
			return fi.GetValue(obj);
		}

		public static object GetPropertyValue(this object obj, string varName)
		{
			System.Type parentType = obj.GetType();
			PropertyInfo fi = parentType.GetProperty(varName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			return fi.GetValue(obj);
		}
	}
}
