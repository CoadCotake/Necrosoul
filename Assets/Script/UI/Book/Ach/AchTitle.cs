using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchTitle : MonoBehaviour
{
    [Header("타이틀")]
    public Image AchImage;
    public Text AchName;

    [SerializeField]Ach_Book_Button Ach_p;

    public GameObject AchC_ContentPlace;
    public List<GameObject> AchC_Content;
  

    [Header("내용 출력")]
    public Image AchImage_C;
    public Text AchName_C;
    public Text AchConect;

    [Header("업적패널")]
    [SerializeField] int PageNumberlimit = 6;
    [SerializeField] int PageNow = 1;
    [SerializeField] int PageTotal;
    public Text PageNumber;
    public Text AchTitle_Name;
    public Toggle My_toggle;

    public Button R_Btn;
    public Button L_Btn;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        My_toggle = this.gameObject.GetComponent<Toggle>();


    }

    // Update is called once per frame
    void Update()
    {
        if(My_toggle.isOn)
        {
            ButtonOn();
        }
    }

    public void ChangeElement(Ach_Book_Button ach_b)        //버튼의 요소
    {
        this.Ach_p = ach_b;
        AchImage.sprite = Ach_p.Ach_Button_Image;
        AchName.text = Ach_p.Ach_Button_Name;
    }
    public void ChangeElement_panel()        //패널의 요소
    {
        //Debug.Log("버튼의 업적 갯수" + AchievementsManage.achievementsManage.AchievementsList[Ach_p.Ach_Button_Conect].Count);
        for (int i = (PageNow - 1) * PageNumberlimit; i < PageNow * PageNumberlimit; i++)
        {
            if (i >= AchievementsManage.achievementsManage.AchievementsList[Ach_p.Ach_Button_Conect].Count)
            {
                AchC_Content[i % PageNumberlimit].gameObject.SetActive(false);
            }
            else
            {
                AchC_Content[i % PageNumberlimit].gameObject.SetActive(true);
                AchC_Content[i % PageNumberlimit].GetComponent<AchC_Contnet>().ChangeAch(AchievementsManage.achievementsManage.AchievementsList[Ach_p.Ach_Button_Conect][i]);
            }
        }
        PageNumber.text = PageNow + "/" + PageTotal;
        AchTitle_Name.text = Ach_p.Ach_Button_Name;
    }

    public void FristStart()
    {
        for (int i = 0; i < AchC_ContentPlace.transform.childCount; i++)       
        {
            AchC_Content.Add(AchC_ContentPlace.transform.GetChild(i).gameObject);
        }
        Debug.Log("리스트 만드는 중");
        AchievementsManage.achievementsManage.CreateAchList(Ach_p.Ach_Button_Conect);       //분류 시켜 리스트 만들기
        PageTotal = AchievementsManage.achievementsManage.AchievementsList[Ach_p.Ach_Button_Conect].Count / PageNumberlimit;
        if (AchievementsManage.achievementsManage.AchievementsList[Ach_p.Ach_Button_Conect].Count % PageNumberlimit != 0)
        {
            PageTotal++;
        }
        PageNumber.text = PageNow + "/" + PageTotal;
    }
    public void ButtonOn()
    {
        R_Btn.onClick.RemoveAllListeners();
        L_Btn.onClick.RemoveAllListeners();
        ChangeElement_panel();
        R_Btn.onClick.AddListener(R_Button);
        L_Btn.onClick.AddListener(L_Button);
    }

    public void L_Button()
    {
        if (PageNow > 1)
        {
            PageNow--;
        }
        ChangeElement_panel();
        PageNumber.text = PageNow + "/" + PageTotal;
    }
    public void R_Button()
    {
        if (PageNow < PageTotal)
        {
            PageNow++;
        }
        ChangeElement_panel();
        PageNumber.text = PageNow + "/" + PageTotal;
    }
}
