using UnityEngine;

///<summary>
///Addon component for a <see cref="SelfDestructor">SelfDestructor</see>.
///When the OnDestroy event is invoked, the specified prefab is instantiated.
///</summary>
public class SpawnOnDestroy : MonoBehaviour
{
	[SerializeField,
	Header("SelfDestructor component this component is associated with")]
	private SelfDestructor _sd;

	[SerializeField,
	Header("Prefab to instantiate when the game object is destroyed")]
	private GameObject _prefab;

	//when the component is loaded, listen for the OnDestroy event on _sd
	private void OnEnable()
	{
		_sd.OnDestroy += OnDestroy;
	}
	//when the component is unloaded, stop listening to the OnDestroy event on _sd
	private void OnDisable()
	{
		_sd.OnDestroy -= OnDestroy;
	}

	//method that is called when the OnDestroy event is invoked
	//instantiates _prefab at the current object's position with no rotation
	private void OnDestroy(SelfDestructor sd)
	{
		GameObject go = GameObject.Instantiate(_prefab, transform.position, Quaternion.identity);
	}
}
