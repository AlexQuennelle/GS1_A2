using UnityEngine;

public class ProjectileWavyMover : ProjectileMover
{
	[SerializeField]
	private float _amplitude = 1.0f;
	[SerializeField]
	private float _frequency = 1.0f;
	private float _startTime;

	protected override void OnEnable()
	{
		_startTime = Time.time;
		base.OnEnable();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		_rb.velocity = _rb.velocity + (Vector2)(transform.right * Mathf.Cos((Time.time - _startTime) * _frequency) * _amplitude);
	}
}
