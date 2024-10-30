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

		if (!col.CompareTag("Player"))
			return;
		if (gameObject.name == "TutorialSlime")
		{
			Debug.Log("TutorialSlime");
			GameManager.Instance.finishTrigger = 9;
		}
		else if (gameObject.name == "BlueSlime")
		{
			Debug.Log("BlueSlime");
			GameManager.Instance.finishTrigger = 12;
		}
		else if (gameObject.name == "FrogBoss")
			GameManager.Instance.finishTrigger = 16;
		else
		{
			Debug.Log("0000000000000000000");
            GameManager.Instance.finishTrigger = 0;
        }      
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
			return;
		}

		_handler.HandleEncounter(_enemy);

		GameObject.Destroy(this.gameObject);
	}
}
