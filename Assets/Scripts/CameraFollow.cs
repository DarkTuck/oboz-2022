using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // podczepiamy pod main kamer�
    [HideInInspector] public Transform target;
    [SerializeField] private float smoothSpeed = 10f;
    // polecam domy�lny offset 0;5.47;-10
    [SerializeField] private Vector3 offset;
    private void FixedUpdate()
    {
        // to odpowiada za wysmuklenie przesuni�cia kamery
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // trzeba to naprawi� bo transform.position nie respektuje collider�w, wi�c mo�na patrze� przez sufit
        // trzeba u�y� transform.Translate albo RigidBody.MovePosition
        transform.position = smoothedPosition;
    }
}
