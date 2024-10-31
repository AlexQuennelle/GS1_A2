using UnityEngine;

public class HealthEffect : MonoBehaviour
{
	[SerializeField]
	private GameObject _damageVFX;
	[SerializeField]
	private GameObject _healVFX;
	[SerializeField]
	private Health _health;
	[SerializeField]
	private SpriteRenderer _sr;
	private int _flashCounter = 0;

	private void OnEnable()
	{
		_health.OnHealthChange += OnHealthChange;
	}
	private void OnDisable()
	{
		_health.OnHealthChange -= OnHealthChange;
	}

	private void OnHealthChange(Health health, int dmg, int type)
	{
		if (type < 0)
		{
			_sr.material.SetFloat("_Flash", 1.0f);
			_flashCounter = (int)(Mathf.Pow(Time.deltaTime, -1.0f) * ((2.0f / 3.0f) / 10.0f));
			if (_damageVFX != null)
			{
				if (_damageVFX.TryGetComponent<ParticleSystem>(out ParticleSystem effect))
				{
					effect.Play();
				}
			}
		}
		if (type > 0 && _healVFX != null)
		{
			if (_healVFX.TryGetComponent<ParticleSystem>(out ParticleSystem effect))
			{
				effect.Play();
			}
		}
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
