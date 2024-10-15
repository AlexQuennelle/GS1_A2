using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
	[SerializeField]
	private SelfDestructor _sd;
	[SerializeField]
	private GameObject _prefab;

	private void OnEnable()
	{
		_sd.OnDestroy += OnDestroy;
	}
	private void OnDisable()
	{
		_sd.OnDestroy -= OnDestroy;
	}

	private void OnDestroy(SelfDestructor sd)
	{
		GameObject go = GameObject.Instantiate(_prefab, transform.position, Quaternion.identity);
	}
}
