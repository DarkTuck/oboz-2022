using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileScript : MonoBehaviour
{
    // trzeba podczepić do prefabu pocisku
    // skrypt odpowiadający za przesunięce zrespawnowany bullet a potem za jego zniszczenie
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
        // Vector3.forward * -1 = Vector3 do tyłu
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
            GM.instance.destroyAsteroids(other);
            UIMG.instance.UpdatePoints();
            Destroy(gameObject);
        }
    }
}
