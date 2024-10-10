using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 1.0f;
	private Rigidbody2D _rb;
	public Vector2 Direction { get; set; }

	private void OnEnable()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (_rb != null)
		{
			_rb.velocity = Direction * _moveSpeed;
		}
	}
}
