using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField]
	private float _radius = 5.0f;
	[SerializeField]
	private GameObject _projectile;
	private bool _enabled = false;

	private List<ProjectilePattern> _patterns;

	private Vector2 _direction;
	private Vector2 _p1, _p2;

	[SerializeField]
	private Transform _player;

	[SerializeField]
	private float _spawnDelay = 1.0f;
	private float _timerEnd;

	public void Enable(ProjectilePatternBase[] patternBases)
	{
		_patterns = new List<ProjectilePattern>();
		foreach (ProjectilePatternBase patternBase in patternBases)
		{
			_patterns.Add(new ProjectilePattern(patternBase.Projectile, patternBase.Directions, patternBase.Delay));
		}
		//_projectile = pattern.Projectile;
		//_direction = pattern.Directions[0];
		//_spawnDelay = pattern.Delay;

		_timerEnd = Time.time + _spawnDelay;
		_enabled = true;
	}
	public void Disable()
	{
		_enabled = false;
	}

	private void Update()
	{
		if (_patterns == null) return;
		foreach (ProjectilePattern pattern in _patterns)
		{
			if (pattern.CanSpawn)
			{
				SpawnProjectiles(pattern);
			}
		}
		//if (Time.time >= _timerEnd && _enabled)
		//{
		//	_timerEnd = Time.time + _spawnDelay;

		//	CalculatePoints();
		//	SpawnProjectiles();
		//}
	}

	private void SpawnProjectiles(ProjectilePattern pattern)
	{
		Vector2 dir = pattern.Directions[(int)Random.Range(0, pattern.Directions.Length)];
		Vector2 p1 = new Vector2();
		Vector2 p2 = new Vector2();
		CalculatePoints(dir, out p1, out p2);
		GameObject go = GameObject.Instantiate(pattern.Projectile, Vector2.Lerp(p1, p2, Random.value), Quaternion.identity);
		go.transform.up = dir.normalized;

		if (go.TryGetComponent<PointAt>(out PointAt tracker))
		{
			tracker.Target = _player;
		}
	}

	private void CalculatePoints(Vector2 dir, out Vector2 p1, out Vector2 p2)
	{
		float m = -(dir.x / dir.y);
		Vector2 d = new Vector2(1 / Mathf.Sqrt(1 + m), m / Mathf.Sqrt(1 + m)) * _radius;

		p1 = (-dir * _radius) - d + (Vector2)transform.position;
		p2 = (-dir * _radius) + d + (Vector2)transform.position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(_p1, 0.1f);
		Gizmos.DrawSphere(_p2, 0.1f);
	}
}
public class ProjectilePattern
{
	public GameObject Projectile { get; private set; }
	public Vector2[] Directions { get; private set; }
	public float Delay { get; private set; }

	private float _timerEnd;
	public bool CanSpawn { get { bool val = Time.time >= _timerEnd; _timerEnd = (val) ? Time.time + Delay : _timerEnd; return val; }
	}

	public ProjectilePattern(GameObject proj, List<Vector2> dirs, float del)
	{
		Projectile = proj;
		Directions = dirs.ToArray();
		Delay = del;

		_timerEnd = Time.time + Delay;
	}

}
