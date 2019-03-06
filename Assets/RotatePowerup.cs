using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerup : MonoBehaviour {

    public float speed = 40.0f;

	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);	
	}
}
