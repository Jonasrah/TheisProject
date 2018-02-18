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
			Vector3 targVel = Vector3.Cross(transform.right, Vector3.up) * wheelRigidbody.velocity.magnitude;
			wheelRigidbody.velocity = Vector3.Lerp(wheelRigidbody.velocity, targVel, Time.fixedDeltaTime * 10);
			scrollDelta = 0;
		}
		else
		{
			wheelRigidbody.angularVelocity = Vector3.Lerp(
									wheelRigidbody.angularVelocity, Vector3.zero, Time.fixedDeltaTime * 50);
		}
		Vector3 rollDir = Vector3.Project(wheelRigidbody.angularVelocity, transform.right);
		Vector3 forwardDir = Vector3.Project(wheelRigidbody.velocity, Camera.main.transform.forward);
		wheelRigidbody.angularVelocity = rollDir;
		//wheelRigidbody.velocity = forwardDir;

	}

	private void LateUpdate() {
		Vector3 camRight = Camera.main.transform.right;
		camRight.y = 0;
		camRight = camRight.normalized;
		float clamp = Mathf.Clamp01(45 / Vector3.Angle(transform.right, camRight));
		Vector3 clampedCamRight = Vector3.Lerp(transform.right, camRight, clamp);
		Quaternion upRot = Quaternion.FromToRotation(transform.right, camRight);
		transform.rotation = Quaternion.Slerp(transform.rotation, upRot * transform.rotation, Time.deltaTime * 10);
		//transform.Rotate(Vector3.up * difInRadians, Space.World);
		
	}
}
