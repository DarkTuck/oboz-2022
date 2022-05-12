using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileScript : MonoBehaviour
{
    // trzeba podczepi� do prefabu pocisku
    // skrypt odpowiadaj�cy za przesuni�ce zrespawnowany bullet a potem za jego zniszczenie
    [SerializeField] float moveSpeed = 10f;
    Rigidbody rb;
    [SerializeField] Transform hitParticlesPrefab;
    [SerializeField] Transform enemyParticlesPrefab;
    [SerializeField] Transform enemyShootParticlePrefab;
    private float timer;
    [SerializeField] private float timeUntilProjectileDestroy = 1.5f;
    [HideInInspector] public bool isPlayerProjectile = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyTimeout());
    }

    void FixedUpdate()
    {
        // Vector3.forward * -1 = Vector3 do ty�u
        rb.MovePosition(transform.position - -transform.forward * moveSpeed);
    }
    IEnumerator DestroyTimeout()
    {
        yield return new WaitForSeconds(timeUntilProjectileDestroy);
        if (isPlayerProjectile)
        {
            GM.instance.combo = 0;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // spawnowanie particli
        if (other.gameObject.tag == "Asteroid")
        {
            if (isPlayerProjectile)
            {
                GM.instance.combo += 1;
            }

            Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
            GM.instance.OnBulletHitAsteroid(other);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(enemyParticlesPrefab, transform.position, transform.rotation);
            GM.instance.OnBulletHitEnemy(other);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Instantiate(enemyShootParticlePrefab, transform.position, transform.rotation);
            GM.instance.playerTakeBulletDamage();
            Destroy(gameObject);
        }
    }
}
