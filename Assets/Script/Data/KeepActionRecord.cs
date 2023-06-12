using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class KeepActionRecord //누적되는 전체 기록
{
    public int T_EnemyKill; //0
    public int T_Money;     //1
    public float T_Time;    //2
    public float T_Damge;   //3
    public float T_Hit;     //3
    public int T_Died;      //4
    public int T_ClearStage;//5
    public int[] Index = new int[8];

    public void indexUpdate()
    {
        Index[0] = T_EnemyKill;
        Index[1] = T_Money;
        Index[2] = (int)T_Time;
        Index[3] = (int)T_Damge;
        Index[4] = (int)T_Hit;
        Index[5] = T_Died;
        Index[6] = T_ClearStage;
    }


    public void KeepSave(ActionRecord action)
    {
        KeepLoad();
        Debug.Log("전체 통계가 세이브 중입니다.");
        Debug.Log("킬수" +T_EnemyKill);
        Debug.Log("돈" + T_Money);
        Debug.Log("시간" + T_Time);
        Debug.Log("데미지" + T_Damge);
        Debug.Log("맞은데미지" + T_Hit);
        Debug.Log("죽은수" + T_Died);
        Debug.Log("클리어수" + T_ClearStage);

        T_EnemyKill +=action.EnemyKill;
        T_Money+=action.Money;
        T_Time+=action.Time;
        T_Damge+=action.Damge;
        T_Hit+=action.Hit;
        T_Died+=action.Died;
        T_ClearStage += action.ClearStage;
        ES3.Save("AllRecord", this, Application.persistentDataPath+ "/" + SavePath.path + "/" + "AllRecord.es3");
        Debug.Log("----------------적용후");
        Debug.Log("킬수" + T_EnemyKill);
        Debug.Log("돈" + T_Money);
        Debug.Log("시간" + T_Time);
        Debug.Log("데미지" + T_Damge);
        Debug.Log("맞은데미지" + T_Hit);
        Debug.Log("죽은수" + T_Died);
        Debug.Log("클리어수" + T_ClearStage+"\n-------------------------------");
        //ES3.Save("AllRecord", this);
    }
    public void KeepSaveNull()
    {
        T_EnemyKill += 0;
        T_Money += 0;
        T_Time += 0;
        T_Damge += 0;
        T_Hit += 0;
        T_Died += 0;
        T_ClearStage += 0;
        ES3.Save("AllRecord", this, Application.persistentDataPath + "/" + SavePath.path + "/" + "AllRecord.es3");
        //ES3.Save("AllRecord", this);
    }

    public void KeepLoad()
    {      
        if (ES3.FileExists(Application.persistentDataPath + "/" + SavePath.path + "/" + "AllRecord.es3"))
        {
            Debug.Log("로드는 성공적으로 되었을가?");
            KeepActionRecord Load = ES3.Load<KeepActionRecord>("AllRecord", Application.persistentDataPath + "/" + SavePath.path + "/" + "AllRecord.es3");
            this.T_EnemyKill = Load.T_EnemyKill;
            this.T_Money = Load.T_Money;
            this.T_Time = Load.T_Time;
            this.T_Damge = Load.T_Damge;
            this.T_Hit = Load.T_Hit;
            this.T_Died = Load.T_Died;
            this.T_ClearStage = Load.T_ClearStage;
        }
        else
        {
            Debug.Log("로드는 성공적으로 되었을가?2");
            KeepSaveNull();
        }
    }

}
