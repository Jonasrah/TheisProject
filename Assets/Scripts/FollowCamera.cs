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
    private float tolerance = 0.2f;
    private Vector3 clippingPoint;
    public LayerMask mask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update() {
        counter = 0;
        
        RaycastHit hit;
        Ray cameraPole = new Ray(target.transform.position, transform.position - target.transform.position);
        Debug.DrawRay(cameraPole.origin, cameraPole.direction, Color.red, 0.1f);
        if (Physics.Raycast(cameraPole, out hit, distance, mask)) {
            counter = distance - hit.distance;
        }
        
        
    }

    void LateUpdate() {
        
        
        float offset = distance - counter;
        Vector3 desiredPosition = target.transform.position - transform.forward * offset;
        desiredPosition +=  transform.right * Input.GetAxis("Mouse X") * rotateSpeed;
        float verticalFactor = 1 - counter / distance;
        desiredPosition += transform.up * Input.GetAxis("Mouse Y") * rotateSpeed * verticalFactor;
        
        
        transform.position = desiredPosition;
        transform.LookAt(target.transform);
    }

    private float CurvedDistance(float c) {
        float multiplier = 2;
        return Mathf.Pow(distance - c, multiplier) / distance;
    }

    
    
}
