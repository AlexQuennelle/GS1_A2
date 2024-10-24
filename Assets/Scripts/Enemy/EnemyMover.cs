using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyMover : MonoBehaviour
{
	[SerializeField]
	private Transform _player;
	[SerializeField]
	private float _moveSpeed = 1.0f;
	[SerializeField]
	private float _chaseSpeed = 2.0f;
	[SerializeField]
	private float _wanderRange = 3.0f;
	[SerializeField]
	private float _aggroRange = 5.0f;
	[SerializeField]
	private MinMaxCurve _behaviourPeriod = new MinMaxCurve(0.0f, 1.0f);
	private float _behaviourChange;
	private bool _wandering;

	[SerializeField]
	private float _easeDuration = 1.0f;
	private float _behaviourLength;

	[SerializeField]
	private Rigidbody2D _rb;

	private Vector2 _targetPos;
	private Vector2 _startPos;

	private void OnValidate()
	{
		_startPos = transform.position;
	}
	private void OnEnable()
	{
		_startPos = transform.position;
		PickWanderTarget();
		_wandering = (Random.value > 0.5f) ? true : false;
		_behaviourLength = _behaviourPeriod.Evaluate(0, Random.value);
		_behaviourChange = Time.time + _behaviourLength;
	}

	private void FixedUpdate()
	{
		if (Vector3.Distance(transform.position, _player.position) > _aggroRange)
		{
			Wander();
		}
		else
		{
			Chase();
		}
	}

	private void Chase()
	{
		_rb.velocity = (Vector2)(_player.position - transform.position).normalized * _chaseSpeed;
		_behaviourChange = Time.time + _behaviourPeriod.constantMax;
		_wandering = true;
	}

	private void Wander()
	{
		if (Time.time >= _behaviourChange)
		{
			_wandering = !_wandering;
			_behaviourLength = _behaviourPeriod.Evaluate(0, Random.value);
			_behaviourChange = Time.time + _behaviourLength;
			PickWanderTarget();
		}
		if (Vector3.Distance(transform.position, _targetPos) < 0.01f)
		{
			PickWanderTarget();
		}
		if (_wandering)
		{
			_rb.velocity = ((Vector3)_targetPos - transform.position).normalized * Ease(_moveSpeed);
		}
		else
		{
			_rb.velocity = Vector2.zero;
		}
	}

	private float Ease(float speed)
	{
		float timeInState = _behaviourChange - Time.time;
		float tIn = (timeInState >= _easeDuration) ? 1.0f : timeInState / _easeDuration;
		float tOut = (timeInState <= _behaviourLength - _easeDuration) ?
			0.0f :
			(timeInState - (_behaviourLength - _easeDuration)) / _easeDuration;
		float easeIn = Mathf.Lerp(0.0f, speed, tIn);
		float easeOut = Mathf.Lerp(speed, 0.0f, tOut);

		return Mathf.Lerp(easeIn, easeOut, timeInState / _behaviourLength);
	}

	private void PickWanderTarget()
	{
		_targetPos = _startPos + GetRandomVector();
	}
	private Vector2 GetRandomVector()
	{
		float r = Random.Range(0.0f, 360.0f);
		return new Vector2(Mathf.Cos(r), Mathf.Sin(r)) * Random.Range(0, _wanderRange);
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(_targetPos, 0.05f);
		Gizmos.DrawWireSphere(_startPos, _wanderRange);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _aggroRange);
	}
}
