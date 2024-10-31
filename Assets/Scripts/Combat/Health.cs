using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _hp = 5;
	[SerializeField]
	private int _maxHp = 5;
	private bool _isDialogue = false;

	public event Action<Health, int, int> OnHealthChange;
	public event Action<Health> OnDeath;

	public int HP { get { return _hp; } set { _hp = value; } }
	public bool IsDialogue { get { return _isDialogue; } set { _isDialogue = value; } }


	public void Heal(int heal){
		_hp += heal;
		if (_hp > _maxHp) _hp = _maxHp;
		OnHealthChange?.Invoke(this, _hp, 1);
	}
	public void TakeDamage(int dmg)
	{
		if (IsDialogue) return;

		_hp -= dmg;

		OnHealthChange?.Invoke(this, _hp, -1);
		if (_hp <= 0)
		{
			_hp = 0;
			OnDeath?.Invoke(this);
		}
	}
}
