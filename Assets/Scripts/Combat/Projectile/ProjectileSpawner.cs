using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField]
	private float _radius = 10.0f;
	[SerializeField]
	private GameObject _projectile;
	private bool _enabled = false;
	private Vector2 p1, p2;

	public void Enable(ProjectilePatternBase pattern)
	{ 
		_projectile = pattern.Projectile;
		_enabled = true;
	}

	private void Update()
	{
		if (!_enabled) return;
	}
}
