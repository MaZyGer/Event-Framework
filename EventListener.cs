using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.EventFramework
{
	public abstract class EventListener<T> : MonoBehaviour
	{
		public EventAction<T> Event;

		public UnityEvent<T> Response = default;

		private void OnEnable()
		{
			Event.RegisterListener(this);
		}

		private void OnDisable()
		{
			Event.UnregisterListener(this);
		}

		public void OnEventRaised(T value)
		{
			Response?.Invoke(value);
		}


	}

	public class EventListener : MonoBehaviour
	{
		public EventAction Event;

		public EvenTypes ListenType;

		public UnityEvent ResponseGameEvent;
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
			ResponseGameEvent.Invoke();
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