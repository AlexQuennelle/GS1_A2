using UnityEngine;

public class EnemyActor : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _sr;

	private bool _ready = false;

	private void Update()
	{
		if (!_ready) return;
	}

	public void Initialize(EnemyBase baseEnemy)
	{
		_sr.sprite = baseEnemy.BaseSprite;
	}
}
