using UnityEngine;

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
	private TrackObject _cameraTracker;

	[SerializeField]
	private GameState _gameState = GameState.Exploration;

	private void OnEnable()
	{
		_encounterHandler = _player.GetComponent<EncounterHandler>();
		if (_encounterHandler != null)
		{
			_encounterHandler.OnEncounter += PlayerEncounter;
		}
	}
	private void OnDisable()
	{
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

	public void EnterCombat()
	{
		if (_gameState != GameState.Exploration) return;
		_gameState = GameState.Combat;
		_player.SetActive(false);
		_combatPlayer.SetActive(true);
	}
	public void ExitCombat()
	{
		if (_gameState != GameState.Combat) return;
		_gameState = GameState.Exploration;
		_player.SetActive(true);
		_combatPlayer.SetActive(false);
	}

	private void PlayerEncounter(EncounterHandler handler)
	{
		EnterCombat();
	}
}

public enum GameState
{
	Exploration,
	Combat
}
