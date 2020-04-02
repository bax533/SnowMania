using UnityEngine;

public class camera_Script : MonoBehaviour {

    public Transform target;


    public Vector3 lastLerp;
    Vector3 lastPos;

    public float smoothSpeed = 0.2f;
    public float endSpeed = 0.02f;
    public Vector3 offset;
    public float endOffset;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!Values.Instance.END)
        {
            Vector3 destination = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, destination, smoothSpeed);
            transform.position = smoothedPosition;
            lastPos = new Vector3(target.position.x - lastLerp.x, target.position.y - lastLerp.y, target.position.z);
        }
        else
        {
            Vector3 destination = lastPos + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, destination, endSpeed);
            transform.position = smoothedPosition;
        }
        //transform.LookAt(target);
	}
}
