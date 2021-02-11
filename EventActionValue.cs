using UnityEngine;

namespace Maz.Unity.EventFramework
{
	public abstract class EventActionValue : IRaiser
	{
		public abstract void Raise();
	}

	[System.Serializable]
	public class EventActionValue<T> : EventActionValue
	{
		public EventAction<T> EventAction;

		[SerializeField]
		T value;
		public T Value
		{
			get => value;
			set
			{
				if (this.value == null || !this.value.Equals(value))
				{
					this.value = value;
					changed = true;
					Raise();
				}
			}
		}

		bool changed;
		public bool Changed
		{
			get
			{
				bool retValue = changed;

				if (changed)
					changed = false;

				return retValue;
			}

		}

		public EventActionValue(T value)
		{
			this.value = value;
		}

		public override void Raise()
		{
			EventAction?.Raise(value);
		}

	}
}