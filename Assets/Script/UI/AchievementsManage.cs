using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManage : MonoBehaviour
{
    static public AchievementsManage achievementsManage;

    [Header("Achievement")]
    [SerializeField]
    public List<Achievements> Achievement_All;  //실질적인 업적
    public List<bool> Ach_Clear;
    [SerializeField] public Dictionary<string, List<Achievements>> AchievementsList = new Dictionary<string, List<Achievements>>(); //업적 보이기용 리스트

    [Header("최초클 띄울 창")]
    public Record record;

    [Header("최초클리어 업적")]
    public List<Achievements> AchClear_panel;
    public AchClear_panel AchClear_panel_place;
    public GameObject AchNewText;
    Achievements AchPrefab;
    GameObject AchPrefab_instance;

    Achievements ach = new Achievements();

    [Header("업적 스텟")]
    public int Ach_MaxHp;
    public int Ach_Damge;
    public int Ach_Defense;
    public int Ach_Jump;
    public int Ach_Speed;
    public int Ach_AtkSPD;

    [Header("직접 넣어야하는 것")]
    public Ach_Book achbook;

    private void Awake()
    {
        achievementsManage = this;
        CreateItem();
        Refrsh_Ach_apply();
        achbook.FirstPreparation();
    }

    public void CreateItem()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Achievements");

        for (int i = 0; i < Data.Count; i++)
        {
            ach.foreignkey = int.Parse(Data[i]["foreignkey"].ToString());
            ach.type= Data[i]["type"].ToString();
            ach.title= Data[i]["title"].ToString();
            ach.contents= Data[i]["contents"].ToString();
            ach.requirements_num = Data[i]["requirements_num"].ToString();
            ach.state_text = Data[i]["stats_text"].ToString();
            ach.stats_name= Data[i]["stats_name"].ToString();
            ach.stats_plus_num= Data[i]["stats_plus_num"].ToString();
            ach.ach_index = Data[i]["ach_index"].ToString();
            Ach_Clear.Add(false);
            Achievement_All.Add(ach.Create());
            //Ach_Classify(ach);
        }
        Clear_Load();
    }

    public void CreateAchList(string Conect)        //업적들을 분류시키고 딕셔너리로 만든다.
    {
        List<Achievements> Ach = new List<Achievements>();
        for(int i=0;i<Achievement_All.Count;i++)
        {
            if (Achievement_All[i].stats_name == Conect)
            {
                if (!Ach_Clear[i])      //클리어 안한것만 넣기
                {
                    Ach.Add(Achievement_All[i]);
                }
            }
            else if (Conect == "clear") //클리어
            {
                if (Ach_Clear[i])
                {
                    Ach.Add(Achievement_All[i]);
                }
            }
            
        }
        AchievementsList.Add(Conect, Ach);
    }

    public void AchClearMove()      //업적 클리어 처리
    {
        for(int i=0; i<Ach_Clear.Count;i++)
        {
            if(Ach_Clear[i])
            {
                Debug.Log("업적 최초 클리어1212" + Achievement_All[i].stats_name);
                Debug.Log("업적 최초 클리어" + AchievementsList[Achievement_All[i].stats_name][i].title);
                AchievementsList[Achievement_All[i].stats_name].Add(Achievement_All[i]);
                AchievementsList[Achievement_All[i].stats_name].RemoveAt(i);
            }
        }
    }
    
    public void AchClearUpdate(int AchClear_Index)        //최초클 패널 띄우기
    {
            AchClear_panel_place.Title.text = AchClear_panel[AchClear_Index - 1].title;
            AchClear_panel_place.Content.text = AchClear_panel[AchClear_Index - 1].contents;
            AchClear_panel_place.Count.text = AchClear_Index.ToString() + "/" + AchClear_panel.Count;
    }

    public void AchClearCheck()     //업적 최초 클 체크
    {
        Debug.Log("업적 최초로 클리어 했는지 체크 중");
        for (int i=0; i<Achievement_All.Count;i++)
        {
            if(!Ach_Clear[i])   //클리어 안된것들 중에서
            {
                if(Main_Record.M_Reord.Check_Ach(Achievement_All[i].type, Achievement_All[i].requirements_num))     //수 비교
                {
                    AchClear_panel.Add(Achievement_All[i]);
                    Ach_Clear[i] = true;
                   // record.ActionText_plus("- "+Achievement_All[i].title + " 클리어");
                }
            }
        }

        if (AchClear_panel.Count > 0)   //최초 클리어 패널 띄우기
        {
            AchClear_panel_place.gameObject.SetActive(true);
            AchClear_panel_place.AchPanelEnd();
            //AchNewText.SetActive(true);
            AchClearMove();
        }
        Refrsh_Ach_apply();
    }

    public void Clear_Load()
    {
        if (ES3.FileExists(Application.persistentDataPath + "/" + SavePath.path + "/" + "Ach_Clear.es3"))
        {
            List<bool> Past = new List<bool>();
            Past = ES3.Load("Ach_Clear", Application.persistentDataPath + "/" + SavePath.path + "/" + "Ach_Clear.es3",Ach_Clear);
            for (int i = 0; i < Past.Count; i++)      //업데이트
            {
                Ach_Clear[i] = Past[i];
            }
        }
        else
        {
            Debug.Log("세이브 파일이 존재하지 않아 새로 생성합니다.");
            Clear_Save();
            Clear_Load();
        }
    }
    public void Clear_Save()
    {
        ES3.Save("Ach_Clear",Ach_Clear, Application.persistentDataPath + "/" + SavePath.path + "/" + "Ach_Clear.es3");
    }

    public void Refrsh_Ach_apply()      //클리어 체크 후 스텟 적용 작업
    {
        for (int i = 0; i < Ach_Clear.Count; i++)
        {
            if (Ach_Clear[i])   //만약 클리어라면
            {
                if (!Achievement_All[i].Statapply)
                {
                    Debug.Log("업적완료 스텟을 적용시킵니다.");
                    PlusState(Achievement_All[i]);    //스텟을 적용시킨다
                }
            }
        }
    }
    public void PlusState(Achievements ach)      //스텟 적용
    {
        Debug.Log("업적완료 스텟을 적용할 것" + ach.type);
        Debug.Log("업적완료 적용할 스텟의 양" + ach.stats_plus_num);
        ach.Statapply = true;
        switch (ach.stats_name)
        {
            case "hp":
                Ach_MaxHp += int.Parse(ach.stats_plus_num.ToString());
                break;
            case "power":
                Ach_Damge += int.Parse(ach.stats_plus_num.ToString());
                break;
            case "armor":
                Ach_Defense += int.Parse(ach.stats_plus_num.ToString());
                break;
            case "jump":
                Ach_Jump += int.Parse(ach.stats_plus_num.ToString());
                break;
            case "speed":
                Ach_Speed += int.Parse(ach.stats_plus_num.ToString());
                break;
        }
    }
}
