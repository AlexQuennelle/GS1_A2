using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
	[SerializeField]
	private Transform _corner1;
	[SerializeField]
	private Transform _corner2;

	[SerializeField]
	private GameObject _orb;

	[SerializeField]
	private float _spawnDelay = 2.0f;
	private float _timerEnd;

	private bool _enabled = false;

	public float SpawnDelay { get { return _spawnDelay; } set { _spawnDelay = value; } }

	public void EnableSpawning()
	{
		_timerEnd = Time.time + _spawnDelay;
		_enabled = true;
	}
	private void Update()
	{
		if (Time.time >= _timerEnd && _enabled)
		{
			_timerEnd = Time.time + _spawnDelay;

			Vector3 pos = new Vector3
				(
					Mathf.Lerp(_corner1.position.x, _corner2.position.x, Random.value),
					Mathf.Lerp(_corner1.position.y, _corner2.position.y, Random.value),
					0.0f
				);
			GameObject go = GameObject.Instantiate(_orb, pos, Quaternion.identity);
		}
	}
}
