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
	float max;

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

			if (rotationDiff > 0 && rotationDiff < 180) {
				isRotating = true;
				List.Add (rotationDiff);
				max = Mathf.Max (List.ToArray ());
				rotationDiff = 0;

			} else if (rotationDiff == 0 && isRotating == true) {
				isRotating = false;
				WriteToArduino ("1");
				Debug.Log ("Rotated with: " + max);
				List.Clear ();
				max = 0;
			}

			oldRotation = rb.transform.eulerAngles.x;
		}
	}

	public void WriteToArduino(string message){
		port.WriteLine(message);
		port.BaseStream.Flush();
	}
}
