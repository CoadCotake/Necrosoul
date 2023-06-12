using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    public ActionRecord ActionRecord=new ActionRecord();
    KeepActionRecord KeepRecord = new KeepActionRecord();

    int hour;
    int min;
    int sec;
    public Text StageName;
    public Text StageNum;
    public Text Timer;
    public Text Kill;
    public Text Money;
    public GameObject ActionText_prefab;
    public GameObject ActionPanel;

    public bool Game_End;

    private void Start()
    {
        ActionRecord.Reset();
        Game_End = false;
    }
    private void FixedUpdate()
    {
        if (!Game_End&&Player_status.p_status.spawn_check)
        {
            if (Player_status.p_status.get_hp() > 0)
            {
                ActionRecord.Time += Time.deltaTime;
            }
            else
            {
                Debug.Log("게임이 종료됨 업적 갱신");
                finish();
            }
        }
        
    }
    public void finish()
    {
        TimerTrans();
        TextTrans();
        SaveRecord();
        AchievementsManage.achievementsManage.AchClearCheck();
        AchievementsManage.achievementsManage.Clear_Save();
        Debug.Log("실행됨");
        Game_End = true;
    }

    public void SaveRecord()      //전체 기록에 남기기
    {
        KeepRecord.KeepSave(ActionRecord);
    }

    public void TimerTrans()
    {
        hour = (int)ActionRecord.Time / 3600;
        min = ((int)ActionRecord.Time%3600) / 60;
        sec = ((int)ActionRecord.Time % 3600) % 60;
    }

    public void TextTrans()     //종료창 텍스트 추가
    {
        ActionRecord.Money = Player_status.p_status.Money;
        StageName.text = "스테이지 :";
        StageNum.text = "아무도 드나들지 않는 숲";
        Kill.text = ActionRecord.EnemyKill.ToString();
        //ActionText_Plus_All();
        //ActionText.text = "-가한 데미지 " + ActionRecord.Damge + "\n" + "-맞은 데미지 " + ActionRecord.Hit+"\n"+ "-돈 " + ActionRecord.Money + " 획득\n";
        Timer.text = hour + "시 " + min + "분 " + sec + "초 ";
        Money.text = ActionRecord.Money.ToString()+"GOLD";



    }

    public void ActionText_plus(string text)       //결과 로그 텍스트 추가
    {
        GameObject Text=Instantiate(ActionText_prefab, ActionPanel.transform);
        Text.GetComponent<Text>().text = text;
    }

    public void ActionText_Plus_All()       //기본적인 로그 텍스트모음
    {
        ActionText_plus("-가한 데미지 " + ActionRecord.Damge);
        ActionText_plus("-맞은 데미지 " + ActionRecord.Hit);
        ActionText_plus("-돈 " + ActionRecord.Money);
    }
}
