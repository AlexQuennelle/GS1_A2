using UnityEngine;

public class EncounterInitiator : MonoBehaviour
{
	[SerializeField]
	private EnemyBase _enemy;

	private EncounterHandler _handler;

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject go = col.gameObject;
		_handler = go.GetComponent<EncounterHandler>();

		AttemptEncounter();
	}
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent<EncounterHandler>(out EncounterHandler eh))
		{
			_handler = null;
		}
	}

	private void AttemptEncounter()
	{
		if (_handler == null)
		{
			Debug.LogWarning($"WARNING! No EncounterHandler found on {_handler.gameObject.name}.");
			return;
		}

		_handler.HandleEncounter(_enemy);

		GameObject.Destroy(this.gameObject);
	}
}
