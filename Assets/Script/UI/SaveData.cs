using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    KeepActionRecord Kep;
    List<bool> Ach_Clear=new List<bool>();

    [Header("세이브 내용 출력 텍스트")]
    public Text Stage_T;
    public Text Clear_T;
    public Text Ach_T;
    public Text Time_T;

    public Text NullText;
    [Header("출력여부")]
    public GameObject Image;
    public GameObject aliveSave;
    public GameObject NullSave;

    bool Savecheck;
    string path;
    int AchClearCount;
    int ClearIndex;
    string RecentStage;

    int hour;
    int min;
    int sec;

    private void Start()
    {
        path = this.gameObject.name;
        KeepLoad();
    }

    public void KeepLoad()
    {
        if (ES3.FileExists(Application.persistentDataPath + "/"+path+"/" + "AllRecord.es3"))
        {
            Debug.Log("세이브 파일 존재");
            Savecheck = true;
            Kep = ES3.Load<KeepActionRecord>("AllRecord", Application.persistentDataPath + "/" + path + "/" + "AllRecord.es3");
            Ach_Clear = ES3.Load("Ach_Clear", Application.persistentDataPath + "/" + path + "/" + "Ach_Clear.es3", Ach_Clear);
            CheckAchClear();
            CheckStageClear();
            TimerTrans();
            OnSave();
            
        }
        else
        {
            OffSave();
            Savecheck = false;
        }
    }

    public void CheckAchClear()
    {
        for(int i=0;i<Ach_Clear.Count;i++)
        {
            if(Ach_Clear[i])
            {
                AchClearCount++;
                ClearIndex = i;
            }
        }
       
    }

    public void CheckStageClear()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Stage");
        RecentStage = Data[ClearIndex]["Name"].ToString();      //임시로 넣었으나 나중에 스테이지 클리어도 만들어야함

    }

    public void ButtonOn()      //클릭했을 때
    {
        SavePath.path = this.path;
        SceneManager.LoadScene("StageSelect");
    }
  
    public void TimerTrans()
    {
        hour = (int)Kep.T_Time / 3600;
        min = ((int)Kep.T_Time % 3600) / 60;
        sec = ((int)Kep.T_Time % 3600) % 60;
    }

    public void OffSave()
    {
        Image.SetActive(false);
        aliveSave.SetActive(false);
        NullSave.SetActive(true);
        NullText.text = "빈공간";
    }
    public void OnSave()
    {
        Image.SetActive(true);
        aliveSave.SetActive(true);
        NullSave.SetActive(false);
        Stage_T.text = RecentStage;
        Clear_T.text = "업데이트 예정";
        Ach_T.text = AchClearCount + " / " + Ach_Clear.Count;
        Time_T.text = "\n" + hour + " 시간 " + min + " 분 " + sec + " 초 ";
    }
}
