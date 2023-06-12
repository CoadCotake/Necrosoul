using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_02 : MonoBehaviour
{
    //move랑die 사정거리만 바꾸기 탄에 눈알
    Vector2 dir;
    private Quaternion rotation;
    public List<Transform> create_position = new List<Transform>();
    public float attack_time;
    bool attack_status;
    public bool can_attack;
    public float bullet_size;
    float attack_weight;
    Unit unit;
    public GameObject Player;
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
    private bool sibling_status;
    private float sibling_weight;
    public float sibling_time;
    public int sibling_count;
    public GameObject[] sibling_object;
    // Start is called before the first frame update
    void Start()
    {
       
        unit = this.GetComponent<Unit>();
        attack_status = true;
        sibling_status = true;
        e_ani = this.transform.GetChild(1).GetComponent<Animator>();
        can_attack = true;
    }
    void ray_to_player()
    {
        float length = Mathf.Log(Mathf.Pow(18, 2) + Mathf.Pow(28, 2)) * 2;
        //Debug.DrawLine(transform.position+ Vector3.up * (bullet_size / 2), Player.transform.position.normalized * range_distance, Color.red);
        Debug.DrawLine(transform.position, Player.transform.position, Color.red);
        // Debug.DrawLine(transform.position - Vector3.up * (bullet_size / 2), Player.transform.position.normalized * range_distance, Color.red);
        // var ray1= Physics2D.Raycast(transform.position+Vector3.up*(bullet_size/2), dir, length, LayerMask.GetMask("platform_can't_pass"));
        // var ray2 = Physics2D.Raycast(transform.position - Vector3.up * (bullet_size / 2), dir, length, LayerMask.GetMask("platform_can't_pass"));
        /*var ray = Physics2D.Raycast(transform.position, dir.normalized, length, LayerMask.GetMask("platform_can't_pass"));
        var ray2 = Physics2D.Raycast(transform.position, dir.normalized * -1, length, LayerMask.GetMask("platform_can't_pass"));
        if (ray.collider == null || ray2.collider == null)
        {
            can_attack = true;
            //Debug.Log("???");

        }
        else
        {

            can_attack = false;
        }*/
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
            sibling_object = GameObject.FindGameObjectsWithTag("enemy_sibling");
            Player = GameObject.FindGameObjectWithTag("Player");
            dir = Player.transform.position - this.transform.position;
            var dir_dist = Mathf.Log(Mathf.Pow(dir.x, 2) + (Mathf.Pow(dir.y, 2)));
            ray_to_player();
            if (can_attack )//벽에 안막힘+사정거리안
            {
                if (attack_status)
                    StartCoroutine("attack");//공격+움직이지 않음
                if (sibling_status)
                    StartCoroutine("sibling");
            }
            else
            {

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
                */
                if (moving_status)
                {
                    move_ai_0();
                }
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
        for(int i = 0; i < 3; i++)
        {
           // Instantiate(created_object[1], create_position[i + 1].position, Quaternion.identity);
        }
    }














    void move_ai_0()
    {
        transform.Translate(Vector3.left * unit.move_speed * Time.deltaTime);//정면으로 움직임
        Debug.Log(Vector3.left * unit.direction * unit.move_speed * Time.deltaTime);
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
    public void create_bullet()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject obj = Instantiate(create_object[0], create_position[0].position, rotation);
        created_object.Add(obj);
        attack_effect1 a = obj.GetComponent<attack_effect1>();

        a.dir = this.dir;
    }
    public void create_sibling()
    {
        if (sibling_object.Length < sibling_count)
        {
            GameObject obj = Instantiate(create_object[1], create_position[1].position, Quaternion.identity);
            
        }
    }

    IEnumerator sibling()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(sibling_time + sibling_weight);
        sibling_status = false;
        create_sibling();
        yield return wait;
        sibling_status = true;
        sibling_weight = Random.Range(0, 1);

    }
    IEnumerator attack()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(attack_time + attack_weight);
        e_ani.SetTrigger("Attack");
        attack_status = false;
        yield return wait;
        attack_status = true;
        attack_weight = Random.Range(-1, 1);

    }
    IEnumerator move()
    {
        var wait = new WaitForSeconds(moving_buffer + moving_weight);

        moving_status = true;
        yield return wait;
        move_corutine_check = false;
        moving_status = false;
        moving_weight = Random.Range(-1, 1);
    }
    IEnumerator idle()
    {
        var wait = new WaitForSeconds(idle_time);
        idle_corutine_check = true;
        yield return wait;
        move_corutine_check = true;
        idle_corutine_check = false;
    }

}
