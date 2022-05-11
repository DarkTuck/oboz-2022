using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipLifeManager : MonoBehaviour
{
    public int starshipHP = 100;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            TakeDamage(10);
            Destroy(collision.gameObject); // add asteroid split effect
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
        // Player died
        GM.instance.AfterPlayerDied();
        gameObject.SetActive(false);
    }
}
