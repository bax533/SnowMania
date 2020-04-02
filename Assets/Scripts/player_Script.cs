﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player_Script : MonoBehaviour {

    Rigidbody rb;
    bool spacebar, spin, left, down, right, up, started;

    private float rotationEps = 4.5f;
    float prevY_Flip = 0;

    float inputX, inputY, triggers;
    float flipRotBack, flipRotFront, currentSpinSpeed;
    public GameObject objectToSpin;
    public Collider[] colliders;
    public Animator anim;
    public float goodAngle = 100f;

    private Joystick joystick;
    private JoyButtonScript rideButton, spinButton;

    public ParticleSystem impactSnow;
    public ParticleSystem brakeSnow;

    //ski skins
    public GameObject[] skiParts; // 0 - bottom; 1 - pattern1; 2 - pattern2;
    public GameObject[] helmetParts; // 0 - bottom; 1 - pattern;

    public Material[] skinMaterials;



    //Ragdoll Objects
    public GameObject Torso;
    public GameObject GameColliders;

    public GameObject FootL;
    public Collider FootL_Collider;
    public GameObject FootR;
    public Collider FootR_Collider;

    public GameObject FootLParent_Ragdoll;
    public GameObject FootRParent_Ragdoll;
    public GameObject FeetParent_Game;

    //
    public GameObject GameManager;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    void resetSki()
    {
        for (int i = 0; i < skiParts.Length; i++)
            skiParts[i].SetActive(false);
    }

    void SetSkiSkin()
    {
        resetSki();
        if(PlayerPrefs.HasKey("currentSki"))
        {
            string skinname = PlayerPrefs.GetString("currentSki");
            skiParts[0].SetActive(true);

            if (skinname[1] == 'd')
            {
                skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
                return;
            }

            if (skinname[1] == 'g')
            {
                skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[1];
                return;
            }

            
            int partIndex = Int32.Parse(skinname[2].ToString());
            int bottomIndex = Values.Instance.getMaterialIndex(skinname).Item1;
            int topIndex = Values.Instance.getMaterialIndex(skinname).Item2;

            if (partIndex == 2)
            {
                skiParts[partIndex].SetActive(true);
                skiParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
                skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
            }
            else
            {
                skiParts[partIndex].SetActive(true);
                skiParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
                skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
            }

        }
        else
        {
            skiParts[0].SetActive(true);
            skiParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
        }
    }

    void resetHelmet()
    {
        for (int i = 0; i < helmetParts.Length; i++)
            helmetParts[i].SetActive(false);
    }

    public void SetHelmetSkin()
    {
        resetHelmet();
        string skinname = PlayerPrefs.GetString("currentHelmet");
        helmetParts[0].SetActive(true);

        if (skinname[1] == 'd')
        {
            helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[0];
            return;
        }

        if (skinname[1] == 'g')
        {
            helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[1];
            return;
        }

        int partIndex = skinname[1] == 'p' ? 1 : 0;
        int bottomIndex = Values.Instance.getMaterialIndex(skinname).Item1;
        int topIndex = Values.Instance.getMaterialIndex(skinname).Item2;

        helmetParts[partIndex].SetActive(true);
        helmetParts[partIndex].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[topIndex];
        helmetParts[0].GetComponent<SkinnedMeshRenderer>().material = skinMaterials[bottomIndex];
    }


    void ActivateSkins()
    {
        SetSkiSkin();
        SetHelmetSkin();
    }

    void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
        spinButton = FindObjectsOfType<JoyButtonScript>()[1];
        rideButton = FindObjectsOfType<JoyButtonScript>()[0];

        ActivateSkins();

        impactSnow.Pause();
        brakeSnow.Pause();

        Values.Instance.state = Values.State.Air;
        rb = this.GetComponent<Rigidbody>();
        SetRagdoll(false);
        Values.Instance.END = false;
        score_Script.levelScore = 0;
    }

    float currentSpeed;

    float Min(float a, float b)
    {
        if (a < b) return a;
        else return b;
    }

    float Max(float a, float b)
    {
        if (a > b) return a;
        else return b;
    }

    float abs(float x)
    {
        if (x < 0) return -x;
        else return x;
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0") || rideButton.pressed) spacebar = true;
        else                             spacebar = false;

        if (spacebar) started = true;

        if (Input.GetKey(KeyCode.S) || Input.GetKey("joystick button 2") || spinButton.pressed) spin = true;
        else                         spin = false;

        if (Input.GetKey(KeyCode.LeftArrow)) left = true;
        else                                 left = false;

        if (Input.GetKey(KeyCode.DownArrow)) down = true;
        else                                 down = false;

        if (Input.GetKey(KeyCode.RightArrow)) right = true;
        else                                  right = false;

        if (Input.GetKey(KeyCode.UpArrow)) up = true;
        else                               up = false;

        inputX = /*joystick.Horizontal;//*/Input.GetAxis("Horizontal");
        inputY = /*-joystick.Vertical;//*/Input.GetAxis("Vertical");
        triggers = /*-1.75f*Input.acceleration.x;//*/-Input.GetAxis("Trigger");

        flipRotBack = (transform.localEulerAngles.x + 90f) % 360;
        flipRotFront = (transform.localEulerAngles.x + 270f) % 360;
        //if (transform.localEulerAngles.x >= 270f && transform.localEulerAngles.y < 179.9f)
        //    flipRotFront = 180 - flipRotFront;


        

        if (inputX == 0 && inputY == 0)
        {
            inputX = right ? 1f : left ? -1f : 0f;
            inputY = up ? -1f : down ? 1f : 0f;
        }

        anim.SetFloat("triggers", triggers);
        anim.SetBool("crunching", spacebar);
        anim.SetFloat("Flipping", abs(triggers));

        if (anim.GetBool("backwards"))
        {
            anim.SetFloat("Flip", -triggers + 1);
            flipRotBack -= 180f;
        }
        else
        {
            anim.SetFloat("Flip", triggers + 1);
        }

        anim.SetFloat("InputX", inputX);
        anim.SetFloat("InputY", -inputY);

        anim.SetFloat("flipRotBack", flipRotBack);//abs(180 - transform.eulerAngles.x) % 360  - gąsieniczka
        anim.SetFloat("flipRotFront", flipRotFront);

    }

    void Move()
    {
        anim.SetFloat("angle", anim.GetFloat("angle") * 0.97f);

        if(Values.Instance.state == Values.State.Ground)
        {
            //Debug.Log(spacebar);
            //Debug.Log(currentSpeed.ToString() + " < " + Values.Instance.maxGround_Speed.ToString());
            if(spacebar)
                currentSpeed = Min(Values.Instance.maxGround_Speed, currentSpeed + Values.Instance.addGround_Speed);
            else
                currentSpeed = Max(Values.Instance.minGround_Speed, currentSpeed - Values.Instance.brakeGround_Speed);
            
            rb.velocity = new Vector3(0f, rb.velocity.y, currentSpeed);
        }
        else if (Values.Instance.state == Values.State.Air)
        {
            if (currentSpeed > Values.Instance.airSpeed)
                currentSpeed *= 0.99f;
            rb.velocity = new Vector3(0f, rb.velocity.y, currentSpeed);
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
                Flip();
            Spin();
        }
        else if(Values.Instance.state == Values.State.Grind)
        {
            Spin();
        }
        else if(Values.Instance.state == Values.State.Finish)
        {
            if (currentSpeed > 5f)
                currentSpeed *= Values.Instance.brakeFinish_Mult;
            else if (currentSpeed > 0.1f)
                currentSpeed *= Values.Instance.brakeFinish_Mult - 0.05f;
            else
            {
                currentSpeed = 0;
                anim.SetBool("stopped", true);
                Debug.Log("TUTAJ");
                GameManager.GetComponent<MainManager>().EndLevel();
            }
            rb.velocity = new Vector3(0f, rb.velocity.y, currentSpeed);
        }
    }
	
    void END_Movement()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.velocity *= 0;
        brakeSnow.Pause();
    }

    void SetRagdoll(bool active)
    {
        if(active)
        {
            GameColliders.SetActive(false);
            rb.useGravity = false;
            anim.enabled = false;
            FootL.transform.SetParent(FootLParent_Ragdoll.transform);
            FootR.transform.SetParent(FootRParent_Ragdoll.transform);
            Torso.SetActive(true);
            FootL.SetActive(true);
            FootR.SetActive(true);
            Torso.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -5000f, 10000f));
            GameManager.GetComponent<MainManager>().Crash();
        }
        else
        {
            rb.useGravity = true;
            GameColliders.SetActive(true);
            anim.enabled = true;
            FootL.transform.SetParent(FeetParent_Game.transform);
            FootR.transform.SetParent(FeetParent_Game.transform);
            Torso.SetActive(false);
            FootL.SetActive(false);
            FootR.SetActive(false);
        }
    }

    void Flip()
    {
        Vector3 m_EulerAngleVelocity = new Vector3(-triggers * Values.Instance.flipRotation, 0f, 0f);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        Debug.Log(transform.rotation.eulerAngles.y);
        if (anim.GetBool("Backwards") == false)
        {
            if (prevY_Flip > 180 && transform.rotation.eulerAngles.y <= 180)
            {
                //Debug.Log("zwiekszam flipa");
                anim.SetInteger("Flips", anim.GetInteger("Flips") + 1);
            }
        }
        else
        {
            if (prevY_Flip < 180 && transform.rotation.eulerAngles.y >= 180)
            {
                //Debug.Log("zwiekszam flipa");
                anim.SetInteger("Flips", anim.GetInteger("Flips") + 1);
            }
        }
        //Debug.Log(transform.rotation.eulerAngles);
        //transform.Rotate(new Vector3(-triggers * Values.Instance.flipRotation, 0f, 0f), Space.World); // flipping
        prevY_Flip = transform.rotation.eulerAngles.y;
    }

    void Spin()
    {
        if (spin)
        {
            currentSpinSpeed = Min(currentSpinSpeed + Values.Instance.addSpinSpeed, Values.Instance.maxSpinSpeed);
            if (objectToSpin.transform.localEulerAngles.y % 180 <= rotationEps)
            {
                //Debug.Log("zwiekszam 180");
                objectToSpin.transform.Rotate(new Vector3(0f, -rotationEps, 0f), Space.Self);
                anim.SetInteger("180s", anim.GetInteger("180s") + 1);
            }
        }

        if (objectToSpin.transform.localEulerAngles.y % 180 > rotationEps)
        {
            objectToSpin.transform.Rotate(new Vector3(0f, -0.5f, 0f) * currentSpinSpeed, Space.Self);
        }
    }

	void Dbg()
	{
        //Debug.Log(Values.Instance.state.ToString());
        //Debug.Log(anim.GetInteger("180s"));
        Debug.Log(joystick.Horizontal + "  -  " + joystick.Vertical);
	}    

    void PlayParticles(ParticleSystem particles)
    {
        particles.transform.position = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
        particles.Play();
    }

    void ToAir()
    {
        Values.Instance.state = Values.State.Air;

        anim.SetBool("InAir", true);
        anim.Play("jump");
        //rb.freezeRotation = true;
        //rb.angularVelocity = Vector3.zero;
        rb.constraints = rb.constraints | RigidbodyConstraints.FreezeRotationX;
    }

    void ToGrind(Transform railTransform)
    {
        prevY_Flip = 0;
        Values.Instance.state = Values.State.Grind;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY;
        transform.rotation = railTransform.rotation;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("shiftyHold"))
        {
            //Debug.Log("END  GRIND");
            SetRagdoll(true);
        }

    }

    void ToGround()
    {
        if(started)
            PlayParticles(impactSnow);

        Values.Instance.state = Values.State.Ground;
        anim.SetBool("InAir", false);

        anim.SetInteger("Flips", 0);
        anim.SetInteger("Spins", 0);

        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;

        if (!started)
        {
            anim.Play("beforeRun");
            return;
        }
        anim.Play("Landing");
        if (objectToSpin.transform.localEulerAngles.y > 180f - rotationEps && objectToSpin.transform.localEulerAngles.y < 180f + rotationEps)
        {
            //anim.Play("IDLEtoFAKIE");
            anim.SetBool("backwards", true);
            //objectToSpin.transform.localEulerAngles.y = 180f;
        }
        else if (objectToSpin.transform.localEulerAngles.y < rotationEps)
        {
            //anim.Play("IDLE");
            anim.SetBool("backwards", false);
        }
        else 
        {
            //Debug.Log("END  SPIN");
            SetRagdoll(true);
        } 
            
        currentSpinSpeed = 0f;
    }
    void ToFinish()
    {
        brakeSnow.Play();
        if (anim.GetBool("backwards"))
            anim.Play("ENDFakie");
        else
            anim.Play("ENDForward");
    }

    void CheckCollisions()
    {
        foreach(Collider col in colliders)
        {
            Collider[] hits = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Level"));

            foreach(Collider hit in hits)
            {
                //Debug.Log(hit.tag.ToString());
                if (col.name == "skiCollider")
                {       
                    if (hit.tag == "Finish")
                    {
                        Values.Instance.state = Values.State.Finish;
                        ToFinish();
                        //return;
                    }
                    if (hit.tag == "Grind")
                    {
                        //Debug.Log("GRINDOWANE JEST");
                        ToGrind(hit.transform);
                    }
                    if (hit.tag == "Air")
                    {
                        if (!(Values.Instance.state == Values.State.Air))
                        {
                            ToAir();
                        }
                        return;
                    }
                    if (hit.tag == "Ground")
                    {
                        if (Values.Instance.state == Values.State.Air || Values.Instance.state == Values.State.Grind)
                        {
                            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("trickEnter") && !anim.GetCurrentAnimatorStateInfo(0).IsName("IDLE") && !anim.GetCurrentAnimatorStateInfo(0).IsName("jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("beforeRun"))
                            {
                                col.enabled = false;
                                Collider[] lSkiHits = Physics.OverlapBox(FootL_Collider.bounds.center, FootL_Collider.bounds.extents, FootL_Collider.transform.rotation, LayerMask.GetMask("Level"));
                                Collider[] rSkiHits = Physics.OverlapBox(FootR_Collider.bounds.center, FootR_Collider.bounds.extents, FootR_Collider.transform.rotation, LayerMask.GetMask("Level"));

                                //Debug.Log(Max(lSkiHits.Length, rSkiHits.Length));

                                foreach (Collider skiHit in lSkiHits)
                                {
                                    //Debug.Log(skiHit.tag);
                                    if (skiHit.tag == "Ground")
                                    {
                                        Values.Instance.END = true;
                                        SetRagdoll(true);
                                        //Debug.Log("END");
                                    }
                                }
                                foreach (Collider skiHit in rSkiHits)
                                {
                                    //Debug.Log(skiHit.tag);
                                    if (skiHit.tag == "Ground")
                                    {
                                        Values.Instance.END = true;
                                        SetRagdoll(true);
                                        //Debug.Log("END");
                                    }
                                }
                                //col.enabled = true;
                                break;
                            }
                            else
                            {
                                ToGround();
                                Values.Instance.state = Values.State.Ground;
                            }
                        }
                    }
                }
                else if (col.name == "Back" || col.name == "Front")
                {
                    if(hit.tag == "Ground")
                    {
                        Values.Instance.END = true;
                        SetRagdoll(true);
                    }
                }
                else
                {
                    if (hit.tag == "Air")
                    {
                        if (!(Values.Instance.state == Values.State.Air))
                            ToAir();
                        Values.Instance.state = Values.State.Air;
                        return;
                    }
                    if (hit.tag == "Ground" || hit.tag == "Grind")
                    {
                        Values.Instance.END = true;
                        SetRagdoll(true);
                        Debug.Log("WYPIERDOLKA od: " + col.name + " w " + hit.tag);
                    }
                }
            }

        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        
        if (!Values.Instance.PAUSE)
        {
            GetInput();
            
            if (!Values.Instance.END)
            {
                CheckCollisions();
                Move();
            }
            else
            {
                //END_Movement();
            }
        }

       // - - - - - - - - - - - - - - - -
        //if (spacebar)
            //ToAir();
		Dbg ();
	}
}
/* TODO
 * achievments in levels
 * dodawanie flipów tyłem jest ostro spierdolone
 * generalnie liczenie flipów jest spierdolone (backflip na raila i frontflip of to 2x backflip)
 */
