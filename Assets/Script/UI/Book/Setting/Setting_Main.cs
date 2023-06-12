using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Main : MonoBehaviour
{
    public GameObject SelectWindow;
    public Animator SelectAni;
    // Start is called before the first frame update
    void Start()
    {
        //SelectWindow = this.gameObject;
        SelectWindow.SetActive(false);
    }


    public void SelectOn()
    {
        Debug.Log("열려라 문이여");
        SelectWindow.SetActive(true);
    }
    public void SelectOff()
    {
        SelectAni.SetBool("On", true);
    }

    public void AniOn()
    {
        SelectAni.SetBool("On", false);
    }
    public void AniOff()
    {
        SelectWindow.SetActive(false);

    }
}
