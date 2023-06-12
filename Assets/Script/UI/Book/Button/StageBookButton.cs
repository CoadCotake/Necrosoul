using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBookButton : MonoBehaviour
{
    public float vr;
    public float hr;
    public int P_select;

    [Header("타이틀")]
    [SerializeField] List<GameObject> BookTitle = new List<GameObject>();
    public Stage_Book SB;
    public GameObject SelectPage;

    AudioManage_Main m_Audio;
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = AudioManage_Main.instance;
        SB = this.gameObject.GetComponent<Stage_Book>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BookButtonManger.bookButtonManger.GetIndex() == 1)
        {
            ButtonMange();
        }

    }
    public void ButtonMange()
    {
        SelectPage.SetActive(false);
        SB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            if (P_select == 1 || P_select == SB.ActiveButton - 1)    //끝에서 버튼을 누른다면
            {
                SB.R_Button();
                P_select = 0;
                m_Audio.UI_Page();
            }
            else
            {
                P_select++;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            Debug.Log("왼쪽");
            if (P_select == 0 || P_select == 2)
            {
                SB.L_Button();
                P_select = 0;
                m_Audio.UI_Page();
            }
            else
            {
                P_select--;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
            Debug.Log("위");
            if (P_select - 2 < 0)
            {
                P_select = P_select + 2;
            }
            else
            {
                P_select = P_select - 2;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
            Debug.Log("아래");
            if (P_select + 2 >= 4)
            {
                P_select = P_select - 2;
            }
            else
            {
                P_select = P_select + 2;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && BookButtonManger.bookButtonManger.buttoncheck == false)
        {
            Debug.Log("엔터침!");
            BookButtonManger.bookButtonManger.ButtonTimerON();
            BookButtonManger.bookButtonManger.BookTitle_Select = SB.Title[P_select];
            BookButtonManger.bookButtonManger.SetIndex(1);
            SB.Title[P_select].GetComponent<StageContent>().StageButtonON();
            m_Audio.UI_Open();
            m_Audio.Game_Enter();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            SelectPage.SetActive(false);
            Debug.Log("X침!");
            BookButtonManger.bookButtonManger.SetIndex(0);
            m_Audio.UI_Cancle();
        }
    }


}
