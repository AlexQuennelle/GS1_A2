using UnityEngine;

public class EnemyActor : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _sr;

	[SerializeField]
	private OrbSpawner _orbSpawner;
	[SerializeField]
	private OrbSpawner _projectileSpawner;

	private bool _ready = false;

	private void Update()
	{
		if (!_ready) return;
	}

	public void Initialize(EnemyBase baseEnemy)
	{
		_sr.sprite = baseEnemy.BaseSprite;
		if (TryGetComponent<Health>(out Health health)) health.HP = baseEnemy.HP;

		_orbSpawner.EnableSpawning();
		_projectileSpawner.EnableSpawning();
	}

	public void DeInit()
	{
		_orbSpawner.DisableSpawning();
		_projectileSpawner.DisableSpawning();
	}
}
