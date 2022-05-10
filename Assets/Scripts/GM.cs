using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    // GameManager
    // trzeba stworzyæ empty object na mapie który bêdzie GMem i podpi¹æ ten skrypt tam
    // linijki tworz¹ce singleton
    public static GM instance;
    void Awake()
    {
        instance = this;
    }
    //Skrypt do niszczenia asteroid
    public void destroyAsteroids(Collider other)
    {
        // Asteroid to skrypt który jest przyczepiony to prefabu asteroidu, skrypt asteroidy musi mieæ w sobie: public int hp
        other.GetComponent<AsteroidScript>().hp -= 1;
        if (other.gameObject.GetComponent<AsteroidScript>().hp <= 0){
            Destroy(other.gameObject);
            Debug.Log("Destroyed asteroid");
        }
    }
}
