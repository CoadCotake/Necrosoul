using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ach_Book : MonoBehaviour
{
    public List<GameObject> Title = new List<GameObject>();
    public GameObject TitlePlace;
    public List<Ach_Book_Button> TotalAch_B = new List<Ach_Book_Button>();
    public Text PageNumber;
    public int ActiveButton;
    [SerializeField]int PageNumberlimit=5;
    [SerializeField]int PageNow=1;
    [SerializeField]int PageTotal;

    Ach_Book_Button Ach_B_Button = new Ach_Book_Button();
    // Start is called before the first frame update
    void Start()
    {
        //FirstPreparation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstPreparation()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Ach_Button");

        for (int i = 0; i < Data.Count; i++)
        {
            Ach_B_Button.Ach_Button_Image = Resources.Load<Sprite>(Data[i]["Image"].ToString());
            Ach_B_Button.Ach_Button_Name = Data[i]["Name"].ToString();
            Ach_B_Button.Ach_Button_Conect = Data[i]["Conect"].ToString();
            TotalAch_B.Add(Ach_B_Button.Create());
        }
        GetTitle();

        PageTotal = TotalAch_B.Count / PageNumberlimit;
        if (TotalAch_B.Count % PageNumberlimit!=0)
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
            if (i >= TotalAch_B.Count)
            {
                Title[i % PageNumberlimit].SetActive(false);
            }
            else
            {
                Title[i % PageNumberlimit].SetActive(true);
                ActiveButton++;
                Title[i % PageNumberlimit].GetComponent<AchTitle>().ChangeElement(TotalAch_B[i]);
                Title[i % PageNumberlimit].GetComponent<AchTitle>().FristStart();
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
