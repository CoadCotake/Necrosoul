using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookMarkButton : MonoBehaviour
{
    
    public int P_select;

    [Header("북마크")]
    [SerializeField] List<GameObject> BookMark = new List<GameObject>();
    [SerializeField] public GameObject BookMark_Select;
    public BookMange BM;
    public AudioManage_Main M_Audio;

    // Start is called before the first frame update
    void Start()
    {
        M_Audio = AudioManage_Main.instance;
        BM = GameObject.Find("TotalBook").GetComponent<BookMange>();
        Fristsetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (BookButtonManger.bookButtonManger.GetIndex() == 0)
        {
            ButtonMange();
        }
    }

    public void Fristsetting()
    {   
        //시작하면서 초기화
        BookMark.Clear();
        for (int i = 0; i < BM.BookMark.Count; i++)
        {
            BM.BookMark[i].GetComponent<Toggle>().isOn = false;
        }
        for (int i = 0; i < BM.Book.Count; i++)
        {
            BM.Book[i].SetActive(false);
        }

        Debug.Log("북마크 갯수" + BM.BookMark.Count);
        for (int i = 0; i < BM.BookMark.Count; i++)
        {
            Debug.Log("확인");
            if (BM.BookMark[i].activeSelf)
            {
                Debug.Log("존재함");
                BookMark.Add(BM.BookMark[i]);
            }
        }
    }
    public void ButtonMange()
    {
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            Debug.Log("오른쪽");
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            Debug.Log("왼쪽");
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
            Debug.Log("위");
            if (P_select == 0)
            {
                P_select = BookMark.Count - 1;
            }
            else
            {
                P_select--;
            }
            BookMark[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
            M_Audio.UI_Page();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
            Debug.Log("아래");
            if (P_select == BookMark.Count - 1)
            {
                P_select = 0;
            }
            else
            {
                P_select++;
            }
            BookMark[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
            M_Audio.UI_Page();
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK])&& BookButtonManger.bookButtonManger.buttoncheck == false)
        {
            BookButtonManger.bookButtonManger.ButtonTimerON();
            Debug.Log("엔터침!");
            M_Audio.UI_Click();
            BookButtonManger.bookButtonManger.BookMark_Select = BookMark[P_select];
            BookButtonManger.bookButtonManger.SetIndex(1);
        }
    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("바뀌면서 등장 인가?");
        Fristsetting();
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
