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
    Rigidbody thisRb;
    void Start()
    {
        thisRb = GetComponent<Rigidbody>();
        StartCoroutine(ShootLoop());
    }

    IEnumerator ShootLoop()
    {
        while (canShoot)
        {
            Shoot();
            yield return new WaitForSeconds(1 / fireRate);
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDir = playerRb.transform.position - transform.position;
        moveDir = new Vector3(moveDir.x, moveDir.y + 1, 0);
        thisRb.MovePosition(transform.position + moveDir * moveSpeed* Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Vector3 pos = transform.position + transform.forward;
        Instantiate(projectilePrefab, pos, transform.rotation);
    }
}
