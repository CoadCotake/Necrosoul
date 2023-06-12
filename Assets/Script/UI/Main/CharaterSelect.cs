using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CharaterSelect : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Background;
    public GameObject Out;
    public void OnPointerClick(PointerEventData eventData)
    {
        Out.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Background.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Background.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Background.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Out_true()
    {
        Out.SetActive(true);
    }
}
