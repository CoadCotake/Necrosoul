using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_anim_event : MonoBehaviour
{
    [Header("Effects")]
    public float melee_force;
    public float melee_1_anim_time;
    public float melee_2_anim_time;
    public float melee_3_anim_time;
    bool landing_ef_chk;
    public AnimationClip melee_1_clip;
    public AnimationClip melee_1_hold_clip;
    public AnimationClip melee_2_clip;
    public AnimationClip melee_2_hold_clip;
    public AnimationClip melee_3_clip;
    public AnimationClip melee_3_hold_clip;
    public AnimationClip air_melee_clip;
    public AnimationClip air_melee_hold_clip;
    public GameObject Player_ghost;
    public List<GameObject> Player_ghost_instansi=new List<GameObject>();
    public Transform created_list;
    public GameObject m_RunStopDust;
    public GameObject m_JumpDust;
    public GameObject m_LandingDust;
    public GameObject m_DodgeDust;
    public GameObject m_WallSlideDust;
    public GameObject m_WallJumpDust;
    public GameObject m_AirSlamDust;
    public GameObject m_ParryEffect;
    public GameObject melee_1;
    public GameObject melee_1_instani;
    public GameObject melee_2;
    public GameObject melee_2_instani;
    public GameObject melee_3;
    public GameObject melee_3_instani;
    public GameObject air_melee_;
    public GameObject air_melee_instani;
    public float melee_1_reaction;
    public float melee_2_reaction;
    public float melee_3_reaction;
    public int melee_dmg;
    private AudioManager_PrototypeHero m_audioManager;
    public PlayerCharacter m_player;
    melee_attack index;
    public Player_animator p_anim;

    void AE_set_melee_attack_dmg()
    {

        melee_dmg= Gamemanager.GM.game_ev.when_P_A_Key_input(Player_status.p_status.get_atk());
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            var a= Instantiate(Player_ghost);
            a.transform.GetChild(0).GetComponent<Player_shadow>().p_created = true;
            Player_ghost_instansi.Add(a);
            a.transform.SetParent(created_list);
            a.SetActive(false);
        }
        m_player= GetComponentInParent<PlayerCharacter>();
        p_anim = GetComponent<Player_animator>();
        m_audioManager = AudioManager_PrototypeHero.instance;
    }
 
    GameObject player_ghost_pulling()
    {
        int index=0;
        for(int i = 0; i < Player_ghost_instansi.Count; i++)
        {
            if (!Player_ghost_instansi[i].activeSelf)
            {
                index = i;
            }
        }
        return Player_ghost_instansi[index];
    }
    // Animation Events
    // These functions are called inside the animation files
    void player_ghost_attack_1_effect()
    {
        if (melee_1_instani.GetComponent<melee_attack>().Double_attack_on)
        {

                var a = player_ghost_pulling();
                
                var b = a.transform.GetChild(0).GetComponent<Player_shadow>();
                b.shadow_original_timer = melee_1_clip.length ;
                b.shadow_time = melee_1_clip.length;
                b.once_chk = true;
                b.anim_chk = false;
                b.shadow_type = true;
                b.animation_level = 3;
                a.SetActive(true);
  
          
        }
    }
    void player_ghost_attack_2_effect()
    {
        if (melee_2_instani.GetComponent<melee_attack>().Double_attack_on)
        {
            var a = player_ghost_pulling();

            var b = a.transform.GetChild(0).GetComponent<Player_shadow>();
            b.shadow_original_timer = melee_1_clip.length;
            b.shadow_time = melee_1_clip.length;
            b.once_chk = true;
            b.anim_chk = false;
            b.shadow_type = true;
            b.animation_level = 1;
            a.SetActive(true);
        }
    }
    void player_died()
    {
        m_player.death();
    }
    void player_resurrection()
    {

        m_player.ressurection();
    }
    void player_ghost_attack_3_effect()
    {
        if (melee_3_instani.GetComponent<melee_attack>().Double_attack_on)
        {
            var a = player_ghost_pulling();

            var b = a.transform.GetChild(0).GetComponent<Player_shadow>();
            b.shadow_original_timer = melee_1_clip.length;
            b.shadow_time = melee_1_clip.length;
            b.once_chk = true;
            b.anim_chk = false;
            b.shadow_type = true;
            b.animation_level = 2;
            a.SetActive(true);
        }
    }
    void player_air_ghost_attack_effect()
    {
        if (air_melee_instani.GetComponent<melee_attack>().Double_attack_on)
        {
            var a = player_ghost_pulling();

            var b = a.transform.GetChild(0).GetComponent<Player_shadow>();
            b.shadow_original_timer = melee_1_clip.length;
            b.shadow_time = melee_1_clip.length;
            b.once_chk = true;
            b.anim_chk = false;
            b.shadow_type = true;
            b.animation_level = 11;
            a.SetActive(true);
        }
    }
   void hitted_push(float x)
    {
        
        var p = this.transform.parent.GetComponent<Rigidbody2D>();
       
        Vector3 v3 = new Vector3(x, 0, 0);

        p.AddForce(v3*m_player.direction, ForceMode2D.Impulse);
    }
    void hitted_push_end()
    {
        var p = this.transform.parent.GetComponent<Rigidbody2D>();
        p.velocity = new Vector3(0, p.velocity.y);
    }
        void Melee_1_on()
    {





            hitted_push(3f);
        
        if (melee_1_instani == null)
        {
           
            var a = Instantiate(melee_1, melee_1.transform.position, Quaternion.identity);
            index = a.GetComponent<melee_attack>();
            index.melee_force = melee_force*3;
            index.set_effect_rotate = true;
            index.effect_rotation = 0;
            a.transform.rotation = this.transform.parent.rotation;
            index.Damage = melee_dmg;
            a.transform.SetParent(created_list);
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);

            a.SetActive(true);
            melee_1_instani = a;
            player_ghost_attack_1_effect();
        }
        else
        {
            melee_1_instani.transform.position = melee_1.transform.position;
            melee_1_instani.transform.rotation = this.transform.parent.rotation;
            index = melee_1_instani.GetComponent<melee_attack>();
            index.melee_force = melee_force*3;
            index.on_crit = false;
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            index.disable_hit = false;
            index.Damage = melee_dmg;
            melee_1_instani.SetActive(true);
            player_ghost_attack_1_effect();
        }
       // melee_1.SetActive(true);
    }
    public void Melee_1_off()
    {
        var p = this.transform.parent.GetComponent<Rigidbody2D>();



        p.velocity = new Vector3(0, p.velocity.y);
        if (melee_1_instani!=null)
            melee_1_instani.SetActive(false);

        hitted_push_end();
    }
    void air_melee_on()
    {
       
        
        if (air_melee_instani == null)
        {
           
            var a = Instantiate(air_melee_, air_melee_.transform.position, Quaternion.identity);

            index = a.GetComponent<melee_attack>();
            index.melee_force = melee_force * 2;
            a.transform.rotation = this.transform.parent.rotation;
            index.Damage = melee_dmg;
            index.set_effect_rotate = true;
            index.effect_rotation = 0;
            a.transform.SetParent(created_list);
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            air_melee_instani = a;
   
            a.SetActive(true);

           
            
            player_air_ghost_attack_effect();
         
        }
        else
        {
           air_melee_instani.transform.position = air_melee_.transform.position;
            air_melee_instani.transform.rotation = this.transform.parent.rotation;
            index = air_melee_instani.GetComponent<melee_attack>();
            index.melee_force = melee_force*2;
            index.on_crit = false;
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            index.disable_hit = false;
            index.Damage = melee_dmg;
            air_melee_instani.SetActive(true);
            player_air_ghost_attack_effect();
        }
    }
    public void air_Melee_off()
    {
        if (air_melee_instani != null)
            air_melee_instani.SetActive(false);

      
    }
    void Melee_2_on()
    {

        if (m_player.on_rush)
        {
            hitted_push(2.5f);
            m_player.on_rush = false;
        }
        else
        {

            hitted_push(1f);
        }

       
        if (melee_2_instani == null)
        {
            
         
            var a = Instantiate(melee_2, melee_2.transform.position, Quaternion.identity);
            index = a.GetComponent<melee_attack>();
            index.melee_force = melee_force;
            index.Damage = melee_dmg;
            index.set_effect_rotate = true;
            a.transform.rotation = this.transform.parent.rotation;
            index.effect_rotation = -0.2f;
            a.transform.SetParent(created_list);
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            a.SetActive(true);
            melee_2_instani = a;
            // melee_2.SetActive(true);
            player_ghost_attack_2_effect();
        }
        else
        {
            melee_2_instani.transform.position = melee_2.transform.position;
            melee_2_instani.transform.rotation = this.transform.parent.rotation;
            index = melee_2_instani.GetComponent<melee_attack>();
            index.melee_force = melee_force;
            index.on_crit = false;
            index.Damage = melee_dmg;
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            index.disable_hit = false;
            
            melee_2_instani.SetActive(true);
            player_ghost_attack_2_effect();
        }
    }
    public void Melee_2_off()
    {
        if (melee_2_instani != null)
            melee_2_instani.SetActive(false);
        hitted_push_end();
    }
    void Melee_3_on()
    {
        hitted_push(1f);
        if (melee_3_instani == null)
        {
            
            var a = Instantiate(melee_3, melee_3.transform.position, Quaternion.identity);
            index = a.GetComponent<melee_attack>();
            index.melee_force = melee_force;
            index.Damage = melee_dmg;
            a.transform.rotation = this.transform.parent.rotation;
            index.set_effect_rotate = true;
            index.effect_rotation = 90;
            a.transform.SetParent(created_list);
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            a.SetActive(true);
            melee_3_instani = a;
            player_ghost_attack_3_effect();
        }
        else
        {
            melee_3_instani.transform.position = melee_3.transform.position;
            melee_3_instani.transform.rotation = this.transform.parent.rotation;
            index = melee_3_instani.GetComponent<melee_attack>();
            index.melee_force = melee_force;
            index.on_crit = false;
            index.Damage = melee_dmg;
            Gamemanager.GM.game_ev.when_P_Attack_effect(index);
            index.disable_hit = false;
            melee_3_instani.SetActive(true);
            player_ghost_attack_3_effect();
        }
        // melee_3.SetActive(true);
    }
    public void Melee_3_off()
    {
        if (melee_3_instani != null)
            melee_3_instani.SetActive(false);
        hitted_push_end();
    }
    void AE_runStop()
    {
        m_audioManager.PlaySound("RunStop");
        float dustXOffset = 0.6f;
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
    }

    void AE_footstep()
    {
        jump_return();
        m_audioManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        m_audioManager.PlaySound("Jump");

      
        
            float dustYOffset = 0.078125f;
            m_player.SpawnDustEffect(m_JumpDust, 0.0f, dustYOffset);
        
        
    }
   void jump_return()
    {
        if (m_player.jump_count != Player_status.p_status.get_jump_count())
            m_player.jump_count = Player_status.p_status.get_jump_count();
    }
    void AE_Landing()
    {
        if (m_player.landing_chk)
        {
            jump_return();
            p_anim.ground_anim_chk = false;
            if (p_anim.ground_anim_chk)
            {
                m_audioManager.PlaySound("Landing");
                float dustYOffset = 0.078125f;
                m_player.SpawnDustEffect(m_LandingDust, 0.0f, dustYOffset);
                p_anim.ground_anim_chk = false;
            }
        }
    }

 

    void AE_AttackAirLanding()
    {
        m_audioManager.PlaySound("AirSlamLanding");
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_AirSlamDust, 0.0f, dustYOffset);

    }

    void AE_Hurt()
    {
        m_audioManager.PlaySound("Hurt");
        p_anim.Hit_state = false;
    }

    void AE_Death()
    {
        m_audioManager.PlaySound("Death");
    }

    void AE_SwordAttack()
    {
        m_audioManager.PlaySound("SwordAttack");
    }

    void AE_SheathSword()
    {
        m_audioManager.PlaySound("SheathSword");
    }

  
}
