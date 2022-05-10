using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    // GameManager
    // trzeba stworzy� empty object na mapie kt�ry b�dzie GMem i podpi�� ten skrypt tam
    // linijki tworz�ce singleton
    public static GM instance;
    void Awake()
    {
        instance = this;
    }
    //Skrypt do niszczenia asteroid
    public void destroyAsteroids(Collider other)
    {
        // Asteroid to skrypt kt�ry jest przyczepiony to prefabu asteroidu, skrypt asteroidy musi mie� w sobie: public int hp
        other.GetComponent<AsteroidScript>().hp -= 1;
        if (other.gameObject.GetComponent<AsteroidScript>().hp <= 0){
            Destroy(other.gameObject);
            Debug.Log("Destroyed asteroid");
        }
    }
}
