using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Maz.Unity.EventFramework 
{
	public static class ScriptableEventConstants
	{
		public const string MenuName = "Event";
	}

	public abstract class ScriptableEvent<T> : ScriptableObject
	{
		[ReadOnly, SerializeField]
		private T value;
		
		public T Value
		{
			get => value;
			set
			{
				if(!this.value.Equals(value))
				{
					this.value = value;
					OnChangedValue?.Invoke(this.value);
				}
			}
		}

		public UnityEvent<T> OnChangedValue;

	}
}

