using UnityEngine;

public class DestroyOnDamage : MonoBehaviour
{
	[SerializeField]
	Damage _damage;

	private void OnEnable()
	{
		_damage.OnDamage += OnDamage;
	}
	private void OnDisable()
	{
		_damage.OnDamage -= OnDamage;
	}

	private void OnDamage(Damage damage)
	{
		_damage.OnDamage -= OnDamage;
		GameObject.Destroy(this.gameObject);
	}
}
