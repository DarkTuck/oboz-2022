using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // podczepiamy pod main kamerê
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 10f;
    // polecam domyœlny offset 0;5.47;-10
    [SerializeField] private Vector3 offset;
    private void FixedUpdate()
    {
        // to odpowiada za wysmuklenie przesuniêcia kamery
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // trzeba to naprawiæ bo transform.position nie respektuje colliderów, wiêc mo¿na patrzeæ przez sufit
        // trzeba u¿yæ transform.Translate albo RigidBody.MovePosition
        transform.position = smoothedPosition;
    }
}
