using System.Collections.Generic;
using UnityEngine;

public class EncounterInitiator : MonoBehaviour
{
	[SerializeField]
	private List<EnemyBase> _spawnPool = new List<EnemyBase>();
	[SerializeField, Range(0, 1)]
	private float _encounterChance;

	private EncounterHandler _handler;

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject go = col.gameObject;
		_handler = go.GetComponent<EncounterHandler>();

		AttemptEncounter();
	}
	private void OnTiggerExit2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent<EncounterHandler>(out EncounterHandler eh)) _handler = null;
	}

	private void AttemptEncounter()
	{
		if (_handler == null)
		{
			Debug.LogWarning($"WARNING! No EncounterHandler found on {_handler.gameObject.name}.");
			return;
		}

		if (Random.value <= _encounterChance)
		{
			_handler.HandleEncounter(_spawnPool[Random.Range(0, _spawnPool.Count)]);
		}
	}
}
