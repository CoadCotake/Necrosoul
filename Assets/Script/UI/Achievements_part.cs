using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements_part : MonoBehaviour
{
    public Achievements ach;
    public Image Rock;

    [Header("텍스트")]
    public Text title;
    public Text contents;
    public Text state_text;

    // Start is called before the first frame update
    void Start()
    {
        title.text = ach.title;
        Main_Record.M_Reord.Keep.indexUpdate();
        state_text.text = ach.state_text;
        AchClearCheck();
    }

    public void AchClearCheck()
    {
        Debug.Log("업적이 완료가 되었는지 체크중");
        if (AchievementsManage.achievementsManage.Ach_Clear[ach.foreignkey])        //업적완료일때 처리
        {
            Debug.Log("스텟 증가" + ach.stats_plus_num);
            contents.text = ach.contents + "\n" + "( 업적 완료 )";
            Rock.gameObject.SetActive(true);
            //AchievementsManage.achievementsManage.Ach_Clear_place(this.gameObject);
        }
        else    //업적이 완료가 안되었다면
        {
            if (ach.ach_index.ToString() == "2")    //시간 관련 업적이라면 다르게 표시
            {
                TimerTrans();
                contents.text = ach.contents + "\n" + "( " + hour + " 시 " + min + " 분 " + sec + " 초 " + " / " + ach.requirements_num + "시간" + " )";
            }
            else
            {
                contents.text = ach.contents + "\n" + "( "  + (int)Main_Record.M_Reord.Keep.Index[int.Parse(ach.ach_index.ToString())] + " / " + ach.requirements_num + " )";
            }
            Rock.gameObject.SetActive(false);
        }
    }
    int hour;
    int min;
    int sec;
    public void TimerTrans()
    {
        hour = (int)Main_Record.M_Reord.Keep.Index[int.Parse(ach.ach_index.ToString())] / 3600;
        min = ((int)Main_Record.M_Reord.Keep.Index[int.Parse(ach.ach_index.ToString())] % 3600) / 60;
        sec = ((int)Main_Record.M_Reord.Keep.Index[int.Parse(ach.ach_index.ToString())] % 3600) % 60;
    }

    
}
