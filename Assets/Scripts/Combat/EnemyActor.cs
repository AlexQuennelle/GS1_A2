using UnityEngine;

/**************************************************************************************************/
/* This class represents the enemy brain during combat                                            */
/* Behaviour is dictated by the enemy base opject passed in to the Initialize method              */
/**************************************************************************************************/
public class EnemyActor : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _sr;

	[SerializeField]
	private OrbSpawner _orbSpawner;
	[SerializeField]
	private ProjectileSpawner _projectileSpawner;
	[SerializeField]
	private Animator _animator;

	private bool _ready = false;

	private void Update()
	{
		if (!_ready) return;
	}

	///<summary>
	///Populates stats and behaviour based on the inputed enemy base object
	///</summary>
	public void Initialize(EnemyBase baseEnemy)
	{
		_sr.sprite = baseEnemy.BaseSprite;
		if (TryGetComponent<Health>(out Health health)) health.HP = baseEnemy.HP;

		_orbSpawner.EnableSpawning();
		//_projectileSpawner.EnableSpawning();

		_animator.runtimeAnimatorController = baseEnemy.Animator;
	}

	///<summary>
	///Cleans up and disables enemy behaviour
	///</summary>
	public void DeInit()
	{
		_orbSpawner.DisableSpawning();
		//_projectileSpawner.DisableSpawning();
		
		_animator.runtimeAnimatorController = null;
	}
}
