using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_03 : MonoBehaviour
{
    Vector2 dir;
    private Quaternion rotation;
    public List<Transform> create_position = new List<Transform>();
    public float attack_time;
    bool attack_status;
    public bool can_attack;
    public float bullet_size;
    float attack_weight;
    Unit unit;
    GameObject Player;
    public List<GameObject> create_object = new List<GameObject>();
    public List<GameObject> created_object = new List<GameObject>();
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
    Animator e_ani;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        unit = this.GetComponent<Unit>();
        attack_status = true;
        e_ani = this.GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        brain();
    }
    void brain()//ai 정리
    {
        if (unit.Health_point > 0)
        {
           
        
                if (attack_status)
                    StartCoroutine("attack");//공격+움직이지 않음

                if (!moving_status) { 
                    StartCoroutine("move");
                }

                if (moving_status)
                {
                    move_ai_0();
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
        for (int i = 0; i < created_object.Count; i++)
        {
            Destroy(created_object[i]);
        }
    }














    void move_ai_0()
    {
        /*if (transform.rotation.z % 180 == 90)
        {
        }
        else
        {*/
            transform.Translate(Vector3.left * unit.move_speed * Time.deltaTime);//정면으로 움직임
       // }

        Debug.DrawLine(transform.position, transform.position - (new Vector3(0.2f, 0, 0) + new Vector3(enemy_size_x / 2, 0, 0)) * unit.direction, Color.green);
       

        //앞에 플랫폼이 없으면 방향 바꿈
        var bottom_ray = Physics2D.Raycast(transform.position - Vector3.left * (enemy_size_x / 2) * unit.direction, Vector3.down, enemy_size_y / 2 + 0.4f, LayerMask.GetMask("platform_can't_pass"));
        Debug.DrawLine(transform.position - Vector3.left * (enemy_size_x / 2) * unit.direction, transform.position + Vector3.left * (enemy_size_x / 2) * unit.direction + (Vector3.down * (enemy_size_y / 2) + new Vector3(0, 0.4f)), Color.blue);
        /*if (unit.direction == 1)
        {
            transform.Rotate(new Vector3(0, 0, -90));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }*/

    }
    void attack_ai_0()
    {
        for (int i = 0; i < 5; i++) {
            float angle = 180 / 5;
        rotation = Quaternion.AngleAxis(angle*(i+1)+transform.rotation.z, Vector3.forward);
        GameObject obj = Instantiate(create_object[0], create_position[0].position, rotation);
        created_object.Add(obj);
        attack_effect1 a = obj.GetComponent<attack_effect1>();
            a.dir = this.dir;
        }

        
    }

    IEnumerator attack()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(attack_time + attack_weight);
        attack_ai_0();
        e_ani.SetBool("Move", false);
        e_ani.SetTrigger("Attack");
        attack_status = false;
        yield return wait;
        attack_status = true;
        attack_weight = Random.Range(-1, 1);

    }
    IEnumerator move()
    {
        var wait = new WaitForSeconds(moving_buffer + moving_weight);
        e_ani.SetBool("Move", true);
        moving_status = true;
        yield return wait;
        move_corutine_check = false;
        e_ani.SetBool("Move", false);
        moving_status = false;
        moving_weight = Random.Range(-1, 1);
    }
    IEnumerator idle()
    {
        var wait = new WaitForSeconds(idle_time);
        e_ani.SetBool("Move", false);
        idle_corutine_check = true;
        yield return wait;
        move_corutine_check = true;
        idle_corutine_check = false;
    }

}
