using UnityEngine;

public class Damage : MonoBehaviour
{
	[SerializeField]
	private int _damage = 1;

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col.gameObject.name);
		if (!col.gameObject.TryGetComponent<Health>(out Health health)) return;

		health.TakeDamage(_damage);
		GameObject.Destroy(this.gameObject);
	}
}
