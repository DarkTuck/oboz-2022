using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMG : MonoBehaviour
{
    public static UIMG instance;
    [SerializeField] TMPro.TextMeshProUGUI healthText;
    [SerializeField] TMPro.TextMeshProUGUI pointsText; 
    [SerializeField] Transform player;
    public int playerPoints = 0;
    private void Awake()
    {
        instance = this;
    }
    public void UpdatePlayerHealth()
    {
        //pointsText.text = pointsText.text + GetComponent<StarshipLifeManager>().starshipHP.ToString();
        healthText.text = "Player HP: " + player.gameObject.GetComponent<StarshipLifeManager>().starshipHP.ToString(); 
        Debug.Log(healthText.text);
    }
    public void AddPoint()
    {
        ++playerPoints;
    }
    public void UpdatePoints()
    {
        pointsText.text = "Points: " + playerPoints.ToString(); 
    }
}
