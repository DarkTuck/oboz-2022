using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectorMgr : MonoBehaviour
{
    [SerializeField] Transform[] shipPrefabs;
    [SerializeField] string[] shipNames;
    [SerializeField] float rotSpeed = 80F;
    [SerializeField] Text shipNameText;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplayedShip();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }

    void SwitchShip(bool isRightClick) // right - true, left - false
    {
        int nextIndex = 0;
        if (isRightClick) //right
        {
            nextIndex = PlayerPrefs.GetInt("SelectedShip", 0) + 1;
        }
        else //left
        {
            nextIndex = PlayerPrefs.GetInt("SelectedShip", 0) - 1;
        }

        if (nextIndex >= shipPrefabs.Length)
        {
            nextIndex = 0;
        }

        if (nextIndex < 0)
        {
            nextIndex = shipPrefabs.Length - 1;
        }

        PlayerPrefs.SetInt("SelectedShip", nextIndex);

        UpdateDisplayedShip();
    }

    void UpdateDisplayedShip()
    {
        int index = PlayerPrefs.GetInt("SelectedShip", 0);
        transform.GetChild(0).gameObject.GetComponent<MeshFilter>().sharedMesh = shipPrefabs[index].GetChild(0).gameObject.GetComponent<MeshFilter>().sharedMesh;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterials = shipPrefabs[index].GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterials;
        shipNameText.text =  shipNames[index];
    }
    public void RightClicked()  
    {
        SwitchShip(true);
    }

    public void LeftClicked()
    {
        SwitchShip(false);
    }
}
