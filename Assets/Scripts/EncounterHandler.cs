using System;
using UnityEngine;

public class EncounterHandler : MonoBehaviour
{
	public event Action<EncounterHandler, EnemyBase> OnEncounter;

	public void HandleEncounter(EnemyBase baseEnemy)
	{
		OnEncounter?.Invoke(this, baseEnemy);
	}
}
