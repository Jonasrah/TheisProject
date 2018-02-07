using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour {
	private Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		Vector3 camDir = Camera.main.transform.forward;
		camDir.y = 0;
		camDir = camDir.normalized;
		float dot = Mathf.Abs(Vector3.Dot(transform.forward, camDir));
		Vector3 wheelDir = Vector3.Project(transform.forward, Vector3.forward);
		Quaternion rotation = Quaternion.Euler(transform.localEulerAngles.x,0,0);
		Quaternion camLookRot = Quaternion.LookRotation(camDir);
		Vector3 newDir = new Vector3(0, wheelDir.y, wheelDir.magnitude);
		//float rotate = Vector3.Angle();
		Debug.DrawRay(transform.position, wheelDir, Color.red);
		Quaternion targetRotation = camLookRot * rotation;
		//transform.Rotate(Vector3.up * (transform.eulerAngles.y - camLookRot.eulerAngles.y) * Time.deltaTime * 5, Space.World);
		rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5));
		//transform.rotation = camLookRot;
		//wheelRigidbody.MoveRotation(targetRotation);
	}
}
