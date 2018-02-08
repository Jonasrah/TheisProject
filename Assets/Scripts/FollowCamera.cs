using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public GameObject target;
    [Range(0.1f, 1f)] public float rotateSpeed = 1f;
    public float distance = 5f;
    private float counter;
    private SphereCollider col;
    private float tolerance = 0.2f;
    private float groundLevel;

    private void Start() {
        col = GetComponent<SphereCollider>();
        col.radius = distance / 2;
        groundLevel = target.transform.position.y;
    }

    void LateUpdate() {
        float offset = distance - counter;
        Vector3 desiredPosition = target.transform.position - transform.forward * offset;
        desiredPosition +=  transform.right * Input.GetAxis("Mouse X") * rotateSpeed;
        desiredPosition += transform.up * Input.GetAxis("Mouse Y") * rotateSpeed;
        float heightDif = desiredPosition.y - target.transform.position.y;
        if (offset - Mathf.Abs(desiredPosition.y) < tolerance) return;
        transform.position = desiredPosition;
        //transform.position += Vector3.up * (target.transform.position.y - groundLevel);
        transform.LookAt(target.transform);
    }

    private void OnTriggerStay(Collider other) {
        Vector3 point = other.ClosestPoint(transform.position);
        float distanceToPoint = Mathf.Max(Vector3.Distance(transform.position, point), distance / 10);
        counter = (col.radius - distanceToPoint) * 2;
        Debug.DrawRay(point, Vector3.up * counter, Color.red);
    }

    private void OnTriggerExit(Collider other) {
        counter = 0;
    }
}
