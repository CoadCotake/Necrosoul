using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_07_AI : MonoBehaviour
{

    public List<node> path;
    Enemy_status E_Status;
    Vector2 dir;
    private Quaternion rotation;
    public List<Transform> create_position = new List<Transform>();
    public float attack_time;
    bool attack_status;

    public float bullet_size;
    float attack_weight;
    Unit unit;
    Rigidbody2D rgd;
    public GameObject Player;
    public GameObject attack_range;//if attack_range.bool=true ->attack
    public List<GameObject> create_object = new List<GameObject>();
    public List<GameObject> created_object = new List<GameObject>();
    public E_07_range E_range;
    Pathfinding_E_07 p_e_07;
    float move_distance;
    public float move_distance_max;
    public float enemy_size_x;
    public float enemy_size_y;
    public float moving_buffer;
    float moving_weight;
    public E_07_chase_range range_distance;
    public bool can_chase;
    bool move_corutine_check;
    bool idle_corutine_check;
    bool moving_status;
    public float idle_time;
    Animator e_ani;
    public bool move_strict;
    public float attack_delay;
    Vector2 node_dir;
    public float wall_bounce_force;
    public float move_force;
    public bool on_attack;
    float s_ran;
    int num;
    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(enemy_size_x, enemy_size_y));
    }
    void Start()
    {
        p_e_07 = this.GetComponent<Pathfinding_E_07>();
        rgd = GetComponent<Rigidbody2D>();
        s_ran = Random.Range(-0.5f, 0.5f);
        unit = this.GetComponent<Unit>();
        attack_status = true;
        e_ani = this.transform.GetChild(1).GetComponent<Animator>();
        E_Status = this.gameObject.GetComponent<Enemy_status>();
        E_Status.set_layout(4);
        unit.can_hitted_ani = false;
        unit.max_hp = E_Status.get_max_hp();
        unit.Health_point = E_Status.get_hp();
        unit.Defense_point = E_Status.get_defense_point();
        unit.move_speed = E_Status.get_speed();
        unit.Attack_point = E_Status.get_atk();
        unit.size_x = enemy_size_x;
        unit.size_y = enemy_size_y;
    }
    void ray_to_player()
    {

        if (range_distance.on_player)
        {
            can_chase = true;
        }
        else
        {
            can_chase = false;
        }
        if (E_range.on_player)
        {
            unit.can_attack = true;

        }
        else
        {

            unit.can_attack = false;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        brain();
    }
    void brain()//ai 정리
    {
        if (unit.onGround)
        {
            unit.onGround = false;
        }
        if (rgd.velocity.magnitude > 5)
        {
            rgd.velocity *= 0.95f;
        }
        if (unit.can_forced)
        {
            unit.can_forced = false;
        }
        if (unit.Health_point > 0)
        {
            ray_to_player();
            if (on_attack)
            {
                rgd.velocity = Vector3.zero;
            }
           

            if (unit.can_attack)//벽에 안막힘+사정거리안
            {

                //StartCoroutine("attack");//공격+움직이지 않음
                if (attack_delay <= 0)
                {
                    attack();
                    E_range.on_player = false;
                }
                else
                {
                    
                }
               

            }
            else
            {
                if (!on_attack|| attack_delay > 0)
                {
                    if (can_chase)
                    {

                        chase_player();
                        //StopCoroutine("attack");
                    }
                    else
                    {


                        move_ai_0();

                    }
                }
               


            }
            if (attack_delay > 0)
            {
                

                attack_delay -= Time.deltaTime;
                e_ani.ResetTrigger("attack");
                E_range.on_player = false;
                rgd.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {

                e_ani.SetBool("attack_delay", false);
                rgd.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        else
        {
            die();
        }

    }

    void die()
    {
        unit.HpCheack();
        e_ani.SetTrigger("death");
        for (int i = 0; i < created_object.Count; i++)
        {
            Destroy(created_object[i]);
        }
    }

    public void create_bullet()
    {

        GameObject obj = Instantiate(create_object[0], Player.transform.position, Quaternion.identity);
        created_object.Add(obj);
        attack_effect_06 a = obj.GetComponent<attack_effect_06>();
        a.Attack = unit.Attack_point;
        // Enemy_status e = this.GetComponent<Enemy_status>();
    }







    void chase_player()
    {
        if (!move_strict&&unit.can_move)
        {

            e_ani.SetBool("move", true);
            e_ani.SetBool("attack_delay", true);
            /*  if (path.Count - 5 >= 0)
              {
                  node_dir = path[path.Count - 5].pos - (Vector2)this.transform.position;
                  if (node_dir.x == 0)
                  {
                      if (path.Count - 6 >= 0)
                      {
                          node_dir = path[path.Count - 6].pos - (Vector2)this.transform.position;
                          if (node_dir.x == 0)
                          {
                              if (path.Count - 7 >= 0)
                              {
                                  node_dir = path[path.Count - 7].pos - (Vector2)this.transform.position;

                              }
                              else
                              {
                                  node_dir = Vector3.zero;
                              }
                          }
                      }
                      else
                      {
                          node_dir = Vector3.zero;
                      }
                  }
              }
              else
              {
                  node_dir = Vector3.zero;
              }*/
            /* if(node_dir!=Vector2.zero)
             rgd.AddForce(node_dir.normalized * move_force);
             else
             {
                 node_dir = path[0].pos - (Vector2)this.transform.position;
                 rgd.AddForce(node_dir.normalized * move_force);
             }*/
            if (path.Count - 5 >= 0)
            {
                node_dir = path[5].pos - (Vector2)this.transform.position;
            }

            rgd.AddForce(node_dir.normalized* (move_force+s_ran));
            if (node_dir.x <= 0)
            {
                if (unit.direction == -1)
                {
                    unit.direction_change_spr();
                }
            }
            else
            {
                if (unit.direction == 1)
                {
                    unit.direction_change_spr();
                }
            }
            if (rgd.velocity.magnitude == 0)
            {
                p_e_07.find_not_stuckpath(path[0]);
            }
            /* transform.Translate(node_dir.normalized * unit.move_speed * Time.deltaTime);
             if (node_dir.x >0)
                   {
                       if (unit.direction == 1)
                       {
                           unit.direction_change_spr();
                       }
                       transform.Translate(Vector3.left * unit.direction * unit.move_speed * Time.deltaTime);
                   }
                   else if(node_dir.x < 0)
                   {
                       if (unit.direction == -1)
                       {
                           unit.direction_change_spr();
                       }
                       transform.Translate(Vector3.left * unit.direction * unit.move_speed * Time.deltaTime);
              }
              else
              {

              }
                   if (node_dir.y > 0)
                   {
                       transform.Translate(Vector3.up* unit.move_speed * Time.deltaTime);
                   }
                   else if (node_dir.y < 0)
                   {
                       transform.Translate(Vector3.down* unit.move_speed * Time.deltaTime);
              }
              else
              {

              }*/




        }
    }





        void move_ai_0()
        {
            if (!move_strict && unit.can_move)
            {
                transform.Translate(Vector3.left * unit.direction * unit.move_speed * Time.deltaTime);//정면으로 움직임

                move_distance += unit.move_speed * Time.deltaTime;
                //정면에 레이캐스트로 벽감지
                Debug.DrawLine(transform.position, transform.position - (new Vector3(0.2f, 0, 0) + new Vector3(enemy_size_x / 2, 0, 0)) * unit.direction, Color.green);
                var wall_ray = Physics2D.Raycast(transform.position, Vector3.left * unit.direction, enemy_size_x / 2 + 0.2f, LayerMask.GetMask("platform_can't_pass"));
                if (wall_ray.collider != null)
                {
                    unit.direction_change_spr();
                    move_distance = 0;
                }
            }
            /*//일정 거리 이상이면 방향 바꿈
            if (move_distance >= move_distance_max)
            {
                unit.direction_change_spr();
                move_distance = 0;
            }*/
            //앞에 플랫폼이 없으면 방향 바꿈
            /* var bottom_ray = Physics2D.Raycast(transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction, Vector3.down, enemy_size_y / 2 + 0.4f, LayerMask.GetMask("platform_can't_pass"));
             var bottom_ray_2 = Physics2D.Raycast(transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction, Vector3.down, enemy_size_y / 2 + 0.4f, LayerMask.GetMask("platform_can_pass"));
             Debug.DrawLine(transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction, transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction + (Vector3.down * (enemy_size_y / 2) + new Vector3(0, 0.4f)), Color.blue);
             if (bottom_ray.collider == null && bottom_ray_2.collider == null)
             {
                 unit.direction_change_spr();
                 move_distance = 0;
             }*/

        }
        void attack()
        {
        rgd.velocity = Vector3.zero;

            
     
        e_ani.SetBool("move", false);
            e_ani.SetTrigger("attack");
       
    }
    /*
    IEnumerator attack()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(attack_time - (float)(0.2 * Gamemanager.GM.stage - 1) + attack_weight);
        attack_status = false;
        e_ani.SetBool("move", false);
        e_ani.SetTrigger("attack");
        yield return wait;
        attack_status = true;
        attack_weight = Random.Range(-1, 1);


    }*/
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Debug.Log("충돌");
            //node_dir *= -1;
            //Vector2 col_force = (Vector2)transform.position - collision.contacts[0].point;
            //rgd.AddForce(-1*col_force.normalized * wall_bounce_force, ForceMode2D.Impulse);
            // rgd.AddForce(, ForceMode2D.Impulse);
           /* if (((Vector2)transform.position - collision.contacts[0].point).y >= 0)
            {
                rgd.AddForce(Vector3.up * wall_bounce_force, ForceMode2D.Impulse);
            }
            else
            {
                rgd.AddForce(Vector3.down * wall_bounce_force, ForceMode2D.Impulse);
            }*/

        }
    }
    IEnumerator move()
        {
            var wait = new WaitForSeconds(moving_buffer + moving_weight);

            moving_status = true;
            e_ani.SetBool("move", true);
            yield return wait;
            move_corutine_check = false;
            moving_status = false;
            e_ani.SetBool("move", false);
            moving_weight = Random.Range(-1, 1);
        }
        IEnumerator idle()
        {
            var wait = new WaitForSeconds(idle_time);
            e_ani.SetBool("move", false);
          
            idle_corutine_check = true;
            yield return wait;
            move_corutine_check = true;
            idle_corutine_check = false;
        }

    }



