using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    public Animator anim;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public void StartAnimation()
    {
        Debug.Log("애니메이션 시작");
        anim.SetBool("Start", false);
        anim.SetBool("Start", true);
    }
    
    public void End()
    {
        Debug.Log("애니메이션 종료");
        anim.SetBool("Start", false);
        this.gameObject.SetActive(false);
    }
}