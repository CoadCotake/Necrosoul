using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_UI : MonoBehaviour
{
    //public Text Hp;
    //public Text ATK;
    //public Text DEF;
    //public Text Speed;
    //public Text ShotSPD;
    //public Text Jump;

    public GameObject Hp;
    public GameObject ATK;
    public GameObject DEF;
    public GameObject Speed;
    public GameObject ShotSPD;
    public GameObject Jump;

    // Update is called once per frame
    void Update()
    {
        
        Hp.transform.GetChild(0).GetComponent<Text>().text = "체력: "+Player_status.p_status.get_max_hp().ToString();
        Hp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = (Player_status.p_status.get_max_hp() - Player_status.p_status.get_max_Hp_bonus() - AchievementsManage.achievementsManage.Ach_MaxHp).ToString();
        Hp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + Player_status.p_status.get_max_Hp_bonus().ToString();
        Hp.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "+" + AchievementsManage.achievementsManage.Ach_MaxHp.ToString();

        ATK.transform.GetChild(0).GetComponent<Text>().text = "공격력: " + Player_status.p_status.get_atk().ToString();
        ATK.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "+" + (Player_status.p_status.get_atk() - Player_status.p_status.get_atk_bonus() - AchievementsManage.achievementsManage.Ach_Damge).ToString();
        ATK.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + Player_status.p_status.get_atk_bonus().ToString();
        ATK.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "+" + AchievementsManage.achievementsManage.Ach_Damge.ToString();

        DEF.transform.GetChild(0).GetComponent<Text>().text = "방어력: " + Player_status.p_status.get_defense_point().ToString();
        DEF.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "+" + (Player_status.p_status.get_defense_point() - Player_status.p_status.get_defense_bonus() - AchievementsManage.achievementsManage.Ach_Defense).ToString();
        DEF.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + Player_status.p_status.get_defense_bonus().ToString();
        DEF.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "+"+AchievementsManage.achievementsManage.Ach_Defense.ToString();
       
        Speed.transform.GetChild(0).GetComponent<Text>().text = "이동속도: " + Player_status.p_status.get_speed().ToString();
        Speed.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "+" + (Player_status.p_status.get_speed()- AchievementsManage.achievementsManage.Ach_Speed- Player_status.p_status.get_speed_bonus()).ToString();
        Speed.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + Player_status.p_status.get_speed_bonus();
        Speed.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "+" + AchievementsManage.achievementsManage.Ach_Speed.ToString();

        ShotSPD.transform.GetChild(0).GetComponent<Text>().text = "공격속도: " + Player_status.p_status.get_firedelay().ToString();
        ShotSPD.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "+" + (Player_status.p_status.get_firedelay()-Player_status.p_status.get_firedelay_bonus()- AchievementsManage.achievementsManage.Ach_AtkSPD).ToString();
        ShotSPD.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + Player_status.p_status.get_firedelay_bonus().ToString();
        ShotSPD.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "+" + AchievementsManage.achievementsManage.Ach_AtkSPD.ToString();

        Jump.transform.GetChild(0).GetComponent<Text>().text = "크리티컬: " + Player_status.p_status.get_origin_critical_rate().ToString();
        Jump.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "+" + Player_status.p_status.get_critical_rate().ToString();
        Jump.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + Player_status.p_status.get_critical_rate_bonus().ToString();
        Jump.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "+" + AchievementsManage.achievementsManage.Ach_Jump.ToString();

    }
}
