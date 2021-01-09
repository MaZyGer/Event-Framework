using System.Collections;
using System.Collections.Generic;
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
				var type = GetValue(property).GetType();
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

		public static object GetValue(this SerializedProperty property)
		{
			System.Type parentType = property.serializedObject.targetObject.GetType();
			System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
			return fi.GetValue(property.serializedObject.targetObject);
		}
	}
}
