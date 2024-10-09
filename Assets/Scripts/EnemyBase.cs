using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "OverStory/New Enemy")]
public class EnemyBase : ScriptableObject
{
	[SerializeField]
	private Sprite _sprite;
	[SerializeField]
	private int _hp = 1;

	public Sprite BaseSprite => _sprite;
	public int HP => _hp;
}
