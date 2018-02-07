using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public GameObject target;
    [Range(0.1f, 1f)] public float rotateSpeed = 1f;
    public float offset = 5f;
     
    void LateUpdate() {
        Vector3 desiredPosition = target.transform.position - transform.forward * offset;
        desiredPosition +=  transform.right * Input.GetAxis("Mouse X") * rotateSpeed;
        desiredPosition += transform.up * Input.GetAxis("Mouse Y") * rotateSpeed;
        if (Mathf.Abs(desiredPosition.y) > offset) return;
        transform.position = desiredPosition;
        transform.LookAt(target.transform);
    }
}
