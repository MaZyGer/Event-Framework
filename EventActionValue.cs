using UnityEngine;

namespace Maz.Unity.EventFramework
{
	public abstract class EventActionValueBase
	{
		[NaughtyAttributes.Expandable]
		public EventAction EventAction;
		public abstract void Raise();
	}

	[System.Serializable]
	public class EventActionValue<T> : EventActionValueBase
	{
		[SerializeField]
		T value;
		public T Value
		{
			get => value;
			set
			{
				if (!this.value.Equals(value))
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