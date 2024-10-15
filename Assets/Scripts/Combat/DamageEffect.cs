using UnityEngine;

public class DamageEffect : MonoBehaviour
{
	[SerializeField]
	private Health _health;
	[SerializeField]
	private SpriteRenderer _sr;
	private bool _flash = false;
	private int _flashCounter = 0;

	private void OnEnable()
	{
		_health.OnDamage += OnDamage;
	}
	private void OnDisable()
	{
		_health.OnDamage -= OnDamage;
	}

	private void OnDamage(Health health, int dmg)
	{
		_sr.material.SetFloat("_Flash", 1.0f);
		_flashCounter = 2;
	}

	private void Update()
	{
		if (_flashCounter > 0)
		{
			_flashCounter--;
		}
		else
		{
			_sr.material.SetFloat("_Flash", 0.0f);
		}
	}
}
