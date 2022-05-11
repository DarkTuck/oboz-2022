using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMG : MonoBehaviour
{
    public static UIMG instance;
    [SerializeField] TMPro.TextMeshProUGUI healthText;
    [SerializeField] TMPro.TextMeshProUGUI pointsText; 
    [SerializeField] TMPro.TextMeshProUGUI multiplierText; 
    [SerializeField] Transform player;
    private void Awake()
    {
        instance = this;
    }
    public void UpdatePlayerHealth()
    {
        //pointsText.text = pointsText.text + GetComponent<StarshipLifeManager>().starshipHP.ToString();
        healthText.text = "Player HP: " + player.gameObject.GetComponent<StarshipLifeManager>().starshipHP.ToString(); 
    }
    public void UpdateScore(float score)
    {
        pointsText.text = "Score: " + ((int)score).ToString(); 
    }

    public void UpdateMultiplier(float combo)
    {
        multiplierText.text = "Score multiplier:" + combo.ToString(); 
    }
    
}
