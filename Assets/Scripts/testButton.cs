using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testButton : MonoBehaviour {

	void Update () {
        GetComponent<Text>().text = Values.Instance.maxGround_Speed.ToString();
	}
}
