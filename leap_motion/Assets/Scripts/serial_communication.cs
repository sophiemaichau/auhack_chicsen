using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

public class serial_communication : MonoBehaviour {
	Rigidbody rb;
	float rotation = 0;
	float oldRotation = 0;
	float rotationDiff = 0;
	SerialPort port;
	bool isRotating = false;
	List<float> List = new List<float>();
	float degreesMoved;

	// Use this for initialization
	void Start (){
		rb = GetComponent<Rigidbody>();
		port = new SerialPort ("/dev/cu.usbmodem1421", 9600);
		port.Open();
	}

	// Update is called once per frame
	void Update () {
		if (port == null) {
			return;
		}

		if (port.IsOpen) {
			port.ReadTimeout = 1;
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
				int degreesInt = (int)degreesMoved;
				string degreesString = degreesInt.ToString ();
				Debug.Log ("Rotated with: " + degreesString);
				WriteToArduino (degreesString);
				List.Clear ();
				degreesMoved = 0;
				rotationDiff = 1;
				break;
			}

			oldRotation = rb.transform.eulerAngles.x;
		}
	}

	public void WriteToArduino(string message){
		port.WriteLine(message);
		port.BaseStream.Flush();
	}

	public float SumArray(float[] toBeSummed){
		float sum = 0;
		foreach (float item in toBeSummed){
			sum += item;
		}
		return sum;
	}
}
