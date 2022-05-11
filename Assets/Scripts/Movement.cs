using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    // podczepiamy pod statek
    [SerializeField] private float thrust = 7f;
    Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 9.5f;
        rb.mass = 1000f;
    }
    void Update()
    {
        StarshipMovement();
    }
    void StarshipMovement()
    {
        // skrypt przesuwania zak�adaj�cy przesuwanie mapy wzgl�dem statku
        // statek mo�e przesuwa� si� w g�r�/d�l; na boki
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0f);
        rb.AddForce(direction * thrust, ForceMode.Acceleration);
    }
}
