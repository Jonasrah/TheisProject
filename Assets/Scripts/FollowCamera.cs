using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    [Range(0.1f, 1f)] public float rotateSpeed = 1f;
    public float distance = 5f;
    private float counter;
    private SphereCollider col;
    private float tolerance = 0.2f;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        col.radius = distance / 2;
<<<<<<< HEAD
        groundLevel = target.transform.position.y;
        Screen.lockCursor = true;
=======
>>>>>>> d2a8cce840341d7e3bd53e46e3b1fa6738f9f262
    }

    void LateUpdate()
    {
        float offset = distance - counter;
        Vector3 desiredPosition = target.transform.position - transform.forward * offset;
<<<<<<< HEAD
        desiredPosition +=  transform.right * Input.GetAxis("Mouse X") * rotateSpeed;
        float verticalFactor = Vector3.Dot(transform.up, Vector3.up);
        desiredPosition += transform.up * Input.GetAxis("Mouse Y") * rotateSpeed * verticalFactor;
        float heightDif = desiredPosition.y - target.transform.position.y;
=======
        desiredPosition += transform.right * Input.GetAxis("Mouse X") * rotateSpeed;
        desiredPosition += transform.up * Input.GetAxis("Mouse Y") * rotateSpeed;
>>>>>>> d2a8cce840341d7e3bd53e46e3b1fa6738f9f262
        if (offset - Mathf.Abs(desiredPosition.y) < tolerance) return;
        transform.position = desiredPosition;
        transform.LookAt(target.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 point = other.ClosestPoint(transform.position);
        float distanceToPoint = Mathf.Max(Vector3.Distance(transform.position, point), distance / 10);
        counter = (col.radius - distanceToPoint) * 2;
        Debug.DrawRay(point, Vector3.up * counter, Color.red);
    }

    private void OnTriggerExit(Collider other)
    {
        counter = 0;
    }
}
