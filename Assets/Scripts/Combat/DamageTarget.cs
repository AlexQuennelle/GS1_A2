using System;
using UnityEngine;

public class DamageTarget : MonoBehaviour
{
	[SerializeField]
	private Health _target;
	[SerializeField]
	private int _damage = 1;

	public event Action<DamageTarget> OnDamage;

	public Health Target { get { return _target; } set { _target = value; } }

	private void OnTriggerEnter2D(Collider2D col)
	{
		_target.TakeDamage(_damage);
        AudioManager.Instance.PlayHitSound();
		AudioManager.Instance.PlayhitEnemy();
        OnDamage?.Invoke(this);
	}
}
