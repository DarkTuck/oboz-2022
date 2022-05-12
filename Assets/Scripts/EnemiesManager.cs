using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Transform enemyPrefab;
    [HideInInspector] public Rigidbody playerRb;
    [SerializeField] float distanceZ = 7F;
    [SerializeField] float minX = -8F;
    [SerializeField] float maxX = 8F;
    [SerializeField] float minY = 5F;
    [SerializeField] float maxY = 10F;
    [SerializeField] float moveSpeed = 2F;
    [SerializeField] float fireRate = 2F;
    [SerializeField] float cutoffZ = -24F;

    // Start is called before the first frame update
    void Start()
    {
        CleanUpOldEnemies();
    }

    void OnEnable()
    {
        CleanUpOldEnemies();
    }

    void CleanUpOldEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SpawnEnemy()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(x, y, distanceZ);

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(0, 180, 0);

        Transform newEnemyT = Instantiate(enemyPrefab, pos, rot);
        newEnemyT.parent = transform;

        EnemyController enemyScript = newEnemyT.gameObject.GetComponent<EnemyController>();
        enemyScript.playerRb = playerRb;
        enemyScript.moveSpeed = moveSpeed;
        enemyScript.fireRate = fireRate;
        enemyScript.cutoffZ = cutoffZ;
    }
    
    public void StopShooting()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<EnemyController>().canShoot = false;
        }
    }
}
