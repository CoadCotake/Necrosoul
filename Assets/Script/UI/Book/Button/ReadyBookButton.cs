using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyBookButton : MonoBehaviour
{
    public float vr;
    public float hr;
    public int P_select;
    public int T_select;

    [Header("타이틀")]
    [SerializeField] List<GameObject> BookTitle = new List<GameObject>();
    
    public Ready_Book RB;
    [Header("직접 넣어야 하는것")]
    public GameObject BookTitlePlace;
    public GameObject Shop_SelectPage;
    public GameObject Charic_SelectPage;

    AudioManage_Main m_Audio;
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = AudioManage_Main.instance;
        RB = this.gameObject.GetComponent<Ready_Book>();
        for(int i=0;i<BookTitlePlace.transform.childCount;i++)
        {
            BookTitle.Add(BookTitlePlace.transform.GetChild(i).gameObject);
        }

    }

    private void OnEnable()
    {
        ButtonActive();
        RB.RCB.Un_Click();
    }

    public void ButtonActive()
    {
        if (T_select == 0)      //영혼샵
        {
            Shop_SelectPage.SetActive(false);
            Charic_SelectPage.SetActive(true);
        }
        else if(T_select==1)   //캐릭터
        {
            Shop_SelectPage.SetActive(true);
            Charic_SelectPage.SetActive(false);
        }
        else
        {
            Shop_SelectPage.SetActive(true);
            Charic_SelectPage.SetActive(false);
        }
    }

    public void ReadybookCheck()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BookButtonManger.bookButtonManger.GetIndex() == 1)
        {
            ButtonMange();
        }
        else if (BookButtonManger.bookButtonManger.GetIndex() == 2 && T_select==0)  //영혼상점
        {
            ButtonMange1();
        }
        else if (BookButtonManger.bookButtonManger.GetIndex() == 2 && T_select == 1)  //캐릭터 선택 및 시작
        {
            ButtonMange2();
        }

    }
    public void ButtonMange()
    {
        BookTitle[T_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            T_select = 1;
            ButtonActive();
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            T_select = 0;
            ButtonActive();
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
           
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
            
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && BookButtonManger.bookButtonManger.buttoncheck == false)
        {
            Debug.Log("엔터침!");
            BookButtonManger.bookButtonManger.ButtonTimerON();
            BookButtonManger.bookButtonManger.BookTitle_Select = BookTitle[T_select];
            ReadybookCheck();
            if (T_select == 1)
            {
                RB.RCB.In_Click();
            }
            BookButtonManger.bookButtonManger.SetIndex(2);
            m_Audio.UI_Open();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            Debug.Log("X침!");
            BookButtonManger.bookButtonManger.BookTitle_Select = null;
            BookButtonManger.bookButtonManger.SetIndex(0);
            m_Audio.UI_Cancle();
        }
    }
    public void ButtonMange1()  //영혼샵
    {
        RB.Shop[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {

        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {

        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
            Debug.Log("위");
            if (P_select <= 0)
            {
                P_select = 3;
            }
            else
            {
                P_select = P_select - 1;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
            Debug.Log("아래");
            if (P_select >= 3)
            {
                P_select = 0;
            }
            else
            {
                P_select = P_select + 1;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && BookButtonManger.bookButtonManger.buttoncheck == false)
        {
            Debug.Log("엔터침!");
            BookButtonManger.bookButtonManger.ButtonTimerON();
            //BookButtonManger.bookButtonManger.SetIndex(3);
            m_Audio.UI_Open();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            Debug.Log("X침!");
            RB.Shop[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            BookButtonManger.bookButtonManger.SetIndex(1);
            m_Audio.UI_Cancle();
        }
    }

    public void ButtonMange2()
    {
       
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {

        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {

        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
            
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
           
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && BookButtonManger.bookButtonManger.buttoncheck == false)
        {
            Debug.Log("엔터침!");
            BookButtonManger.bookButtonManger.ButtonTimerON();
            BookButtonManger.bookButtonManger.SetIndex(3);
            AudioManage_BGM.instance.Stage1();
            LoadingSceneManager.l_scenemanager.LoadStage("stage1");
            m_Audio.UI_Open();
            m_Audio.Game_Enter();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            Debug.Log("X침!");
            BookButtonManger.bookButtonManger.SetIndex(1);
            RB.RCB.Un_Click();
            m_Audio.UI_Cancle();
        }
    }
}
