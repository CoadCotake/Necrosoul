using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven_Book : MonoBehaviour
{
    public List<GameObject> Title = new List<GameObject>();
    public GameObject TitlePlace;
    public List<Inven_Book_Button> TotalInven_B = new List<Inven_Book_Button>();
    public Text PageNumber;
    public int ActiveButton;
    [SerializeField]int PageNumberlimit=4;
    [SerializeField]int PageNow=1;
    [SerializeField]int PageTotal;

    Inven_Book_Button Inven_B_Button = new Inven_Book_Button();

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
        List<Dictionary<string, object>> Data = CSVReader.Read("Inven_Button");

        for (int i = 0; i < Data.Count; i++)
        {
            Inven_B_Button.Inven_Button_Image = Resources.Load<Sprite>(Data[i]["Image"].ToString());
            Inven_B_Button.Inven_Button_Name = Data[i]["Name"].ToString();
            Inven_B_Button.key = int.Parse(Data[i]["Key"].ToString());
            TotalInven_B.Add(Inven_B_Button.Create());
        }
        GetTitle();

        PageTotal = TotalInven_B.Count / PageNumberlimit;
        if (TotalInven_B.Count % PageNumberlimit!=0)
        {
            PageTotal++;
        }
        ChangeElement();
        PageNumber.text = PageNow + "/" + PageTotal;
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
            Debug.Log("버튼 생산중입니다!");
            if (i >= TotalInven_B.Count)
            {
                Debug.Log("버튼 중입니다!"+i);
                Title[i % PageNumberlimit].SetActive(false);
            }
            else
            {
                Debug.Log("버튼 wkf!" + i);
                Title[i % PageNumberlimit].SetActive(true);
                ActiveButton++;
                Title[i % PageNumberlimit].GetComponent<InvenTitle>().ChangeElement(TotalInven_B[i]);
                Title[i % PageNumberlimit].GetComponent<InvenTitle>().FristStart();
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
        PageNumber.text = PageNow + "/" + PageTotal;
    }
    public void R_Button()
    {
        if(PageNow<PageTotal)
        {
            PageNow++;
        }
        ChangeElement();
        PageNumber.text = PageNow + "/" + PageTotal;
    }
}
