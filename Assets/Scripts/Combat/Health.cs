using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _hp = 5;

	public event Action<Health, int> OnDamage;
	public event Action<Health> OnDeath;

	public int HP { get { return _hp; } set { _hp = value; } }


    public void TakeDamage(int dmg)
	{
		_hp -= dmg;

		OnDamage?.Invoke(this, _hp);
		if (_hp <= 0)
		{
			_hp = 0;
			OnDeath?.Invoke(this);
		}
	}
}
