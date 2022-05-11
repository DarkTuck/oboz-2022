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
            starshipHP -= 10;
            Destroy(collision.gameObject);
            UIMG.instance.UpdatePlayerHealth();
            Debug.Log(starshipHP);
        }
    }
}
