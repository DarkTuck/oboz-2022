using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipLifeManager : MonoBehaviour
{
    public int starshipHP = 100;
    [SerializeField] private GameObject deathParticles;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            TakeDamage(10);
            Destroy(collision.gameObject); // add asteroid split effect
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(15);
            collision.gameObject.GetComponent<EnemyController>().OnCrashed();
        }
    }

    public void TakeDamage(int dmg)
    {
        starshipHP -= dmg;
        UIMG.instance.UpdatePlayerHealth();

        if (starshipHP <= 0)
        {
            OnPlayerDied();
        }
    }

    void OnPlayerDied()
    {
        GM.instance.AfterPlayerDied();
        UIMG.instance.hideInGameUI();
        Instantiate(deathParticles, transform.position, transform.rotation);
        gameObject.SetActive(false);
        UIMG.instance.showFinalScore();
    }
}
