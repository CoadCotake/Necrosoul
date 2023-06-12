using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchBookButton : MonoBehaviour
{
    public float vr;
    public float hr;
    public int P_select;

    [Header("Title 페이지 넣기")]
    public GameObject Ach_Title_P;
    //public GameObject Ach_Content_P;

    [Header("타이틀")]
    [SerializeField] List<GameObject> BookTitle = new List<GameObject>();
    public Ach_Book AB;

    [Header("내용 버튼")]
    public Button R_Button;
    public Button L_Button;

    AudioManage_Main m_Audio;

    // Start is called before the first frame update
    void Start()
    {
        m_Audio = AudioManage_Main.instance;
        AB = this.gameObject.GetComponent<Ach_Book>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (BookButtonManger.bookButtonManger.GetIndex() == 1)
        {
            ButtonMange();
        }
        else if(BookButtonManger.bookButtonManger.GetIndex() == 2)
        {
            ButtonMange2();
        }
    }
    public void ButtonMange()
    {
        AB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);

        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            Debug.Log("오른쪽");
            //AB.R_Button();
            //P_select = 0;
            m_Audio.UI_Page();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            Debug.Log("왼쪽");
            //AB.L_Button();
            //P_select = 0;
            m_Audio.UI_Page();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
            Debug.Log("위");
            if (P_select == 0)
            {
                P_select = AB.ActiveButton - 1;
            }
            else
            {
                P_select--;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
            Debug.Log("아래");
            if (P_select == AB.ActiveButton - 1)
            {
                P_select = 0;
            }
            else
            {
                P_select++;
            }
            m_Audio.UI_Chose();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && BookButtonManger.bookButtonManger.buttoncheck == false)
        {
            Debug.Log("엔터침!");
            Open_Ach_Cotent();
            BookButtonManger.bookButtonManger.BookTitle_Select = AB.Title[P_select];
            BookButtonManger.bookButtonManger.SetIndex(2);
            m_Audio.UI_Open();

        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            Debug.Log("X침!");
            BookButtonManger.bookButtonManger.BookMark_Select = null;
            BookButtonManger.bookButtonManger.SetIndex(0);
            AB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            m_Audio.UI_Close();
        }
    }

    public void ButtonMange2()
    {
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            Debug.Log("오른쪽");
            R_Button.onClick.Invoke();
            m_Audio.UI_Page();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            Debug.Log("왼쪽");
            L_Button.onClick.Invoke();
            m_Audio.UI_Page();
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            Open_Ach_Title();
            Debug.Log("X침!");
            BookButtonManger.bookButtonManger.BookTitle_Select = null;
            BookButtonManger.bookButtonManger.SetIndex(1);
            m_Audio.UI_Close();
        }
    }


    public void Open_Ach_Title()
    {
        //Ach_Content_P.SetActive(false);
        Ach_Title_P.SetActive(true);

    }
    
    public void Open_Ach_Cotent()
    {
        //Ach_Content_P.SetActive(true);
        Ach_Title_P.SetActive(false);
    }
}
