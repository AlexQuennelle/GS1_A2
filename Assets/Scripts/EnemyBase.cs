using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "OverStory/New Enemy")]
public class EnemyBase : ScriptableObject
{
	[SerializeField]
	private Sprite _sprite;
	public Sprite BaseSprite => _sprite;
}
