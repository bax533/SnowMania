using System;
using UnityEngine;

public class camera_Script : MonoBehaviour {

    public GameObject target;


    public Vector3 lastLerp;
    Vector3 lastPos;

    public float smoothSpeed = 0.2f;
    public float endSpeed = 0.02f;
    public Vector3 offset;
    public float endOffset;
    public Vector3 mov;
    public float orthSize;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!Values.Instance.END)
        {
            Vector3 destination = target.transform.position + offset;
            orthSize = Math.Max(target.GetComponent<player_Script>().currentSpeed / 1.5f, 10.5f);
            GetComponent<Camera>().orthographicSize = orthSize;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position , destination, smoothSpeed);
            transform.position = smoothedPosition;
            lastPos = new Vector3(target.transform.position.x - lastLerp.x, target.transform.position.y - lastLerp.y, target.transform.position.z);
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
