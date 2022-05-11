using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    // GameManager
    // trzeba stworzy� empty object na mapie kt�ry b�dzie GMem i podpi�� ten skrypt tam
    // linijki tworz�ce singleton
    public static GM instance;
    [SerializeField] StarshipLifeManager playerLifeMgr;
    [SerializeField] private float enemySpawnRate = 2F;
    [SerializeField] private EnemiesManager m_EnemiesManager;
    [SerializeField] int enemyDealDamage = 10;
    float score = 0;
    float scoreMultiplier = 1;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(EnemySpawner()); 
        UIMG.instance.UpdateScore(score);
        UIMG.instance.UpdateMultiplier(scoreMultiplier);
    }
    //Skrypt do niszczenia asteroid
    public void OnBulletHitAsteroid(Collider other)
    {
        // Asteroid to skrypt kt�ry jest przyczepiony to prefabu asteroidu, skrypt asteroidy musi mie� w sobie: public int hp
        other.GetComponent<AsteroidScript>().hp -= 1;
        if (other.gameObject.GetComponent<AsteroidScript>().hp <= 0){
            Destroy(other.gameObject);

            scoreMultiplier += 0.1F;
            UIMG.instance.UpdateMultiplier(scoreMultiplier);
        }
    }
    public void Moved1Meter()
    {
        score += 1F * scoreMultiplier;
        UIMG.instance.UpdateScore(score);
    }    

    public void AsteroidEscaped()
    {
        scoreMultiplier = 1F;
        UIMG.instance.UpdateMultiplier(scoreMultiplier);
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            m_EnemiesManager.SpawnEnemy();
            yield return new WaitForSeconds(1 / enemySpawnRate);
        }
    }
    public void playerTakeBulletDamage()
    {
        playerLifeMgr.TakeDamage(enemyDealDamage);
    }
    public void OnBulletHitEnemy(Collider other)
    {
        other.GetComponent<EnemyController>().hp -= 1;
        if(other.GetComponent<EnemyController>().hp <= 0)
        {
            Destroy(other.gameObject);
            score += 2 * scoreMultiplier;

            UIMG.instance.UpdateScore(score);
        }
    }
    public void AfterPlayerDied()
    {
        m_EnemiesManager.StopShooting();
    }
}
