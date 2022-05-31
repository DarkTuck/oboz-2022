using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] Vector3 sizeHover = new Vector3(1F, 1F, 1F);
    [SerializeField] Vector3 sizeIdle = new Vector3(0.9F, 0.9F, 0.9F); 
    RectTransform m_RectT;
    // Start is called before the first frame update
    void Start()
    {
        m_RectT = GetComponent<RectTransform>();
        MouseOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseOn()
    {
        m_RectT.localScale = sizeHover;
    }

    public void MouseOff()
    {
        m_RectT.localScale = sizeIdle;
    }
}
