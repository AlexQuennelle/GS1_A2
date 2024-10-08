using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _player;
	[SerializeField]
	private Transform _combatArena;

	[SerializeField]
	private TrackObject _cameraTracker;

	private GameState _gameState = GameState.Exploration;

	private void OnEnable()
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
}

public enum GameState
{
	Exploration,
	Combat
}
