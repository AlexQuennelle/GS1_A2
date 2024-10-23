using UnityEngine;

public class DamageEffect : MonoBehaviour
{
	[SerializeField]
	private Health _health;
	[SerializeField]
	private SpriteRenderer _sr;
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
		_flashCounter = (int)(Mathf.Pow(Time.deltaTime, -1.0f) * ((2.0f / 3.0f) / 10.0f));
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
