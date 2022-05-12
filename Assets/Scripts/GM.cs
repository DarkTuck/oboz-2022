using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    // GameManager
    // trzeba stworzy� empty object na mapie kt�ry b�dzie GMem i podpi�� ten skrypt tam
    // linijki tworz�ce singleton
    public static GM instance;
    StarshipLifeManager playerLifeMgr;
    [SerializeField] Transform[] shipVariants;
    [SerializeField] Transform shipSpawnT;
    [SerializeField] CameraFollow m_CameraFollow;
    [SerializeField] private float enemySpawnRate = 2F;
    [SerializeField] private EnemiesManager m_EnemiesManager;
    [SerializeField] int enemyDealDamage = 10;
    public int rifleMagazine = 30;
    float score = 0;
    float scoreMultiplier = 1;
    public int combo = 0;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedShip", 0);
        Transform playerT = Instantiate(shipVariants[index], shipSpawnT.position, shipSpawnT.rotation);
        playerLifeMgr = playerT.gameObject.GetComponent<StarshipLifeManager>();
        m_EnemiesManager.playerRb = playerT.gameObject.GetComponent<Rigidbody>();
        m_CameraFollow.target = playerT;
        UIMG.instance.player = playerT;

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

    public void EnemiesIncreaser()
    {
        if (score >= 500 && score <= 1600)
            enemySpawnRate = 1F;
        else if (score >= 1600 && score <= 3000)
            enemySpawnRate = 0.5F;
        else
            enemySpawnRate = 0.3F;
    }

    public float getScore() {  
        return score;
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
            //m_EnemiesManager.SpawnEnemy();
            if (m_EnemiesManager.gameObject.transform.childCount == 0)
            {
                m_EnemiesManager.SpawnEnemy();
            }

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
