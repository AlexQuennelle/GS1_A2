using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "OverStory/New Enemy")]
public class EnemyBase : ScriptableObject
{
	[SerializeField]
	private Sprite _sprite;
	[SerializeField]
	private RuntimeAnimatorController _animator;
	[SerializeField]
	private int _hp = 1;
	[SerializeField]
	private List<ProjectilePatternBase> _patterns;
	//private ProjectilePatternBase _pattern;

	public Sprite BaseSprite => _sprite;
	public RuntimeAnimatorController Animator => _animator;
	public List<ProjectilePatternBase> Patterns => _patterns;
	//public ProjectilePatternBase Pattern => _pattern;
	public int HP => _hp;
}
