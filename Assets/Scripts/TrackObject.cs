using UnityEngine;

///<summary>
///Component that locks an object's position to a target object with a specified offset
///</summary>
public class TrackObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset = new Vector3(0.0f, 0.0f, 0.0f);
	///<value>The transform component of the target object</value>
    public Transform Target { get; set; }

	//set the current object's position to the target object's position plus the offset
	//this happens every frame
    private void Update()
    {
        transform.position = Target.position + _offset;
    }
}
