using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shadow_animator : MonoBehaviour
{
    internal bool move_state;
    internal bool sword_delay;
    private Animator m_animator;
    public int anim_state;
    private int m_currentAttack = 0;
    public int m_currentAttack_2 = 0;
    public Rigidbody2D rgd;
    public bool death_state;
    public bool Hit_state;
    public bool jump_state;
    public bool attack_state;
    public bool crouch_state;
    Player_shadow p_s;
    private void Update()
    {
        set_airspeed();
        m_animator.SetFloat("Attackspeed", Player_status.p_status.get_firedelay() + 1);
        
      
        if (jump_state)
        {
            jump_anim();
        }
        else if (move_state)
        {
            move_anim();
        }
        else
        {
            idle_anim();
        }
    }
    public void set_ground(bool a)
    {
        m_animator.SetBool("Grounded", a);
    }
    public void sword_attack_anim()
    {
        if (!sword_delay)
        {




            m_animator.SetInteger("sword_count", m_currentAttack_2);
            m_animator.SetTrigger("swordattack" + 1);
        }



    }

    public void sword_attack_anim(int a)
    {
        if (!sword_delay)
        {




            m_animator.SetInteger("sword_count", a);
            m_animator.SetTrigger("swordattack" + 1);
        }



    }
    public void sword_anim_start()
    {

        m_animator.SetInteger("sword_count", 0);
        m_currentAttack_2 = 0;
        sword_delay = false;
    }
    public void air_attack_anim_mirror()
    {

        if (m_currentAttack > 2)
            m_currentAttack = 1;

        m_animator.SetTrigger("AirAttack_mirror");



    }
    public void air_attack_anim()
    {



        if (m_currentAttack > 2)
            m_currentAttack = 1;

        m_animator.SetTrigger("AirAttack");




    }
    public void attack_combo()
    {
        m_animator.SetTrigger("sword_combo");
    }
    public void set_airspeed()
    {
        m_animator.SetFloat("AirSpeedY", rgd.velocity.y);
    }
    public void jump_anim()
    {
        m_animator.SetTrigger("Jump");
        m_animator.SetBool("Grounded", false);
    }
    public void dash()
    {
        m_animator.SetBool("mustbeing", true);
        m_animator.SetBool("dash", true);
        melee_initialize();
    }
    public void melee_initialize()
    {
        this.GetComponent<Player_shadow_anim_event>().Melee_1_off();
        this.GetComponent<Player_shadow_anim_event>().Melee_2_off();
        this.GetComponent<Player_shadow_anim_event>().Melee_3_off();
        this.GetComponent<Player_shadow_anim_event > ().air_Melee_off();
    }
    public void dash_end()
    {
        m_animator.SetBool("mustbeing", false);
        m_animator.SetBool("dash", false);
    }
    void set_anim_Chk()
    {
        p_s.anim_chk = true;
    }
    void Start()
    {
        p_s = this.transform.parent.GetComponent<Player_shadow>();
        m_animator = GetComponent<Animator>();
        rgd = GetComponentInParent<Rigidbody2D>();
    }
    public void idle_anim()
    {
        m_animator.SetInteger("AnimState", 0);

    }
    public void move_anim()
    {
        m_animator.SetInteger("AnimState", 1);

    }
}
