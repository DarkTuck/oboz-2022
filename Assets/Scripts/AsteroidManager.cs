using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] Transform asteroidPrefab;
    [SerializeField] float speed = 1F;
    [SerializeField] float distanceZ = 60F;
    [SerializeField] int asteroidHp = 2;
    [SerializeField] int asteroidSize = 2;
    [SerializeField] float spawnRate = 1F;
    [SerializeField] float minX = -8F;
    [SerializeField] float maxX = 8F;
    [SerializeField] float minY = 5F;
    [SerializeField] float maxY = 10F;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AsteroidSpawner());
    }

    void SpawnAsteroid()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(x, y, distanceZ);
        Transform newObject = Instantiate(asteroidPrefab, pos, Quaternion.identity);
        newObject.GetComponent<AsteroidScript>().hp = asteroidHp;
        newObject.GetComponent<AsteroidScript>().moveSpeed = speed;
        newObject.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
        newObject.parent = transform;
    }

    IEnumerator AsteroidSpawner()
    {
        while (true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(1F / spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
