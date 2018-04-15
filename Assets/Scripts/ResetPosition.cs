using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetPosition : MonoBehaviour {
	public UnityEvent e_OutOfBounds;

	public Transform spawn;

	public Transform player;
	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if (spawn == null) {
			Debug.Log("No spawntransform");
			return;
		}
		
		e_OutOfBounds = new UnityEvent();
		e_OutOfBounds.AddListener(AllocatePosition);
	}
	

	void AllocatePosition () {
		player.position = spawn.position;
	}
}
