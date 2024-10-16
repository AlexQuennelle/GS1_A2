using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ProjectileMover : MonoBehaviour
{
	[SerializeField]
	protected MinMaxCurve _velocity = new MinMaxCurve(0.0f, 1.0f);
	protected Rigidbody2D _rb;

	private void OnEnable()
	{
		if (!TryGetComponent<Rigidbody2D>(out _rb))
		{
			Debug.LogWarning($"{this.gameObject.name} doesn't have a Rigidbody2D attached.");
		}
		_rb.velocity = transform.up * _velocity.Evaluate(0, Random.value);
	}

	protected virtual void FixedUpdate()
	{
	}
}