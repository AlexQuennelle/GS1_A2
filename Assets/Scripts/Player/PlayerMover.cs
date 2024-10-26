using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 2.5f;
	private Rigidbody2D _rb;
	public Vector2 Direction { get; set; }
	public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

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
