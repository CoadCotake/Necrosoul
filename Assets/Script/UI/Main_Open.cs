using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Open : MonoBehaviour
{
    public GameObject Panel;
    public Animator ani;

    private void Awake()
    {
        Time.timeScale = 1;
        Debug.Log("스타트");
        //ani.SetBool("Check", false);
    }
    public void Openpanel()
    {
        Debug.Log("세이브 버튼 클릭");
       // ani.SetBool("Check", true);
    }
}
