using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

public class bottle_behavior : MonoBehaviour {
	Rigidbody rb;
	float rotation = 0;
	float oldRotation = 0;
	float rotationDiff = 0;
	SerialPort port;

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

			if (rotationDiff > 0) {
				WriteToArduino ("PING");
				Debug.Log ("Rotating");
				rotationDiff = 0;
			}
			oldRotation = rb.transform.eulerAngles.x;
		}
	}
		
	public void WriteToArduino(string message){
		port.WriteLine(message);
		port.BaseStream.Flush();
	}
}
