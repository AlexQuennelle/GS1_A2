using UnityEngine;
using UnityEngine.InputSystem;

///<summary>
///Component that bridges the <see cref="InputSystem">Unity input system</see> and custom scritps.
///Listens to input events and sends that data down to other associated scripts
///</summary>
public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField]
	private PlayerInput _playerInput;
	[SerializeField]
	private PlayerMover _pm;
	[SerializeField]
	private PlayerAnimationManager _animation;

	private void OnEnable()
	{
		_playerInput.actions.FindAction("Move").performed += HandleDirInput;
		_playerInput.actions.FindAction("Move").canceled += StopDirInput;
	}
	private void OnDisable()
	{
		_playerInput.actions.FindAction("Move").performed -= HandleDirInput;
		_playerInput.actions.FindAction("Move").canceled -= StopDirInput;
	}

	private void HandleDirInput(InputAction.CallbackContext context)
	{
		Vector2 inputDir = context.ReadValue<Vector2>();
		_pm.Direction = inputDir;
		_animation.Facing = inputDir;
		_animation.Moving = true;
	}
	private void StopDirInput(InputAction.CallbackContext context)
	{
		_pm.Direction = Vector2.zero;
		_animation.Moving = false;
	}
}
