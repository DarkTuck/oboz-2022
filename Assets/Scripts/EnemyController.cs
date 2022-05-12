using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 3;
    public float moveSpeed = 0.8F;
    public float fireRate = 2F;
    public float cutoffZ = -24F;
    public bool canShoot = true;
    [HideInInspector] public Rigidbody playerRb;
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform destructionParticles;
    [SerializeField] float timeToKamikaze = 8F;
    [SerializeField] float kamikazeSpeed = 8F;
    Rigidbody thisRb;
    int secPassed = 0;
    bool isKamikaze = false;
    void Start()
    {
        thisRb = GetComponent<Rigidbody>();
        StartCoroutine(ShootLoop());
        StartCoroutine(CountSeconds());
    }

    public void OnCrashed()
    {
        Instantiate(destructionParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator ShootLoop()
    {
        while (canShoot)
        {
            Shoot();
            yield return new WaitForSeconds(1 / fireRate);
        }
    }

    IEnumerator CountSeconds()
    {
        while (secPassed < timeToKamikaze)
        {
            secPassed++;
            yield return new WaitForSeconds(1);
        }

        StopCoroutine(ShootLoop());
        isKamikaze = true;
    }
    
    void FixedUpdate()
    {
        Vector3 moveDir = playerRb.transform.position - transform.position;
        float speed = kamikazeSpeed; // often overriten in next if

        if (!isKamikaze)
        {
            moveDir = new Vector3(moveDir.x, moveDir.y , 0);
            speed = moveSpeed;
        }
        
        thisRb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);

        if (thisRb.transform.position.z < -8F)
        {
            OnCrashed();
        }
    }

    void Shoot()
    {
        Vector3 pos = transform.position + transform.forward;
        Transform bulletT = Instantiate(projectilePrefab, pos, transform.rotation);
        bulletT.gameObject.GetComponent<ProjectileScript>().isPlayerProjectile = false;
    }
}
