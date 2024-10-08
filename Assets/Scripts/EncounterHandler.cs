using System;
using UnityEngine;

public class EncounterHandler : MonoBehaviour
{
	public Action<EncounterHandler> OnEncounter;

	public void HandleEncounter()
	{
		OnEncounter?.Invoke(this);
	}
}
