using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle_behavior : MonoBehaviour {
	Rigidbody rb;
	float velocity;

	// Use this for initialization
	void Start (){
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.angularDrag += 1f;
		velocity = rb.angularVelocity.magnitude;
		Debug.Log (velocity);
	}
}
