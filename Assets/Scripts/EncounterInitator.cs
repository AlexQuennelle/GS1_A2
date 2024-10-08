using UnityEngine;

public class EncounterInitiator : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject go = col.gameObject;

		EncounterHandler handler = go.GetComponent<EncounterHandler>();
		if (handler == null) return;

		handler.HandleEncounter();
	}
}
