using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_status : MonoBehaviour
{
    //레이아웃 숫자

    [SerializeField]
    public int original_MaX_HP;//최대 체력 원본
    private int MaX_HP_bonus;//최대 체력 가산점
    private int HP;//현재 체력
    private int original_Defense;//방어력 원본
    private int Defense_bonus;//방어력 가산점
    private float original_untouchable_time;//무적시간 원본 
    private float untouchable_time_bonus;//무적시간 보너스

    float original_speed;
    float speed_bonus;

    [Header("jump")]
    private float original_jump_force;//점프높이 원본
    float jump_force_bonus;//점프높이 가산점
    private int original_max_jump_count;//점프 횟수 원본
    int max_jump_count_bonus;//점프횟수 가산점

    [Header("gun")]
    private float original_firedelay;//원본 발사 속도
    private float firedelay_bonus;//발사속도 보너스
    public int original_Gun_Atk;//총 공격력 원본
    private int Gun_Atk_bonus;//총 공격력 가산점
    private float original_bullet_speed;//총알 속도 원본
    private float bullet_speed_bonus;//총알 속도 가산점
    private bool volly;//연발,단발

    [Header("dash")]
    float original_dash_force;//대쉬길이 원본
    float dash_force_bonus;//대쉬길이 가산점
    int original_max_dash_count = 1;//대쉬횟수 원본
    int max_dash_count_bonus;//대쉬횟수 가산점

    int money;

    //List<Dictionary<string, object>> Data = CSVReader.Read("playerStatus");
    //csv로  original 값 정하면 될듯?
    public void set_layout(int i)
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Enemy");

        original_MaX_HP = int.Parse(Data[i]["Max_Hp"].ToString());
        HP = get_max_hp();
        money = int.Parse(Data[i]["KillMoney"].ToString());
        original_Defense = int.Parse(Data[i]["Defense"].ToString());
        original_speed = float.Parse(Data[i]["Move_Speed"].ToString());

        original_Gun_Atk = int.Parse(Data[i]["Attack"].ToString());


    }
    public int get_money()
    {
        return money+(int)((Gamemanager.GM.stage - 1));
    }
    public float get_firedelay()
    {
        return original_firedelay + firedelay_bonus;
    }
    public int get_atk()
    {
        return original_Gun_Atk + Gun_Atk_bonus+ (int)((Gamemanager.GM.stage - 1) * Gamemanager.GM.atk_upr);
    }
    public float get_bullet_speed()
    {
        return original_bullet_speed + bullet_speed_bonus+(float)((Gamemanager.GM.stage - 1)* Gamemanager.GM.b_spd_upr);
    }
    public bool get_volly()
    {
        return volly;
    }
    public float get_dash_force()
    {
        return original_dash_force + dash_force_bonus;
    }
    public float get_speed()
    {
        return original_speed + speed_bonus + (float)((Gamemanager.GM.stage - 1) * Gamemanager.GM.spd_upr);
    }
    void Awake()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get_max_hp()
    {
        return (original_MaX_HP+MaX_HP_bonus)+(int)((Gamemanager.GM.stage-1)*Gamemanager.GM.hp_upr*original_MaX_HP);
    }
    public int get_hp()
    {
        return HP;
    }
    public void set_max_HP(int i)
    {
        MaX_HP_bonus += i;
    }
    public void set_hp(int i)
    {
        HP += i;
    }
    public void damage_hp(int i)
    {
        HP -= i-i*get_defense_point();
    }
    public float get_untouchable_time()
    {
        return original_untouchable_time + untouchable_time_bonus;
    }
    public int get_dash_count()
    {
        return original_max_dash_count+max_dash_count_bonus;
    }
    public int get_jump_count()
    {
        return original_max_jump_count + max_jump_count_bonus;
    }
    public float get_jump_force()
    {
        return original_jump_force + jump_force_bonus;
    }
    public int get_defense_point()
    {
        return original_Defense + Defense_bonus;
    }
}
