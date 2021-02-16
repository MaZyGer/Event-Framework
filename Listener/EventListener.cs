using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
	public sealed class EventListener : MonoBehaviour
	{
		public EventAction Event;

		public UnityEvent Response = default;

		void OnEnable()
		{
			Event?.RegisterListener(this);
		}

		void OnDisable()
		{
			Event?.UnregisterListener(this);
		}

		public void OnEventRaised()
		{
			Response?.Invoke();
		}
	}

	public abstract class EventListener<T> : MonoBehaviour
	{
		public EventAction<T> Event;

		public UnityEvent<T> Response = default;

		private void OnEnable()
		{
			Event?.RegisterListener(this);
		}

		private void OnDisable()
		{
			Event?.UnregisterListener(this);
		}

		public void OnEventRaised(T value)
		{
			Response?.Invoke(value);
		}
	}

}