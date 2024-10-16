using UnityEngine;

///<summary>
///Component that acts as an interface between <see cref="PlayerInputHandler">player input</see> and an animator.
///</summary>
public class PlayerAnimationInterface : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	///<value>The direction the player sprite should face.</value>
	public Vector2 Facing { get; set; }
	///<value>Whether the player is moving or not.</value>
	public bool Moving { get; set; }

	//updates the animator properties every frame
	private void Update()
	{
		//check if the facing direction is along one of the 4 45 degree axes
		//if it isn't, run the code block inside
		if (Mathf.Abs(Facing.x) != Mathf.Abs(Facing.y))
		{
			//update the animator properties for x and y input
			_animator.SetFloat("inputX", Facing.x);
			_animator.SetFloat("inputY", Facing.y);
		}
		//update the moving boolean in the animator
		_animator.SetBool("moving", Moving);
	}
}
