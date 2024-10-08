using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _player;
	[SerializeField]
	private Transform _combatArena;

	private GameState _gameState = GameState.Exploration;

	private void Update()
	{
		switch (_gameState)
		{
			default:
			case (GameState.Exploration):
				break;
			case (GameState.Combat):
				break;
		}
	}
}

public enum GameState
{
	Exploration,
	Combat
}
