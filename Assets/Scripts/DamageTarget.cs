using UnityEngine;

public class DamageTarget : MonoBehaviour
{
	[SerializeField]
	private Health _target;
	[SerializeField]
	private int _damage = 1;

	private void OnTriggerEnter2D(Collider2D col)
	{
		_target.TakeDamage(_damage);
	}
}
