using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Pattern", menuName = "OverStory/New Projectile Pattern")]
public class ProjectilePatternBase : ScriptableObject
{
	[SerializeField]
	private GameObject _projectile;
	[SerializeField]
	private float _spawnDelay = 1.0f;
	[SerializeField]
	private List<Vector2> _directions = new List<Vector2>();

	public GameObject Projectile => _projectile;
	public float Delay => _spawnDelay;
	public List<Vector2> Directions => _directions;
}
