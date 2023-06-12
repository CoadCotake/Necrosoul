using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class StartAnimation : MonoBehaviour, IPointerClickHandler
{
    Animator StartAni;
    static public bool First=true;
    static public bool OpeningEnd;
    AudioManage_Main m_audio;

    public AudioManage_BGM m_b_audio;
    private void Awake()
    {
        m_audio = AudioManage_Main.instance;
        m_b_audio = AudioManage_BGM.instance;
        StartAni = this.GetComponent<Animator>();
        if(!First)
        {
            StartAni.SetBool("First", false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {
            StartAni.SetBool("Click", true);
        }
    }
    public void Frist()
    {
        StartAni.SetBool("First", false);
        First = false;
    }
    public void knifeSound()
    {
        m_audio.UI_Main_Knife();
    }

    public void WindowOut()
    {
        m_audio.UI_Main_WindowOut();
    }

    public void Logo()
    {
        m_audio.UI_Main_Logo();
    }
    public void End()
    {
        OpeningEnd = true;
        this.gameObject.SetActive(false);
        m_b_audio.Main_Select_BGM();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //StartAni.SetBool("Click", true);
    }
    
    

}
