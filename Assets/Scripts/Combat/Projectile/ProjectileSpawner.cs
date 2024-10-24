using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField]
	private float _radius = 5.0f;
	[SerializeField]
	private GameObject _projectile;
	private bool _enabled = false;

	private Vector2 _direction;
	private Vector2 _p1, _p2;

	[SerializeField]
	private Transform _player;

	[SerializeField]
	private float _spawnDelay = 1.0f;
	private float _timerEnd;

	public void Enable(ProjectilePatternBase pattern)
	{
		_projectile = pattern.Projectile;
		_direction = pattern.Directions[0];
		_spawnDelay = pattern.Delay;

		_timerEnd = Time.time + _spawnDelay;
		_enabled = true;
	}
	public void Disable()
	{
		_enabled = false;
	}

	private void Update()
	{
		if (Time.time >= _timerEnd && _enabled)
		{
			_timerEnd = Time.time + _spawnDelay;

			CalculatePoints();
			SpawnProjectiles();
		}
	}

	private void SpawnProjectiles()
	{
		GameObject go = GameObject.Instantiate(_projectile, Vector2.Lerp(_p1, _p2, Random.value), Quaternion.identity);
		go.transform.up = _direction.normalized;

		if (go.TryGetComponent<PointAt>(out PointAt tracker))
		{
			tracker.Target = _player;
		}
	}

	private void CalculatePoints()
	{
		float m = -(_direction.x / _direction.y);
		Vector2 d = new Vector2(1 / Mathf.Sqrt(1 + m), m / Mathf.Sqrt(1 + m)) * _radius;

		_p1 = (-_direction * _radius) - d + (Vector2)transform.position;
		_p2 = (-_direction * _radius) + d + (Vector2)transform.position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(_p1, 0.1f);
		Gizmos.DrawSphere(_p2, 0.1f);
	}
}
