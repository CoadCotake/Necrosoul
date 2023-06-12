using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenBookButton : MonoBehaviour
{
    public float vr;
    public float hr;
    public int P_select;
    public GameObject In_B_Select;

    [Header("타이틀")]
    [SerializeField] List<GameObject> InvenTitle = new List<GameObject>();
    public Inven_Book IB;

    [Header("내용 버튼")]
    public Button R_Button;
    public Button L_Button;

    AudioManage_Main m_Audio;

    [Header("Title 페이지 넣기")]
    public CanvasGroup In_Title_P;

    // Start is called before the first frame update
    void Start()
    {
        IB = this.gameObject.GetComponent<Inven_Book>();
        m_Audio = AudioManage_Main.instance;
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
        IB.Title[P_select].GetComponent<InvenTitle>().SetOnButtonPage();
        IB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        vr = Input.GetAxis("Vertical");
        hr = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            if (P_select == IB.ActiveButton - 1)
            {
                P_select = IB.ActiveButton - 1;
            }
            else
            {
                P_select++;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            if (P_select == 0)
            {
                P_select = 0;
            }
            else
            {
                P_select--;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && !BookButtonManger.bookButtonManger.buttoncheck)
        {
            Open_In_Cotent();
            BookButtonManger.bookButtonManger.ButtonTimerON();
            In_B_Select.SetActive(true);
            Debug.Log("엔터침!");
            m_Audio.UI_Open();
            BookButtonManger.bookButtonManger.SetIndex(2);
            BookButtonManger.bookButtonManger.BookTitle_Select = IB.Title[P_select];
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            Debug.Log("X침!");
            m_Audio.UI_Close();
            BookButtonManger.bookButtonManger.BookTitle_Select = null;
            BookButtonManger.bookButtonManger.SetIndex(0);
            IB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(false);

        }
    }

    public void Page_R()
    {
        Debug.Log("오른쪽");
        IB.R_Button();
        P_select = 0;
        m_Audio.UI_Page();
    }

    public void Page_L()
    {
        Debug.Log("왼쪽");
        IB.L_Button();
        P_select = 0;
        m_Audio.UI_Page();
    }

    public void Open_In_title()
    {
        //Ach_Content_P.SetActive(false);
        In_Title_P.alpha = 1;

    }

    public void Open_In_Cotent()
    {
        //Ach_Content_P.SetActive(true);
        In_Title_P.alpha = 0;
    }
}
