using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ach_Stat : MonoBehaviour
{
    public Text HP;
    public Text Power;
    public Text Def;
    public Text Spd;
    public Text AtkSpd;
    public Text Cri;

    private void OnEnable()
    {

        HP.text = "+" + AchievementsManage.achievementsManage.Ach_MaxHp.ToString();


        Power.text = "+" + AchievementsManage.achievementsManage.Ach_Damge.ToString();


        Def.text = "+" + AchievementsManage.achievementsManage.Ach_Defense.ToString();


        Spd.text = "+" + AchievementsManage.achievementsManage.Ach_Speed.ToString();


        AtkSpd.text = "+" + AchievementsManage.achievementsManage.Ach_AtkSPD.ToString();


        Cri.text = "+" + AchievementsManage.achievementsManage.Ach_Jump.ToString();
    }
}
