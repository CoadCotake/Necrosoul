using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : GameCharacter
{
   public float size_x;
   public float size_y;
   // List<GameObject> Particle_pulling;
  public Color unit_hitted_color;
    public float hitted_material_time;
    public float hitted_material_timer;
    Material this_material;
    public int enemy_rank;//0=common 1=elite 2=boss
    public bool e_active;
    public float active_timer;
    public float active_time = 1.5f;
    public GameObject Player;
    public GameObject Destroy_effect;
    public Animator e_ani;
    Color color;
    protected int damaged;
    bool on_hiited_force;
    public bool sentinal;
    public GameObject DNP;
    public bool onGround=true;
    public ProgressBarPro progressBar;
    public ActionRecord record;
    Rigidbody2D rgd;
    public bool can_forced;
    public List<Vector2> hitted_force_list = new List<Vector2>();
    public Vector2 hitted_force;
    public float manner_time;
    float manner_timer;
    GameObject enemy_pos;
    private void Awake()
    {
        // E_Status = this.transform.parent.gameObject.GetComponent<Enemy_status>();
       
        DNP = GameObject.Find("DemoManager");
    }
   /* GameObject get_particle_pulling()
    {
        for(int i = 0; i < Particle_pulling.Count; i++)
        {
            if (!Particle_pulling[i].activeSelf)
            {
                return Particle_pulling[i];
            }
        }
        Particle_pulling[0].SetActive(false);
        return Particle_pulling[0];
    }*/
    void Start()
    {
        /* for(int i = 0; i < 5; i++)
         {
             var a = Instantiate(Gamemanager.GM.enemy_hitted_particle.gameObject);
             Particle_pulling.Add(a);
             a.SetActive(false);

             a.transform.SetParent(this.transform);

         }*/
        if (Gamemanager.GM.Enemy_destroy_effect != null)
        {
            Destroy_effect = Gamemanager.GM.Enemy_destroy_effect;
        }
        this_material = this.transform.GetChild(1).GetComponent<SpriteRenderer>().material;
        can_forced = true;
        if(hitted_force_list.Count>0)
            hitted_force = hitted_force_list[0];
        record = GameObject.Find("GameSystem").GetComponent<Record>().ActionRecord;
        DNP = GameObject.Find("DemoManager");
        Player = Gamemanager.GM.Player_obj;
        can_move = true;
        can_attack = false;
        rgd = GetComponent<Rigidbody2D>();
        prefab = Resources.Load("Ef") as GameObject;
        color = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color;
        unit_hitted_color = new Color(1, 0.3f, 0.3f);
        hitted_material_time = 0.5f;
        if (enemy_pos == null)
        {
            GameObject ins = Instantiate(Gamemanager.GM.enemy_minimap_pos);
            ins.transform.position = this.transform.position;
            ins.transform.SetParent(this.transform);
        }
    }
    void FixedUpdate()
    {
        
        if (manner_timer > 0)
        {
            manner_timer -= Time.deltaTime;
            can_attack = false;
        }
        if (hitted_material_timer > 0)
        {
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().color = unit_hitted_color;
            hitted_material_timer -= Time.deltaTime;
        }
        else
        {
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
        }
        if (GameObject.FindGameObjectWithTag("Player_illusion")==null)
        {

            Player = Gamemanager.GM.Player_obj;
        }
        else
        {
            Player = GameObject.FindGameObjectWithTag("Player_illusion");
        }
        if (!e_active)
        {
            active_enemy();
        }
        if (on_hiited_force&&onGround)
        {
            var bottom_ray = Physics2D.Raycast(transform.position + Vector3.right * (size_x / 2) * direction, Vector3.down, size_y / 2 + 0.4f, LayerMask.GetMask("platform_can't_pass"));
            var bottom_ray_2 = Physics2D.Raycast(transform.position + Vector3.right * (size_x / 2) * direction, Vector3.down, size_y / 2 + 0.4f, LayerMask.GetMask("platform_can_pass"));
           
            if (bottom_ray.collider == null && bottom_ray_2.collider == null)
            {
                rgd.velocity = Vector3.zero;
                on_hiited_force = false;
            }
            if (rgd.velocity.x == 0)
            {
                on_hiited_force = false;
            }
        }
       /* if (Player != null)
        {
            if (transform.position.x - Player.transform.position.x >= 0)
            {
                if (direction == -1)
                {
                    direction_change ();
                    direction = -1;
                }
            }
            else
            {
                if (direction == 1)
                {
                    direction_change();
                    direction = 1;
                }
            }
        }*/

        }
    private void OnEnable()
    {
        manner_timer = manner_time;
    }
    void active_enemy()
    {
        if (active_timer < active_time)
        {
            color.a = active_timer;
            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
            active_timer += Time.deltaTime;
        }
        else
        {
            color.a = 1;
            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = color;
            e_active = true;
        }
    }
    public GameObject prefab;
    GameObject Ins;

    public bool can_hitted;

    public bool can_hitted_ani;
    private bool death_chk;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (enemy_rank != 3)
        {
            if (e_active && Health_point > 0)
            {

                if (other.tag == "Bullet")
                {
                    if (can_hitted_ani)
                    {
                        hitted_animation();
                    }
                    hitted_range(other.gameObject);
                }
                else if (other.tag == "melee")
                {
                    if (can_hitted_ani)
                    {
                        hitted_animation();
                    }
                    hitted_melee(other.gameObject,other);
                }
            }
        }
    }
     void hitted_forced_melee(melee_attack a,Vector3 d)
    {
        Debug.Log("근접에 의해 밀려남:" + a.melee_force);
        rgd.velocity = Vector3.zero;
        on_hiited_force = true;
        if (d.x > 0) {
            rgd.AddForce(new Vector2(-1*a.melee_force, 0), ForceMode2D.Impulse);
        }
        else
        {
            rgd.AddForce(new Vector2(a.melee_force, 0), ForceMode2D.Impulse);
        }
    }
    void hitted_forced_sp(float a)
    {
        on_hiited_force = true;
        rgd.velocity = Vector3.zero;
        rgd.AddForce(new Vector2(a, 0), ForceMode2D.Impulse);
    }
    void hitted_animation()
    {
        if (this.transform.GetChild(1).GetComponent<Animator>() != null)
        {
            e_ani = this.transform.GetChild(1).GetComponent<Animator>();
            e_ani.SetTrigger("hitted");
            e_ani.SetTrigger("death");
        }
    }
    void hitted_range(GameObject a)
    {
        hitted_material_timer = hitted_material_time;
        Animator ani = transform.GetChild(1).GetComponent<Animator>();
        //피격트리거 온
        var le = this.transform.position - a.transform.position;
        if (direction == 1)
        {
            if (le.x > 0)
            {
                direction_change_spr();
            }
        }
        else
        {
            if (le.x < 0)
            {
                direction_change_spr();
            }
        }
        Debug.Log("데미지 입힘");
        this.gameObject.GetComponent<Enemy_UI>().Hit();
        // Ins=Instantiate(prefab);
        //Ins.transform.position = this.transform.position;
       
        if (Player_status.p_status.critical())
        {
           damaged= character_lose_health(Mathf.RoundToInt((a.GetComponent<Bullet>().Damge * Player_status.p_status.get_critical_damage())), DNP, a.gameObject.transform);
        }
        else
        {
            damaged = character_lose_health(a.GetComponent<Bullet>().Damge, DNP, a.gameObject.transform);
        }
        Gamemanager.GM.game_ev.when_Enemy_hitted(damaged, this);
        sentinal = true;
      progressBar.SetValue(Health_point, max_hp, true);
        a.GetComponent<Bullet>().DestroyBullet();
        record.Damge += a.GetComponent<Bullet>().Damge;
    }
   public void hitted_melee(GameObject a,Collider2D col)
    {
        if (!a.GetComponent<melee_attack>().E.Contains(this)&&!a.GetComponent<melee_attack>().disable_hit)
        {
            hitted_material_timer = hitted_material_time;
            var me = a.GetComponent<melee_attack>();

        me.E.Add(this);
            Gamemanager.GM.get_combo();
            var le = Player.transform.position - this.transform.position;
            if (direction == 1)
            {
                if (le.x > 0)
                {
                    direction_change_spr();
                }
               
            }
            else
            {
                if (le.x < 0)
                {
                    direction_change_spr();
                }
               
            }

            Vector2 hit_pos = col.ClosestPoint(a.transform.position);
          
            //피격트리거 온
            if (can_forced)
            {
                hitted_forced_melee(me,Player.transform.position-this.transform.position);
            }
            Debug.Log("데미지 입힘"+ me);
            this.gameObject.GetComponent<Enemy_UI>().Hit();
            //  Ins=Instantiate(prefab);
            // Ins.transform.position = this.transform.position;
            if (Player_status.p_status.critical()/*||me.on_crit*/)
            {
                //me.on_crit = true;
                Font_manager.DN.SpawnText(2, "Critical", a.gameObject.transform);
                damaged = character_lose_health(Mathf.RoundToInt((me.Damage * Player_status.p_status.get_critical_damage())), DNP, a.gameObject.transform);
                var par = Instantiate(Gamemanager.GM.enemy_hitted_critical_particle);
                par.transform.position = this.transform.position;
                if (me.sword_effect.gameObject.activeSelf)
                {
                    me.sword_effect.gameObject.SetActive(false);
                }
                me.sword_effect.GetComponent<p_sword_hitted_particle>().crit = true;
                me.sword_effect.transform.position = this.transform.position;
                me.sword_effect.gameObject.SetActive(true);

                if (direction == 1)
                {
                    par.transform.Rotate(0, -180, 0);
                }
                else
                {

                }
            }
            else
            {
                damaged = character_lose_health(me.Damage, DNP, a.gameObject.transform);
                var par = Instantiate(Gamemanager.GM.enemy_hitted_particle);
                par.transform.position = this.transform.position;
                if (direction == 1)
                {
                    par.transform.Rotate(0, -180, 0);
                }
                else
                {

                }
                if (me.sword_effect.gameObject.activeSelf)
                {
                    me.sword_effect.gameObject.SetActive(false);
                }
                //me.sword_effect.transform.position = hit_pos;
                me.sword_effect.transform.position = this.transform.position;
                me.sword_effect.gameObject.SetActive(true);

            }
            if (Health_point <= 0)
            {
                me.E.Remove(this);
            }
            Gamemanager.GM.game_ev.when_Enemy_hitted(damaged, this);
            sentinal = true;
            if (progressBar == null)
            {
                progressBar = GetComponent<Enemy_UI>().GetBar();
                progressBar.SetBarColor(Color.red);
            }
            progressBar.SetValue(Health_point, max_hp, true);
            
            record.Damge += me.Damage;
        }
    }
    public void hitted_SP(int a)
    {
        hitted_material_timer = hitted_material_time;
        Gamemanager.GM.get_combo();
        hitted_forced_sp(1f);
        //피격트리거 온
      /*  if (can_forced)
        {
            hitted_forced_melee(me);
        }*/
 
        this.gameObject.GetComponent<Enemy_UI>().Hit();
        //  Ins=Instantiate(prefab);
        // Ins.transform.position = this.transform.position;
        if (Player_status.p_status.critical() )
        {

            damaged = character_lose_health(Mathf.RoundToInt((a * Player_status.p_status.get_critical_damage())), DNP, this.transform);
        }
        else
        {
            damaged = character_lose_health(a, DNP, this.transform);
        }
        Gamemanager.GM.game_ev.when_Enemy_hitted(damaged, this);
        sentinal = true;
        progressBar.SetValue(Health_point, max_hp, true);

        record.Damge += a;
    }
    public void death()
    {
        if (!death_chk)
        {
            e_active = false;
            death_chk = true;
            Enemy_status e = GetComponent<Enemy_status>();
            int Total = e.get_money();
            Gamemanager.GM.game_ev.when_get_money(Total);
            Font_manager.DN.SpawnNumber(13, Total, this.gameObject.transform);
            record.EnemyKill++;
            this.gameObject.GetComponent<Enemy_UI>().DestoryUI();
            var par = Gamemanager.GM.destroy_particle_pulling();
            par.transform.position = this.transform.position;
            par.SetActive(true);
            Instantiate(Destroy_effect, this.transform.position, Quaternion.identity);

            if (this.transform.parent.parent.GetComponent<Enemy_group>() != null)
            {
                Enemy_group a = this.transform.parent.parent.GetComponent<Enemy_group>();
                a.enemy.Remove(this.transform.parent.gameObject);
                Debug.Log("aaaaa");
            }
            
            Gamemanager.GM.game_ev.Enemy_death(this.transform);
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
    public void HpCheack()
    {
        if (Health_point <= 0&& !death_chk)
        {
            death();

        }
    }

}

