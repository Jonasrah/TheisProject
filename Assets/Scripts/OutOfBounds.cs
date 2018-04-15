using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

	private ResetPosition rp;
	
	private void Start() {
		rp = FindObjectOfType<ResetPosition>();
	}

	private void OnTriggerEnter(Collider other) {
		rp.e_OutOfBounds.Invoke();
	}
}
