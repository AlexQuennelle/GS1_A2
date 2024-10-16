using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

///<summary>
///Component that destroys the attached game object after a lifetime timer runs out.
///Immediately before the game object is destroyed, an OnDestroy event is invoked.
///</summary>
public class SelfDestructor : MonoBehaviour
{
	[SerializeField]
	private MinMaxCurve _lifetime = new MinMaxCurve(1.0f, 2.0f);
	private float _eol;

	//event called immediately before the object is destroyed
	public event Action<SelfDestructor> OnDestroy;

	//initialize the end of life time when the object is loaded
	private void OnEnable()
	{
		//evalutate the lifetime object with a time of 0 and a random lerp factor
		//this returns a float between the 2 constants set in the inspector
		//adding the returned value to Time.time gives us an end of life time we can check against
		_eol = Time.time + _lifetime.Evaluate(0, Random.value);
	}

	//every frame we check Time.time against the end of life variable
	//this tells us if the gameObject should be destroyed
	private void Update()
	{
		if (Time.time >= _eol)
		{
			OnDestroy?.Invoke(this);
			Destroy(this.gameObject);
		}
	}
}
