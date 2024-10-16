using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField, Header("Players")]
	private GameObject _player;
	private EncounterHandler _encounterHandler;
	[SerializeField]
	private GameObject _combatPlayer;

	[SerializeField, Header(" ")]
	private Transform _combatArena;
	[SerializeField]
	private EnemyActor _enemyActor;

	[SerializeField]
	private TrackObject _cameraTracker;

	[SerializeField]
	private GameState _gameState = GameState.Exploration;

	private void OnEnable()
	{
		//_encounterHandler = _player.GetComponent<EncounterHandler>();
		//if (_encounterHandler != null)
		//{
		//	_encounterHandler.OnEncounter += PlayerEncounter;
		//}
		if (_player.TryGetComponent<EncounterHandler>(out _encounterHandler))
		{
			_encounterHandler.OnEncounter += PlayerEncounter;
		}

		//temp
		_enemyActor.GetComponent<Health>().OnDeath += Victory;
		_combatPlayer.GetComponent<Health>().OnDeath += Defeat;
	}
	private void OnDisable()
	{
		if (_encounterHandler != null)
		{
			_encounterHandler.OnEncounter -= PlayerEncounter;
		}
	}

	private void Update()
	{
		switch (_gameState)
		{
			default:
			case (GameState.Exploration):
				_cameraTracker.Target = _player.transform;
				break;
			case (GameState.Combat):
				_cameraTracker.Target = _combatArena;
				break;
		}
	}

	public void EnterCombat(EnemyBase baseEnemy)
	{
		if (_gameState != GameState.Exploration) return;
		_gameState = GameState.Combat;
		_player.SetActive(false);
		_combatPlayer.SetActive(true);
		_enemyActor.Initialize(baseEnemy);
	}
	public void ExitCombat()
	{
		if (_gameState != GameState.Combat) return;
		_gameState = GameState.Exploration;

		_enemyActor.DeInit();

		_combatPlayer.SetActive(false);
		_player.SetActive(true);
	}

	private void PlayerEncounter(EncounterHandler handler, EnemyBase baseEnemy)
	{
		EnterCombat(baseEnemy);
	}

#region temp methods
	private void Victory(Health health)
	{
		ExitCombat();
	}
	private void Defeat(Health health)
	{
		SceneManager.LoadScene(0);
	}
#endregion
}

public enum GameState
{
	Exploration,
	Combat
}
