using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_06 : MonoBehaviour
{
    //식물
    Enemy_status E_Status;
    Vector2 dir;

    public List<Transform> create_position = new List<Transform>();
    public float attack_time;
    bool attack_status;

    public float bullet_size;
    float attack_weight;
    Unit unit;
    public GameObject Player;

    public List<GameObject> create_object = new List<GameObject>();
    public GameObject created_object ;

    public float move_distance_max;
    public float enemy_size_x;
    public float enemy_size_y;
    public float moving_buffer;
    float moving_weight;
    public float range_distance;
    public bool on_attack;
    public float idle_time;
    public E_AI_06_range attack_range;
    Animator e_ani;
    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(enemy_size_x, enemy_size_y));
    }
    void Start()
    {
        
            Player = this.transform.GetComponent<Unit>().Player;
        
        unit = this.GetComponent<Unit>();
        attack_status = true;
        e_ani = this.transform.GetChild(1).GetComponent<Animator>();
        E_Status = this.gameObject.GetComponent<Enemy_status>();
        E_Status.set_layout(3);
        unit.can_hitted_ani = true;
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
        //콜라이더로 처리
        //can_attack=true

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        brain();
    }
    void brain()//ai 정리
    {
        if (unit.Health_point > 0)
        {
            Player = this.transform.GetComponent<Unit>().Player;
            dir = Player.transform.position - this.transform.position;
            if (Player.GetComponent<PlayerCharacter>()!=null)
            {
                if (attack_range.on_player && Player.GetComponent<PlayerCharacter>().onground)
                {
                    unit.can_attack = true;
                }
                else
                {
                    unit.can_attack = false;
                }
            }
            else
            {
                if (attack_range.on_player)
                {
                    unit.can_attack = true;
                }
                else
                {
                    unit.can_attack = false;
                }
            }
            if (created_object != null && created_object.activeSelf)
            {
                on_attack = true;
                
            }else if(created_object != null && !created_object.activeSelf)
            {
                on_attack = false;
            }
            e_ani.SetBool("on_attack", on_attack);
            if (unit.can_attack)//벽에 안막힘+사정거리안
            {
                if (attack_status && !on_attack)
                {
                    if (dir.x > 0 && unit.direction == 1)
                    {
                        unit.direction_change_spr();
                    }
                    else if (dir.x < 0 && unit.direction == -1)
                    {
                        unit.direction_change_spr();
                    }
                    // attack_플레이어 좌표 받아서 오브젝트 생성
                    StartCoroutine("attack");//공격+움직이지 않음
                }
              
            }
            else
            {
                StartCoroutine("idle");//대기  상태
                                       /* if (move_corutine_check)
                                        {
                                            if (!moving_status)
                                                StartCoroutine("move");//이동상태
                                        }
                                        else
                                        {
                                            if (!idle_corutine_check)
                                                StartCoroutine("idle");//대기  상태
                                        }
                                        if (moving_status)
                                        {
                                            move_ai_0();
                                        }*/
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
        
       
    }

    public void create_bullet()
    {
        if (created_object == null)
        {
            RaycastHit2D bot_ray = Physics2D.Raycast(Player.transform.position, Vector2.down, 99f, LayerMask.GetMask("platform_can't_pass"));
            GameObject obj = Instantiate(create_object[0], bot_ray.point+(Vector2.up*Player.GetComponent<PlayerCharacter>().Player_Y*0.7f), Quaternion.identity);
            created_object = obj;
            created_object.transform.SetParent(this.transform.parent);
            attack_effect_06 a = obj.transform.GetChild(0).GetComponent<attack_effect_06>();
            a.Attack = unit.Attack_point * 2;
            // Enemy_status e = this.GetComponent<Enemy_status>();
        }
        else
        {
            RaycastHit2D bot_ray = Physics2D.Raycast(Player.transform.position, Vector2.down, 99f, LayerMask.GetMask("platform_can't_pass"));
            created_object.transform.position = bot_ray.point + (Vector2.up * Player.GetComponent<PlayerCharacter>().Player_Y * 0.7f);
            attack_effect_06 a = created_object.transform.GetChild(0).GetComponent<attack_effect_06>();
            a.Attack = unit.Attack_point*2;
            created_object.SetActive(true);
        }
    }






    






   /* void move_ai_0()
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
        //일정 거리 이상이면 방향 바꿈
        if (move_distance >= move_distance_max)
        {
            unit.direction_change_spr();
            move_distance = 0;
        }
        //앞에 플랫폼이 없으면 방향 바꿈
        var bottom_ray = Physics2D.Raycast(transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction, Vector3.down, enemy_size_y / 2 + 0.4f, LayerMask.GetMask("platform_can't_pass"));
        var bottom_ray_2 = Physics2D.Raycast(transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction, Vector3.down, enemy_size_y / 2 + 0.4f, LayerMask.GetMask("platform_can_pass"));
        Debug.DrawLine(transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction, transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction + (Vector3.down * (enemy_size_y / 2) + new Vector3(0, 0.4f)), Color.blue);
        if (bottom_ray.collider == null && bottom_ray_2.collider == null)
        {
            unit.direction_change_spr();
            move_distance = 0;
        }

    }*/

    IEnumerator attack()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(attack_time - (float)(0.2 * Gamemanager.GM.stage - 1) + attack_weight);
        attack_status = false;
        e_ani.SetBool("move", false);
        e_ani.SetTrigger("attack");
        yield return wait;
        attack_status = true;
        attack_weight = Random.Range(-1, 1);

    }
    IEnumerator move()
    {
        var wait = new WaitForSeconds(moving_buffer + moving_weight);


        e_ani.SetBool("move", true);
        yield return wait;

        e_ani.SetBool("move", false);
        moving_weight = Random.Range(-1, 1);
    }
    IEnumerator idle()
    {
        var wait = new WaitForSeconds(idle_time);
        e_ani.SetBool("move", false);

        yield return wait;

    }

}
