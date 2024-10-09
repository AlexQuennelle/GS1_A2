using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
	[SerializeField]
	private float _lifetime = 1.0f;
	private float _eol;

	private void OnEnable()
	{
		_eol = Time.time + _lifetime;
	}

	private void Update()
	{
		if (Time.time >= _eol)
		{
			Destroy(this.gameObject);
		}
	}
}
