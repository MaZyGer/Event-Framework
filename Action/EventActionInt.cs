using UnityEngine;

namespace Maz.Unity.EventFramework
{
	[CreateAssetMenu(menuName = ScriptableEventConstants.MenuName + "/" + nameof(EventActionInt))]
	public class EventActionInt : EventAction<int>
	{

	}

}

