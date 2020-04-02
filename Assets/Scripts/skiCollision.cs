using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skiCollision : MonoBehaviour { //NIEUZYWANE

    public Rigidbody rb;
    public Animator anim;

    public float maxAngle = 95;
    void OnCollisionEnter(Collision collision)
    {
        
        // measure angle
        //Debug.Log(Vector3.Angle(vel, -normal));
    }
}
