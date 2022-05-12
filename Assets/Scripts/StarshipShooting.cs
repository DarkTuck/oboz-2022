using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipShooting : MonoBehaviour
{
    // Skrypt podczepiamy pod statek
    // Odpowiada on za wystrzelenie pocisku
    [SerializeField] Transform projectilePrefab;
    [SerializeField] float shootSpeed = 1f;
    [SerializeField] Transform shootPosition;
    private int baseMagazineVal;
    [SerializeField] float reloadTime = 2F;
    bool canShoot = true;

    private void Start()
    {
        baseMagazineVal = GM.instance.rifleMagazine;
    }
    void Update()
    {
        Shoot();
    }
    // zmienne isGame, isShooting mog� przyda� si� w p�niejszych zmianach skryptu
    void Shoot()
    {
        if (Input.GetButton("Fire1")){
            if (canShoot)
            {
                SpawnBullet();
                StartCoroutine(ShootDelay());
                UIMG.instance.UpdatePlayerMagazine();
            }
        }
    }
    // szybka funkcja do stworzenia pocisku
    void SpawnBullet(){
        Instantiate(projectilePrefab, shootPosition.position, transform.rotation);
    }
    // korutyna odpowiadaj�ca za strike rate pocisk�w
    private IEnumerator ShootDelay(){
        canShoot = false;
        if(GM.instance.rifleMagazine <= 0)
        {
            GM.instance.rifleMagazine = baseMagazineVal;
            yield return new WaitForSeconds(reloadTime);
            canShoot = true;
        }else
        {
            GM.instance.rifleMagazine--;
            yield return new WaitForSeconds(1 / shootSpeed);
            canShoot = true;
        }
    }
}
