using UnityEngine;

public class Heal : MonoBehaviour
{
	[SerializeField]
	private int _amount = 1;
	public event Action<Damage> OnHeal;

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col.gameObject.name);
		if (!col.gameObject.TryGetComponent<Health>(out Health health)) return;

		OnHeal?.Invoke(this);
		health.Heal(_amount);
	}
}
