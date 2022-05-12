using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControls : MonoBehaviour
{
    [SerializeField] float joySensitivity = 1F;
    [SerializeField] Vector3 lastMousePos;

    private void Start()
    {
        lastMousePos = Input.mousePosition;
    }
    private void Update()
    {
        Turning();
    }
    private void Turning()
    {
        Vector3 joyRot = new Vector3(0, Input.GetAxis("Joy X"), 0);
        if (Input.GetAxis("Joy X") != 0) 
        {
            transform.Rotate(joyRot * joySensitivity * Time.deltaTime);
        }
        else if (lastMousePos != Input.mousePosition)
        {
            lastMousePos = Input.mousePosition;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }
}
