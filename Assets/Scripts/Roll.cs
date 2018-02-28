using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
	public float rollSpeed = 1;
	private float scrollDelta;

	private Rigidbody wheelRigidbody;
	private bool grounded;
	private Vector3 normal = Vector3.up;
	[SerializeField] private float maxRollSpeed = 25;

	// Use this for initialization
	private void Start ()
	{
		wheelRigidbody = GetComponent<Rigidbody>();
		wheelRigidbody.maxAngularVelocity = 50;
	}

	private void Update() {
		scrollDelta += Input.GetAxis("ScrollWheel");
	}

	private void FixedUpdate()
	{
		float currentRollingSpeed = wheelRigidbody.angularVelocity.magnitude;
		Vector3 forward = Vector3.Cross(transform.right, normal);
		
		Vector3 targVel = forward * currentRollingSpeed;
		targVel.y += wheelRigidbody.velocity.y;
		
		Vector3 rollDir = Vector3.Project(wheelRigidbody.angularVelocity, transform.right);
		

		if (grounded) //wheelRigidbody.velocity = targVel;
		wheelRigidbody.angularVelocity = rollDir;
		
		Debug.DrawRay(transform.position, forward * wheelRigidbody.velocity.magnitude, Color.red, 0.1f);
		Debug.DrawRay(transform.position, wheelRigidbody.velocity, Color.green, 0.1f);

		if (Mathf.Abs(scrollDelta) > 0f) {
			float scrollForce = scrollDelta * rollSpeed;
			wheelRigidbody.angularVelocity = Vector3.ClampMagnitude(wheelRigidbody.angularVelocity, maxRollSpeed);
			wheelRigidbody.AddTorque(transform.right * scrollForce, ForceMode.VelocityChange);
			scrollDelta = 0;
		}
		else
		{
			/* Scroll wheel too unpredictable for this
			wheelRigidbody.angularVelocity = Vector3.zero;
			*/
		}
		
		
		RotateToCameraDirection();
		
		if (grounded) return;
		wheelRigidbody.AddForce(Vector3.down * 10, ForceMode.Acceleration);
	}

	private void RotateToCameraDirection() {
		Vector3 camRight = Camera.main.transform.right;
		camRight.y = 0;
		camRight = camRight.normalized;
		
		Quaternion upRot = Quaternion.FromToRotation(transform.right, camRight);
		transform.rotation = Quaternion.Slerp(transform.rotation, upRot * transform.rotation, Time.fixedDeltaTime * 10);
		
	}

	private void OnCollisionStay(Collision other)
	{
		grounded = true;

		if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			normal = other.contacts[0].normal;
		}
		
		Debug.DrawRay(other.contacts[0].point, normal * 3, Color.magenta);
	}
	private void OnCollisionExit(Collision other)
	{
		grounded = false;
		normal = Vector3.up;
	}
}
