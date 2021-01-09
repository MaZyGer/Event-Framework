using Maz.Unity.Events;
using Signals;
using UnityEngine;

namespace Maz.Unity.EventFramework.Example
{
	public class Player : MonoBehaviour
    {
		[NaughtyAttributes.Expandable]
		public PlayerData playerData;

		public EventValue<int> Health;
		public EventActionValue<int> Mana;


		public int Damage;

		private void Start()
		{
			Health.Value = playerData.MaxHealth;

			Health.Raise();
			Mana.Raise();
		}

		private void Update()
		{
			Damage = playerData.Damage;
		}



		[NaughtyAttributes.Button("-10 HP", enabledMode: NaughtyAttributes.EButtonEnableMode.Playmode)]
		public void DecreaseHP()
        {
            Health.Value -= 10;
		}

		[NaughtyAttributes.Button("-10 MP", enabledMode: NaughtyAttributes.EButtonEnableMode.Playmode)]
		public void DecreaseMP()
		{
			Mana.Value -= 10;
		}

		[Signal]
		float HealAmount = 0f;

		void Heal(int amount)
		{
			Debug.Log("Heal " + amount);
			Health.Value += amount;
		}


	}
}