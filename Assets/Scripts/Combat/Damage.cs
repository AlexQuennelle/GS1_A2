using System;
using UnityEngine;

public class Damage : MonoBehaviour
{
	[SerializeField]
	private int _damage = 1;
	public event Action<Damage> OnDamage;

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col.gameObject.name);
		if (!col.gameObject.TryGetComponent<Health>(out Health health)) return;

		OnDamage?.Invoke(this);
		health.TakeDamage(_damage);
	}
}
