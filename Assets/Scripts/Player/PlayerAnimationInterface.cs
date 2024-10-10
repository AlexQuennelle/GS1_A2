using UnityEngine;

public class PlayerAnimationInterface : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	public Vector2 Facing { get; set; }
	public bool Moving { get; set; }

	private void Update()
	{
		if (Mathf.Abs(Facing.x) != Mathf.Abs(Facing.y))
		{
			_animator.SetFloat("inputX", Facing.x);
			_animator.SetFloat("inputY", Facing.y);
		}
		_animator.SetBool("moving", Moving);
	}
}
