using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBack : MonoBehaviour {
	private bool wheelOn;

	private Rigidbody rb;

	private Vector3 startPosition;

	private bool rollSequenceStarted;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		startPosition = transform.position;
	}
	

	private void OnCollisionStay(Collision other) {
		if (!other.gameObject.CompareTag("Player")) return;
		wheelOn = true;
		Debug.Log("Wheel is on");
	}

	private void OnCollisionExit(Collision other) {
		if (!other.gameObject.CompareTag("Player")) return;
		wheelOn = false;
		Debug.Log("Wheel off! Initiating Roll Sequence");
		StartCoroutine(WaitAndRoll());
	}

	IEnumerator WaitAndRoll() {
		if (rollSequenceStarted) yield break;
		rollSequenceStarted = true;
		Debug.Log("Waiting");
		bool timeIsUp = false;
		float time = 0;
		float distanceToStart = Vector3.Distance(rb.position, startPosition);
		while (distanceToStart > 1f) {
			if (wheelOn) {
				Debug.Log("Wheel Back On");
				break;
			}

			time += Time.deltaTime;
			timeIsUp = time >= 2f;
			
			if (timeIsUp) {
				yield return new WaitForFixedUpdate();
				Debug.Log("Rolling Back " + time);
				rb.AddTorque(Vector3.right, ForceMode.Acceleration);
				distanceToStart = Vector3.Distance(rb.position, startPosition);	
			}

			yield return null;
		}

		rollSequenceStarted = false;
		yield return null;

	}
}
