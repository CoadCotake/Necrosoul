using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchC_Contnet : MonoBehaviour
{
    int hour;
    int min;
    int sec;
    [Header("업적 요소")]
    public Text Title;
    public Text Content;
    public Text stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeAch(Achievements A)
    {
        Title.text = A.title;
        if (A.stats_name == "power")
        {
            Content.text = A.contents +" ( " + Main_Record.M_Reord.Keep.T_EnemyKill +" 마리"+" )";
        }
        else
        {
            TimerTrans();
            Content.text = A.contents + " ( " + hour + "시 " + min + "분 " + sec + "초 " + " ) ";
        }
        stats.text = A.state_text;
    }

    public void TimerTrans()
    {
        hour = (int)Main_Record.M_Reord.Keep.T_Time / 3600;
        min = ((int)Main_Record.M_Reord.Keep.T_Time % 3600) / 60;
        sec = ((int)Main_Record.M_Reord.Keep.T_Time % 3600) % 60;
    }
}
