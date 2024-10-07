using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 1.0f;
	[SerializeField]
	private PlayerInput _playerInput;
	[SerializeField]
	private Animator _animator;
	private Rigidbody2D _rb;
	private Vector2 _inputDir;

	private void OnEnable()
	{
		_playerInput.actions.FindAction("Move").performed += HandleMove;
		_playerInput.actions.FindAction("Move").canceled += StopMove;
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnDisable()
	{
		_playerInput.actions.FindAction("Move").performed -= HandleMove;
		_playerInput.actions.FindAction("Move").canceled -= StopMove;
	}

	private void FixedUpdate()
	{
		if (_rb != null)
		{
			_rb.velocity = _inputDir * _moveSpeed;
		}
	}

	private void HandleMove(InputAction.CallbackContext context)
	{
		_inputDir = context.ReadValue<Vector2>();
		if (Mathf.Abs(_inputDir.x) != Mathf.Abs(_inputDir.y))
		{
			_animator.SetFloat("inputX", _inputDir.x);
			_animator.SetFloat("inputY", _inputDir.y);
		}
	}

	private void StopMove(InputAction.CallbackContext context)
	{
		_inputDir = Vector2.zero;
	}
}
