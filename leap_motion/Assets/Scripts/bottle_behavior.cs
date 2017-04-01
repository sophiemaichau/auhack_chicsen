using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle_behavior : MonoBehaviour {
	Rigidbody rb;
	float rotation = 0;
	float oldRotation = 0;
	float rotationDiff = 0;
	bool isRotating = false;
	List<float> List = new List<float>();
	float degreesMoved;

	// Use this for initialization
	void Start (){
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.angularDrag += 1f;
		rotation = rb.transform.eulerAngles.x;
		rotationDiff = Mathf.Abs (oldRotation - rotation);

		if (rotationDiff > 1 && rotationDiff < 300) {
			isRotating = true;
			List.Add (rotationDiff);
			degreesMoved = SumArray(List.ToArray());
		}

		while (rotationDiff == 0 && isRotating == true) {
			isRotating = false;
			Debug.Log ("Rotated with: " + (int)degreesMoved);
			List.Clear ();
			degreesMoved = 0;
			rotationDiff = 1;
			break;
		}

		oldRotation = rb.transform.eulerAngles.x;
	}

	public float SumArray(float[] toBeSummed){
		float sum = 0;
		foreach (float item in toBeSummed){
			sum += item;
		}
		return sum;
	}
}
