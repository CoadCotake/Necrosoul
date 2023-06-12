using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_AI_01 : Unit
{
    
    float move_vel;
    public float move_vel_start;
    public float move_vel_plus;

    public float move_vel_limit_start;
    public float move_vel_limit_plus;
    float move_vel_limit;

    public bool on_main;
    public float start_main_dir;

    public bool on_groggy;
    float groggy_gauge;
    float groggy_gauge_max;
    public float groggy_time;
    float groggy_timer;

    public bool on_protected;
    public float protect_broken_time;
    float protect_broken_timer;
    //우선순위  0순위 메인-1순위:소환패턴(유령수 적을때)-2순위 장판(장판지속시간이 끝났을 때)-3순위 탄막
    public float d_magni;
   float vel_limit;
    public float vel_limit_start;
    public float vel_limit_plus;
    public float m_dir;
    GameObject Player;
    Vector3 dir;
    Rigidbody2D rgd;
  
    bool attack_status;
    Animator e_ani;
    Enemy_status E_Status;
    public float enemy_size_x;
    public float enemy_size_y;
    public Transform pulling_List;
    public List<Transform> created_ghost_tp = new List<Transform>();
    public GameObject create_ghost;
    int live_ghost_num;
    List<GameObject> create_ghost_list = new List<GameObject>();
    public GameObject create_bullet;
    List<GameObject> create_bullet_list = new List<GameObject>();
    public float fire_delay;
    float fire_delay_timer;
    public GameObject create_wall;
    List<GameObject> create_wall_list = new List<GameObject>();
    List<Vector3> wall_pos = new List<Vector3>();
    public List<node> path;

    GameObject get_pulling_objct(List<GameObject> l)
    {
        for(int i = 0; i < l.Count; i++)
        {
            if (!l[i].activeSelf)
            {
                return l[i];
            }
        }
        return l[0];
        
    }
    void enemy_camera_fitting()
    {
        var viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewport_pos.x < -0.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);
        if (viewport_pos.y < -0.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);
        if (viewport_pos.x > 1.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);
        if (viewport_pos.x > 1.15) rgd.AddForce(rgd.velocity * -1.01f, ForceMode2D.Impulse);


        if (transform.rotation.y > 360)
            transform.Rotate(0, -360, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(enemy_size_x, enemy_size_y));
    }
    private void Start()
    {
        on_protected = true;
        rgd = GetComponent<Rigidbody2D>();
        vel_limit = vel_limit_start;
       
        attack_status = true;
        e_ani = this.transform.GetChild(1).GetComponent<Animator>();
        E_Status = this.gameObject.GetComponent<Enemy_status>();
        E_Status.set_layout(1);
        can_hitted_ani = false;
        max_hp = E_Status.get_max_hp();
        Health_point = E_Status.get_hp();
        Defense_point = E_Status.get_defense_point();
       move_speed = E_Status.get_speed();
      Attack_point = E_Status.get_atk();
      size_x = enemy_size_x;
        groggy_gauge_max = max_hp * 0.1f;
        groggy_gauge = groggy_gauge_max;
        size_y = enemy_size_y;
        for(int i = 0; i < 6;i++)
        {
            var a = Instantiate(create_ghost, pulling_List);
            create_ghost_list.Add(a);
            a.SetActive(false);
        }
        for (int i = 0; i < 6; i++)
        {
            var a = Instantiate(create_ghost, pulling_List);
            create_ghost_list.Add(a);
            a.SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            var a = Instantiate(create_wall, pulling_List);
            create_wall_list.Add(a);
            a.SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            var a = Instantiate(create_bullet, pulling_List);
            create_bullet_list.Add(a);
            a.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
      
        }
        else
        {
            dir = Player.transform.position - this.transform.position;
            d_magni = dir.magnitude;
        }
    }
    private void FixedUpdate()
    {
        brain();
    }
    void brain()
    {
        if (Health_point > 0)
        {
            enemy_camera_fitting();
            if (fire_delay_timer > 0)
            {
                fire_delay_timer -= Time.deltaTime;
            }
            if (!on_main)
            {
                if (d_magni > start_main_dir)
                {
                    if (live_ghost_num < 2)
                    {
                        atk_ai_1();
                    }
                    else if(fire_delay_timer<=0)
                    {
                        atk_ai_0();
                    }
                    else
                    {
                        move_ai_0();
                    }
                }
                else
                {
                    if(d_magni!=0)
                    on_main = true;
                }
            }
            else
            {
                atk_ai_2();
            }
        }
    }
    void move_ai_0()
    {
        transform.Translate(dir.normalized *move_speed * Time.deltaTime);

    }
    void atk_ai_0()
    {
        var a = get_pulling_objct(create_bullet_list);
        a.GetComponent<b_1_bullet>().Dir = dir;
        a.transform.position = this.transform.position;
        a.SetActive(true);
        fire_delay_timer = fire_delay;
 
    }
    void atk_ai_1()
    {
        for(int i = 0; i < 3; i++)
        {
            var a = get_pulling_objct(create_ghost_list);
            a.SetActive(true);
            a.transform.position = created_ghost_tp[i].position;
            live_ghost_num++;
            //a에다가 플레이어 추격 달기
        }
    }
    void atk_ai_2()
    {
        if (path.Count != 0)
        {
            var dir_2 = path[1].pos - (Vector2)this.transform.position;
            if (rgd.velocity.magnitude < vel_limit)
                rgd.AddForce(dir_2.normalized * (move_speed + move_vel));
        }
        

        if (move_vel < move_vel_limit)
        {
            move_vel += move_vel_plus*Time.deltaTime;
        }
    }
    void atk_ai_3()
    {
       
       var viewport_pos = Camera.main.WorldToViewportPoint(Player.transform.position);
        int mugen = 0;
        while (wall_pos.Count < 4)
        {
            mugen++;
            if (mugen > 100)
            {
                Debug.Log("100번찍어 안넘어갔거나 무한루프거나");
                break;
            }
            var rand_x = Random.Range(-1, 1);
            var rand_y = Random.Range(-1, 1);
            Vector3 a = new Vector3(viewport_pos.x + rand_x, viewport_pos.y + rand_y, viewport_pos.z);
            var pos = Camera.main.ViewportToWorldPoint(a);
            bool wall_multiple_chk=true;
            if (wall_pos.Count != 0)
            {
                for(int i = 0; i > wall_pos.Count; i++)
                {
                    if((wall_pos[i].x-1<pos.x&&wall_pos[i].x+1>pos.x)|| (wall_pos[i].y- 1 < pos.y && wall_pos[i].y + 1 > pos.y))//1=벽의 넓이,높이
                    {
                        wall_multiple_chk = false;
                        break;
                    }
                }
            }
            if (!wall_multiple_chk)
            {
                return;
            }
            Collider2D box = Physics2D.OverlapBox(pos, new Vector2(1, 1), 0);//1=벽의 넓이,높이
            if (box != null)
            {
                if (box.gameObject.layer == 12)
                {
                    return;
                }
                else
                {
                    var wall = get_pulling_objct(create_wall_list);
                    wall.transform.position = pos;
                    wall.SetActive(true);
                    wall_pos.Add(pos);
                }

            }
            else
            {
                var wall = get_pulling_objct(create_wall_list);
                wall.transform.position = pos;
                wall.SetActive(true);
                wall_pos.Add(pos);
            }
          
        }
        
    }
    new void OnTriggerEnter2D(Collider2D other)
    {
        if (!on_groggy)
        {
            if (other.tag == "melee")
            {
                if (on_protected)
                {

                    hitted_on_protected(other.gameObject);
                }
                else
                {

                    hitted_on_unprotected(other.gameObject);
                }
            }
        }
        else
        {
            if (other.tag == "melee")
            {
                hitted_on_groggy(other.gameObject);
            }
        }
    }
    void on_hitted_melee(GameObject a,float b)
    {
        if (!a.GetComponent<melee_attack>().E.Contains(this) && !a.GetComponent<melee_attack>().disable_hit)
        {
            hitted_material_timer = hitted_material_time;
            var me = a.GetComponent<melee_attack>();

            me.E.Add(this);
            Gamemanager.GM.get_combo();
            if (Player_status.p_status.critical())
            {
                damaged = character_lose_health(Mathf.RoundToInt((a.GetComponent<melee_attack>().Damage * Player_status.p_status.get_critical_damage()) * b), DNP, a.gameObject.transform);
                var par = Instantiate(Gamemanager.GM.enemy_hitted_critical_particle);
                par.transform.position = this.transform.position;
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
                damaged = character_lose_health(Mathf.RoundToInt(a.GetComponent<melee_attack>().Damage * b), DNP, a.gameObject.transform);
                var par = Instantiate(Gamemanager.GM.enemy_hitted_particle);
                par.transform.position = this.transform.position;
                if (direction == 1)
                {
                    par.transform.Rotate(0, -180, 0);
                }
                else
                {

                }
            }
            if (Health_point <= 0)
            {
                me.E.Remove(this);
            }
            Gamemanager.GM.game_ev.when_Enemy_hitted(damaged, this);
            groggy_gauge -= damaged;
            record.Damge += me.Damage;
        }
    }
    void hitted_on_protected(GameObject a)
    {
        if(a.tag == "melee")
        {
            on_hitted_melee(a.gameObject, 0.5f);
        }
    }
    void hitted_on_unprotected(GameObject a)
    {
        if (a.tag == "melee")
        {
            move_vel = move_vel_start;
            hitted_forced_melee(a.GetComponent<melee_attack>(), Player.transform.position - this.transform.position);
            on_hitted_melee(a.gameObject, 1);
        }
    }
    void hitted_on_groggy(GameObject a)
    {
        if (a.tag == "melee")
        {
            move_vel = move_vel_start;
            hitted_forced_melee(a.GetComponent<melee_attack>(), Player.transform.position - this.transform.position);
            on_hitted_melee(a.gameObject, 1.5f);
        }
    }
    void hitted_forced_melee(melee_attack a, Vector3 d)
    {
        Debug.Log("근접에 의해 밀려남:" + a.melee_force);
        rgd.velocity = Vector3.zero;

        if (d.x > 0)
        {
            rgd.AddForce(new Vector2(-1 * a.melee_force, 0), ForceMode2D.Impulse);
        }
        else
        {
            rgd.AddForce(new Vector2(a.melee_force, 0), ForceMode2D.Impulse);
        }
    }
    new void hitted_SP(int a)
    {
       
        Gamemanager.GM.get_combo();

        //피격트리거 온
        /*  if (can_forced)
          {
              hitted_forced_melee(me);
          }*/

        this.gameObject.GetComponent<Enemy_UI>().Hit();
        //  Ins=Instantiate(prefab);
        // Ins.transform.position = this.transform.position;
        if (Player_status.p_status.critical())
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
    void hitted_on_player(GameObject a)
    {
        var b= a.GetComponent<PlayerCharacter>();
        b.can_attack = false;
        b.can_move = false;
        this.transform.position = a.transform.position+Vector3.up*6;
    }
}
