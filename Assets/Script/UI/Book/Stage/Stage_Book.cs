using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Book : MonoBehaviour
{
    public List<GameObject> Title = new List<GameObject>();
    public GameObject TitlePlace;
    public List<Stage> TotalStage = new List<Stage>();
    public Text PageNumber;
    public int ActiveButton;
    [SerializeField]int PageNumberlimit=5;
    [SerializeField]int PageNow=1;
    [SerializeField]int PageTotal;


    Stage stage = new Stage();
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
        List<Dictionary<string, object>> Data = CSVReader.Read("Stage");

        for (int i = 0; i < Data.Count; i++)
        {
            stage.Stage_Image = Resources.Load<Sprite>(Data[i]["Iamge"].ToString());
            stage.Stage_Name = Data[i]["Name"].ToString();
            stage.Stage_Descrition = Data[i]["Description"].ToString();
            TotalStage.Add(stage.Create());
        }
        GetTitle();

        PageTotal = TotalStage.Count / PageNumberlimit;
        if (TotalStage.Count % PageNumberlimit!=0)
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
            if (i >= TotalStage.Count)
            {
                Title[i % PageNumberlimit].SetActive(false);
            }
            else
            {
                Title[i % PageNumberlimit].SetActive(true);
                ActiveButton++;
                Title[i % PageNumberlimit].GetComponent<StageContent>().ChangeElement(TotalStage[i]);
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
