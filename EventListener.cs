using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
	[System.Serializable]
	public class EventListener<T>
	{
		public UnityEvent<T> ResponseInt = default;

		public void OnEventRaised(T value)
		{
			ResponseInt?.Invoke(value);
		}

	}

	public class EventListener : MonoBehaviour
	{
		public EventAction Event;

		public EvenTypes ListenType;

		public UnityEvent Response;
		public UnityEvent<int> ResponseInt = default;
		public UnityEvent<float> ResponseFloat = default;
		public UnityEvent<double> ResponseDouble = default;
		public UnityEvent<string> ResponseString = default;

		private void OnEnable()
		{
			Event.RegisterListener(this);
		}

		private void OnDisable()
		{
			Event.UnregisterListener(this);
		}

		public void OnEventRaised()
		{
			Response.Invoke();
		}

		public void OnEventRaised(int value)
		{
			ResponseInt.Invoke(value);
		}

		public void OnEventRaised(float value)
		{
			ResponseFloat.Invoke(value);
		}

		public void OnEventRaised(double value)
		{
			ResponseDouble.Invoke(value);
		}

		public void OnEventRaised(string value)
		{
			ResponseString.Invoke(value);
		}
	}
}