using UnityEngine;

public class DamageArea : MonoBehaviour
{
	[SerializeField]
	private int _damagePerTick = 1;
	[SerializeField]
	private float _tickLength = 0.5f;
	private float _tickTimer;
	[SerializeField]
	private GameObject _area;

	private void OnEnable()
	{
		_tickTimer = Time.time + _tickLength;
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.TryGetComponent<Health>(out Health h))
		{
			h.TakeDamage(_damagePerTick);
		}
	}
	private void FixedUpdate()
	{
		if(Time.time >= _tickTimer)
		{
			_tickTimer = Time.time + _tickLength;
			_area.SetActive(true);
		}
		else{
			_area.SetActive(false);
		}
	}
}
