using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileScript : MonoBehaviour
{
    // trzeba podczepiæ do prefabu pocisku
    // skrypt odpowiadaj¹cy za przesuniêce zrespawnowany bullet a potem za jego zniszczenie
    [SerializeField] float moveSpeed = 10f;
    Rigidbody rb;
    [SerializeField] Transform hitParticlesPrefab;
    private float timer;
    [SerializeField] private float timeUntilProjectileDestroy = 1.5f;
    void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Vector3.forward * -1 = Vector3 do ty³u
        rb.MovePosition(transform.position - -transform.forward * moveSpeed);
    }
    private void Update()
    {
        // timer liczy czas do zniszczenia
        timer += Time.deltaTime;
        if (timer >= timeUntilProjectileDestroy)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // spawnowanie particli
        if (other.gameObject.tag == "Asteroid")
        {
            Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
            Debug.Log("Hit asteroid");
            GM.instance.destroyAsteroids(other);
            Destroy(gameObject);
        }
    }
}
