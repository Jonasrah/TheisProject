using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
	public float rollSpeed = 1;

	private Rigidbody wheelRigidbody;
	// Use this for initialization
	void Start ()
	{
		wheelRigidbody = GetComponent<Rigidbody>();
		wheelRigidbody.maxAngularVelocity = 500;
	}

	private void FixedUpdate()
	{
		float scroll = Input.GetAxis("ScrollWheel") * rollSpeed;
		Vector3 camDir = Camera.main.transform.forward;
		camDir.y = 0;
		camDir = camDir.normalized;
		
		
		Vector3 rotation = transform.rotation.eulerAngles;
		Quaternion camLookRot = Quaternion.LookRotation(camDir);
		float rotate = camLookRot.eulerAngles.y;
		Quaternion targetRotation = Quaternion.Euler(rotation.x, rotate, 0);
		//transform.Rotate(0, rotate * Time.fixedDeltaTime * 5, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 30);
		
		if (Mathf.Abs(scroll) > 0f)
		{
			
			wheelRigidbody.AddTorque(transform.right * scroll, ForceMode.VelocityChange);
		}
		else
		{
			wheelRigidbody.angularVelocity = Vector3.Lerp(wheelRigidbody.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 30);
		}
		wheelRigidbody.angularVelocity = transform.right * wheelRigidbody.angularVelocity.magnitude; // add backwards
	}
}
