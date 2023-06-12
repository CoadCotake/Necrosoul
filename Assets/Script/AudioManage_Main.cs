using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManage_Main : MonoBehaviour
{
    // Make it a singleton class that can be accessible everywhere
    public static AudioManage_Main instance;
    public AudioMixerGroup SFX_mixer;
    
    [SerializeField]
    Sound[] m_sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one AudioManger in scene");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        DonDestoryManage.DDM.DDM_List.Add(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        for(int i = 0; i < m_sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + m_sounds[i].m_name);
            go.transform.SetParent(transform);
            m_sounds[i].SetSource(go.AddComponent<AudioSource>());
            m_sounds[i].m_source.outputAudioMixerGroup = SFX_mixer;
        }
    }

    public void PlaySound (string name)
    {
        for(int i = 0; i < m_sounds.Length; i++)
        {
            if(m_sounds[i].m_name == name)
            {
                Debug.Log("사운듣 플레이 중");
                m_sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("AudioManager: Sound name not found in list: " + name);
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name && m_sounds[i].IsPlaying())
            {
                m_sounds[i].Stop();
                return;
            }
        }
    }

    public bool IsPlaying(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name && m_sounds[i].IsPlaying())
            {
                return true;
            }
        }

        return false;
    }




    //------------사운드 설정

    //Main Title 사운드
    public void UI_Main_Knife()
    {
        PlaySound("knife_0");
        PlaySound("knife_1");
    }

    public void UI_Main_WindowOut()
    {
        PlaySound("WindowOut");
    }

    public void UI_Main_Logo()
    {
        PlaySound("Logo");
    }

    //UI 사운드

    public void UI_Move()
    {
        PlaySound("");
    }

    public void UI_Click()      //단순 클릭
    {
        PlaySound("Button_Click");
    }

    public void UI_Chose()      // 버튼 선택
    {
        PlaySound("Button_Choose");
    }

    public void UI_Cancle()     //취소 버튼
    {
        PlaySound("Button_Click");
        PlaySound("Button_Cancel");
    }

    public void UI_Select()     // 확인 버튼
    {
        PlaySound("Button_Click");
        PlaySound("Button_OK");
    }

    public void UI_Page()   //페이지 넘김
    {
        PlaySound("Page");
    }

    public void UI_Open()   // 페이지 열기
    {
        PlaySound("Button_Click");
        PlaySound("Button_OK");
        PlaySound("UI_Open");
    }

    public void UI_Close()  // 페이지 닫기
    {
        PlaySound("Button_Click");
        PlaySound("Button_Cancel");
        PlaySound("UI_Close");
    }

    public void UI_Erorr()      //에러 났을 경우
    {
        PlaySound("Error");
    }

    public void UI_Scroll()     //스크롤 소리
    {
        PlaySound("UI_Scroll");
    }

    public void Game_Enter()        //게임 입장
    {
        Debug.Log("게임 입장이 실행 되었어요~~~~~~~~~~~~~~~~~~~~");
        PlaySound("Game_Enter");
    }

    // ---------- 플레이어
    public void Dash()      //대쉬
    {
        PlaySound("Dash");
    }

    public void attacked()  //피격
    {
        PlaySound("attacked");
    }
    
    public void teleport()  //아이템 - 텔레포트
    {
        PlaySound("teleport");
    }

    public void purchase()  //상점 구입
    {
        PlaySound("purchase");
    }

    public void dropItem()  // 아이템 드랍
    {
        PlaySound("dropItem");
    }

    public void takeItem()  //아이템 획득
    {
        PlaySound("takeItem");
    }

    public void prtalOpen() //포털 열리는 소리
    {
        PlaySound("prtalOpen");
    }

    public void portalEnter()   //포탈 들어가는 소리
    {
        PlaySound("portalEnter");
    }

    public void portalExit()    //포탈 나가는 소리
    {
        PlaySound("portalExit");
    }




    // --------적 사운드

    //적 공통된 소리
    public void boom()
    {
        PlaySound("boom");
    }

    //여러 적 겹치는 소리들
    public void Charge_And_Bee_Attack()
    {
        PlaySound("Charge_And_Bee_Attack");
    }


    //적 - 양
    public void Charge_Enemy_footsteps()
    {
        PlaySound("Charge_Enemy_footsteps");
    }


    //적 - 해골
    public void Skull_Throw()
    {
        PlaySound("Skull_Throw");
    }



}
