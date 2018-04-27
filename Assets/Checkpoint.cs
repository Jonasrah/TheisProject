using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	private ResetPosition _resetPosition;
	// Use this for initialization
	void Start () {
		_resetPosition = FindObjectOfType<ResetPosition>();
	}
	

	private void OnTriggerEnter(Collider other) {
		if (!other.gameObject.CompareTag("Player")) return;
		Debug.Log("New Spawn Point");
		_resetPosition.spawn = transform;
	}
}
