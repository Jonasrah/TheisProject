using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
	public float rollSpeed = 1;
	private float scrollDelta;

	private Rigidbody wheelRigidbody;
	// Use this for initialization
	void Start ()
	{
		wheelRigidbody = GetComponent<Rigidbody>();
		wheelRigidbody.maxAngularVelocity = 50;
	}

	private void Update() {
		scrollDelta += Input.GetAxis("ScrollWheel");
	}

	private void FixedUpdate()
	{

		float scroll = scrollDelta * rollSpeed;
		
		if (Mathf.Abs(scroll) > 0f)
		{
			
			wheelRigidbody.AddTorque(transform.right * scroll, ForceMode.VelocityChange);
			scrollDelta = 0;
		}
		else
		{
			wheelRigidbody.angularVelocity = Vector3.Lerp(
									wheelRigidbody.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 50);
		}
		wheelRigidbody.angularVelocity = Vector3.Project(wheelRigidbody.angularVelocity, transform.right); // add backwards

	}

	private void LateUpdate() {
		Vector3 camRight = Camera.main.transform.right;
		camRight.y = 0;
		camRight = camRight.normalized;
		//float difInRadians = Vector3.Angle();
		Quaternion upRot = Quaternion.FromToRotation(transform.right, camRight);
		transform.rotation = Quaternion.Slerp(transform.rotation, upRot * transform.rotation, Time.deltaTime * 5);
		//transform.Rotate(Vector3.up * difInRadians, Space.World);
		
	}
}
