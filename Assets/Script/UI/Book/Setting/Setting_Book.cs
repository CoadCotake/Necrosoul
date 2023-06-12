using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Book : MonoBehaviour
{
    public List<GameObject> Title = new List<GameObject>();
    public GameObject TitlePlace;
    public List<Setting_class> TotalSetting = new List<Setting_class>();
    //public Text PageNumber;
    public int ActiveButton;
    [SerializeField]int PageNumberlimit=5;
    [SerializeField]int PageNow=1;
    [SerializeField]int PageTotal;


    Setting_class setting = new Setting_class();
    // Start is called before the first frame update
    void Start()
    {
        FirstPreparation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstPreparation()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Setting_Button");

        for (int i = 0; i < Data.Count; i++)
        {
            setting.Setting_Image = Resources.Load<Sprite>(Data[i]["Image"].ToString());
            setting.Setting_Name = Data[i]["Name"].ToString();
            TotalSetting.Add(setting.Create());
        }
        GetTitle();

        PageTotal = TotalSetting.Count / PageNumberlimit;
        if (TotalSetting.Count % PageNumberlimit!=0)
        {
            PageTotal++;
        }
        ChangeElement();
        //PageNumber.text = PageNow + "/" + PageTotal;
    }

    public void GetTitle()
    {
        for(int i=0; i < TitlePlace.transform.childCount;i++)
        {
            Title.Add(TitlePlace.transform.GetChild(i).gameObject);
        }
    }

    public void ChangeElement()
    {
        ActiveButton = 0;
        for (int i=(PageNow-1)*PageNumberlimit;i< PageNow*PageNumberlimit;i++)
        {
            if (i >= TotalSetting.Count)
            {
                Title[i % PageNumberlimit].SetActive(false);
            }
            else
            {
                Title[i % PageNumberlimit].SetActive(true);
                ActiveButton++;
                //해당 버튼에 GameOjbect부분에 정보를 주어 버튼이 클릭되면 준 정보의 패널이 열리도록 설정하기
            }
        }
    }
    public void L_Button()
    {
        if(PageNow>1)
        {
            PageNow--;
        }
        ChangeElement();
        //PageNumber.text = PageNow + "/" + PageTotal;
    }
    public void R_Button()
    {
        if(PageNow<PageTotal)
        {
            PageNow++;
        }
        ChangeElement();
        //PageNumber.text = PageNow + "/" + PageTotal;
    }
}
