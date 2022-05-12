using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMG : MonoBehaviour
{
    public static UIMG instance;
    [SerializeField] TMPro.TextMeshProUGUI healthText;
    [SerializeField] TMPro.TextMeshProUGUI pointsText; 
    [SerializeField] TMPro.TextMeshProUGUI multiplierText; 
    [SerializeField] TMPro.TextMeshProUGUI comboText;
    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] Canvas endScreenCanvas;
    [SerializeField] TMPro.TextMeshProUGUI endScoreText;
    [HideInInspector] public Transform player;
    [SerializeField] TMPro.TextMeshProUGUI bulletsMagazineText;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        comboText.gameObject.SetActive(false);
        endScreenCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
    }

    public void UpdatePlayerHealth()
    {
        //pointsText.text = pointsText.text + GetComponent<StarshipLifeManager>().starshipHP.ToString();
        healthText.text = "Player HP: " + player.gameObject.GetComponent<StarshipLifeManager>().starshipHP.ToString(); 
    }
    public void UpdatePlayerMagazine(){
        bulletsMagazineText.text = "Bullets: " + GM.instance.rifleMagazine.ToString();
    }
    public void UpdateScore(float score)
    {
        pointsText.text = "Score: " + ((int)score).ToString(); 
    }

    public void UpdateMultiplier(float combo)
    {
        multiplierText.text = "Score multiplier:" + combo.ToString(); 
    }

    public void UpdateCombo(int combo)
    {
        if (combo == 0)
        {
            multiplierText.enabled = false;
        }
        else
        {
            multiplierText.text = "x" + combo.ToString(); 
            multiplierText.enabled = true;
        }
        
    }
    public void showFinalScore(){
        int score = Mathf.RoundToInt((int)GM.instance.getScore());
        endScoreText.text = "Final Points: " + GM.instance.getScore().ToString();
    }
    public void hideInGameUI() {
        gameplayCanvas.gameObject.SetActive(false);
        endScreenCanvas.gameObject.SetActive(true);
    }
    public void OpenMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
