using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.Singleton;

public class GameManager : MonoSingleton<GameManager>
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

	private bool combatTrigger = true;
	private int finishTrigger = 9;

	private void OnEnable()
	{
		SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneAt(0));
		if (_player.TryGetComponent<EncounterHandler>(out _encounterHandler)) _encounterHandler.OnEncounter += PlayerEncounter;
		if (_enemyActor.TryGetComponent<Health>(out Health enemyHealth)) enemyHealth.OnDeath += Victory;
		if (_combatPlayer.TryGetComponent<Health>(out Health playerHealth)) playerHealth.OnDeath += Defeat;
	}
	private void OnDisable()
	{
		if (_encounterHandler != null) _encounterHandler.OnEncounter -= PlayerEncounter;
		if (_enemyActor.TryGetComponent<Health>(out Health enemyHealth)) enemyHealth.OnDeath -= Victory;
		if (_combatPlayer.TryGetComponent<Health>(out Health playerHealth)) playerHealth.OnDeath -= Defeat;
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
		UIManager.Instance.ShowPanel<CombatPanel>(PanelBase.PanelShowLayer.Front, PanelBase.Ani.None, combatPanel =>
		{
			combatPanel.GetHealthData(_combatPlayer.GetComponent<Health>(), _enemyActor.GetComponent<Health>());
		});
		if (combatTrigger)
		{
			DialogueManager.Instance.BeginDialogue(8);
			combatTrigger = false;
		}	   
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
		UIManager.Instance.HidePanel<CombatPanel>(PanelBase.Ani.None);
		if (finishTrigger == 9 || finishTrigger == 12 || finishTrigger == 16)
		{
			DialogueManager.Instance.BeginDialogue(finishTrigger);
			if (finishTrigger == 12)
			{
				finishTrigger = 16;
			} else {
				finishTrigger = 12;
			}
		}
	}

	public void Pause(bool pause)
	{
		if (_gameState == GameState.Exploration)
		{
			_player.GetComponent<PlayerMover>().MoveSpeed = pause ? 0.0f : 2.5f;
		}
		else if (_gameState == GameState.Combat)
		{
			_combatPlayer.GetComponent<PlayerMover>().enabled = !pause;
			_combatPlayer.GetComponent<Health>().IsDialogue = pause;
			_enemyActor.GetComponent<Health>().IsDialogue = pause;
		}
	}

	//put encounter transition here
	private void PlayerEncounter(EncounterHandler handler, EnemyBase baseEnemy)
	{
		EnterCombat(baseEnemy);
	}

	#region temp methods
	//put victory screen here
	private void Victory(Health health)
	{
		ExitCombat();
	}
	//put game over screen here
	private void Defeat(Health health)
	{
        UIManager.Instance.HidePanel<CombatPanel>(PanelBase.Ani.None);	
        SceneManager.LoadScene(0);
    }
	#endregion
}

public enum GameState
{
	Exploration,
	Combat
}
