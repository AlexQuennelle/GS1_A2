using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SelfDestructor : MonoBehaviour
{
	[SerializeField]
	private MinMaxCurve _lifetime = new MinMaxCurve(1.0f, 3.0f);
	private float _eol;

	//event called immediately before the object is destroyed
	public event Action<SelfDestructor> OnDestroy;

	private void OnEnable()
	{
		_eol = Time.time + _lifetime.Evaluate(0);
	}

	private void Update()
	{
		if (Time.time >= _eol)
		{
			OnDestroy?.Invoke(this);
			Destroy(this.gameObject);
		}
	}
}
