using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_05_AI : MonoBehaviour
{
    Vector2 dir;
    private Quaternion rotation;
    Enemy_status E_Status;
    public float attack_time;
    bool attack_status;
    public bool can_attack;
    public float bullet_size;
    float attack_weight;
    Unit unit;
    GameObject Player;

    float move_distance;
    public float move_distance_max;
    public float enemy_size_x;
    public float enemy_size_y;
    public float moving_buffer;
    float moving_weight;
    public float range_distance;
    bool move_corutine_check;
    bool idle_corutine_check;
    bool moving_status;
    public float idle_time;
    public float random_num;
    public float random_num2;
    public Vector2 sentinal_size;
    private Collider2D sensitive;
    public int upvector = 1;
    Animator e_ani;
    public int wall_bound = 1;
    float t;
    // Start is called before the first frame update
    void Start()
    {
       
        Player = GameObject.FindGameObjectWithTag("Player");
        unit = this.GetComponent<Unit>();
        unit.size_x = enemy_size_x;
        unit.size_y = enemy_size_y;
        attack_status = true;
        e_ani = this.transform.GetChild(1).GetComponent<Animator>();
        E_Status = this.gameObject.GetComponent<Enemy_status>();
        E_Status.set_layout(2);
        unit.can_hitted_ani = true;
        unit.max_hp = E_Status.get_max_hp();
        unit.Health_point = E_Status.get_hp();
        unit.Defense_point = E_Status.get_defense_point();
        unit.move_speed = E_Status.get_speed();
        unit.Attack_point = E_Status.get_atk();
    }
    void ray_to_player()
    {

        float length = Mathf.Log(Mathf.Pow(18, 2) + Mathf.Pow(28, 2)) * 2;
        var dir_dist = Mathf.Log(Mathf.Pow(dir.x, 2) + (Mathf.Pow(dir.y, 2)));
        if (Player != null)
            Debug.DrawLine(transform.position, Player.transform.position.normalized * dir_dist, Color.red);
        // Debug.DrawLine(transform.position - Vector3.up * (bullet_size / 2), Player.transform.position.normalized * range_distance, Color.red);
        // var ray1= Physics2D.Raycast(transform.position+Vector3.up*(bullet_size/2), dir, length, LayerMask.GetMask("platform_can't_pass"));
        // var ray2 = Physics2D.Raycast(transform.position - Vector3.up * (bullet_size / 2), dir, length, LayerMask.GetMask("platform_can't_pass"));
        var ray = Physics2D.Raycast(transform.position, dir, length, LayerMask.GetMask("platform_can't_pass"));
        var ray2 = Physics2D.Raycast(transform.position, dir, length, LayerMask.GetMask("platform_can_pass"));
        if (ray.collider != null)
        {
    
            can_attack = false;
        }
        else
        {
   
            can_attack = true;
        }
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
            if (unit.onGround)
            {
                unit.onGround = false;
            }
            e_ani.SetBool("Move", unit.can_move);

            Player = this.transform.GetComponent<Unit>().Player;
            
            if(Player!=null)
                dir = Player.transform.position - this.transform.position;
            var dir_dist = Mathf.Log(Mathf.Pow(dir.x, 2) + (Mathf.Pow(dir.y, 2)));
            ray_to_player();
            

            /*if (dir_dist<range_distance||unit.sentinal)//벽에 안막힘+사정거리안
            {
                attack_ai_0();
            }
            else
            {*/

               
               
                    move_ai_0();
                
           // }
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














    void move_ai_0()
    {


        /* if (random_num > 0.5)
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

         }*/
        if (unit.can_move)
        {
          
            transform.Translate(Vector3.left * unit.direction * unit.move_speed * Time.deltaTime);



            e_ani.SetBool("move", true);

            //move_distance += unit.move_speed * Time.deltaTime;
            //정면에 레이캐스트로 벽감지
            Debug.DrawLine(transform.position, transform.position - (new Vector3(0.2f, 0, 0) + new Vector3(enemy_size_x / 2, 0, 0)) * unit.direction, Color.blue);
            //Debug.DrawLine(transform.position, transform.position - (new Vector3(0, 0,2, 0) + new Vector3(enemy_size_y / 2, 0, 0)) * unit.direction*, Color.blue);
            //Debug.DrawLine(transform.position, transform.position - (new Vector3(0.2f, 0, 0) + new Vector3(enemy_size_y / 2, 0, 0)) * unit.direction, Color.blue);
            var wall_ray = Physics2D.Raycast(transform.position + Vector3.up * enemy_size_y * 0.5f, Vector3.left * unit.direction, enemy_size_x / 1.5f + 0.4f, LayerMask.GetMask("platform_can't_pass"));
            var wall_ray2 = Physics2D.Raycast(transform.position - Vector3.up * enemy_size_y * 0.5f, Vector3.left * unit.direction, enemy_size_x / 1.5f + 0.4f, LayerMask.GetMask("platform_can't_pass"));
            
            if (wall_ray.collider != null||wall_ray2.collider!=null)
            {
                Debug.Log("벼게 닿음");
                unit.direction_change_spr();
                Debug.Log("col");
            }
          
        }
        }
    void attack_ai_0()
    {
        e_ani.SetBool("move", true);
        transform.Translate(dir.normalized * unit.move_speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(enemy_size_x,enemy_size_y));
    }
    IEnumerator move()
    {
        var wait = new WaitForSeconds(moving_buffer + moving_weight);
        e_ani.SetBool("move", true);
        moving_status = true;
        yield return wait;
        move_corutine_check = false;
        e_ani.SetBool("move", false);
        moving_status = false;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            wall_bound *= -1;
        }
    }
}
