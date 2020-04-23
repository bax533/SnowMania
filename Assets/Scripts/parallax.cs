using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {

    private float lengthX, lengthZ, startposX, startposZ;

    public GameObject cam;
    public float parallaxEffect;
    public float offsetX, offsetY, offsetZ;

	void Start () {
        startposX = transform.position.x;
        startposZ = transform.position.z + offsetX;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        /*float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float distX = (cam.transform.position.x * parallaxEffect);
        */
        float tempZ = (cam.transform.position.z * (1 - parallaxEffect));
        float distZ = (cam.transform.position.z * parallaxEffect);

        transform.position = new Vector3(startposZ + distZ + offsetX, cam.transform.position.y + offsetY, transform.position.z);

        /*if (tempX > startposX + lengthX) startposX += lengthX;
        if (tempX < startposX - lengthX) startposX -= lengthX;*/
    }
}
