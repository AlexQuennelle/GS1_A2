using UnityEngine;

public class PointAt : MonoBehaviour
{
	//the target transform to be pointed at
	private Transform _target;
	[
		SerializeField,
		Range(0, 180),
		Tooltip(
				"The maximum number of degrees the object can rotate per update" + "\n" +
				"The lower the number, the slower the object rotates" + "\n" +
				"Setting this to 0 makes it point at the target instantly"
				)
	]
	private float _maxAngle;

	//method that is called repeatedly at a fixed rate
	void FixedUpdate()
	{
		//get the direction vector that points from the current object to the target
		Vector2 targetV = (_target.position - transform.position).normalized;
		//if the maxAngle variable is 0, then set the object's transform.up vector to be the target vector, and return
		//this causes the object to instantly rotate to point at the target, and exits the function early
		if (_maxAngle == 0)
		{
			transform.up = targetV;
			return;
		}
		//call the vector3 version of the RotateTowards() function with the target vector, up transform, and max angle values
		//this function does exactly what the name implies, and returns a vector rotated towards matching the target
		//the returned vector will never be more than the max angle degrees away from the initial up transform
		//by storing this new vector back into transform.up, the rotation is applied to the object
		transform.up = Vector3.RotateTowards(transform.up, targetV, Mathf.Deg2Rad * _maxAngle, 0);
	}

	public Transform Target { get { return _target; } set { _target = value; } }
}
