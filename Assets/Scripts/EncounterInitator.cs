using System.Collections.Generic;
using UnityEngine;

public class EncounterInitiator : MonoBehaviour
{
	[SerializeField]
	private List<EnemyBase> _spawnPool = new List<EnemyBase>();

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject go = col.gameObject;

		EncounterHandler handler = go.GetComponent<EncounterHandler>();
		if (handler == null) return;

		handler.HandleEncounter(_spawnPool[0]);
	}
}
