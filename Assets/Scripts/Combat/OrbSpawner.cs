using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
	[SerializeField]
	private Transform _corner1;
	[SerializeField]
	private Transform _corner2;

	[SerializeField]
	private GameObject _damageOrb;
	[SerializeField]
	private GameObject _healOrb;

	[SerializeField]
	private float _spawnDelay = 2.0f;
	private float _timerEnd;

	private List<GameObject> _orbs = new List<GameObject>();

	private bool _enabled = false;

	public float SpawnDelay { get { return _spawnDelay; } set { _spawnDelay = value; } }

	public void EnableSpawning()
	{
		_timerEnd = Time.time + _spawnDelay;
		_enabled = true;
	}
	public void DisableSpawning()
	{
		_enabled = false;
	}

	private void Update()
	{
		if (Time.time >= _timerEnd && _enabled)
		{
			_timerEnd = Time.time + _spawnDelay;

			GameObject dgo = GameObject.Instantiate(_damageOrb, RandomPos(), Quaternion.identity);
			GameObject hgo = GameObject.Instantiate(_healOrb, RandomPos(), Quaternion.identity);
			if (dgo.TryGetComponent<DamageTarget>(out DamageTarget dt))
			{
				dt.Target = GetComponent<Health>();
				dt.OnDamage += OnDamageOrbCollected;
			}
			if (hgo.TryGetComponent<Heal>(out Heal heal))
			{
				heal.OnHeal += OnHealOrbCollected;
			}

			_orbs.Add(dgo);
			_orbs.Add(hgo);
		}
	}

	private void OnHealOrbCollected(Heal heal)
	{
		CleanOrbs();
	}
	private void OnDamageOrbCollected(DamageTarget target)
	{
		CleanOrbs();
	}
	private void CleanOrbs()
	{
		GameObject[] orbArray = _orbs.ToArray();
		foreach (GameObject orb in orbArray)
		{
			_orbs.Remove(orb);
			if (orb.TryGetComponent<DamageTarget>(out DamageTarget dt)) dt.OnDamage -= OnDamageOrbCollected;
			if (orb.TryGetComponent<Heal>(out Heal heal)) heal.OnHeal -= OnHealOrbCollected;
			GameObject.Destroy(orb);
		}
	}

	private Vector3 RandomPos() => new Vector3(Mathf.Lerp(_corner1.position.x, _corner2.position.x, Random.value), Mathf.Lerp(_corner1.position.y, _corner2.position.y, Random.value), 0.0f);
}
