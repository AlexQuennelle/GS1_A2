using UnityEngine;

public class TrackObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset = new Vector3(0.0f, 0.0f, 0.0f);
    public Transform Target { get; set; }

    private void Update()
    {
        transform.position = Target.position + _offset;
    }
}
