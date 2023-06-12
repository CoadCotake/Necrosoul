using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookButtonManger : MonoBehaviour
{
    static public BookButtonManger bookButtonManger;
    public Animator BookAni;
    int Index; 

    [Header("북마크")]
    [SerializeField]public GameObject BookMark_Select;

    [Header("타이틀")]
    public GameObject BookTitle_Select;

    [Header("책")]
    public GameObject Book_select;

    [Header("인벤변수")]
    public int Inven_Key_check=0;

    [Header("중복 버튼 눌림 방지")]
    public float time;
    public bool buttoncheck;

    [Header("북마크, 책 그림자 추가")]
    public GameObject Book_Sh;
    public GameObject BookMark_sh;

    [Header("직접 넣기")]
    public GameObject TotalBook;
    public CanvasGroup InvenTitle;

    private void Awake()
    {
        bookButtonManger = this;
        BookAni_Check();
    }
    private void Update()
    {
        if (buttoncheck)
        {
            ButtonTimer();
        }
    }
    public void ResetButton()
    {
        BookMark_Select = null;
        BookTitle_Select = null;
        Book_select = null;
        Index = 0;
        time = 0;
        buttoncheck = false;
        InvenTitle.alpha = 1;
        TotalBook.GetComponent<RectTransform>().anchoredPosition = new Vector3(1011f,6.13f, 0f);
        BookAni_Check();
    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetButton();
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void SetIndex(int i)
    {
        Index = i;
        BookAni_Check();
    }
    public int GetIndex()
    {
        return Index;
    }

    public void BookAni_Check()
    {
        if (Index == 0)
        {
            BookMark_sh.SetActive(false);
            Book_Sh.SetActive(true);
            BookAni.SetBool("Chose", false);
        }
        else
        {
            BookMark_sh.SetActive(true);
            Book_Sh.SetActive(false);
            BookAni.SetBool("Chose", true);
        }
    }
    public void ButtonTimerON()
    {
        Debug.Log("타이머 작동");
        buttoncheck = true;
        time = 1;
    }
    public void ButtonTimerON2()
    {
        Debug.Log("타이머 작동");
        buttoncheck = true;
        time = 20;
    }
    public void ButtonTimer()
    {
        Debug.Log("타이머 작동중");
        if (time >= 0)
        {
            
            time -=1;
            Debug.Log("타이머 작동중11 :" + time) ;
        }
        else
        {
            buttoncheck = false;

        }

    }
}
