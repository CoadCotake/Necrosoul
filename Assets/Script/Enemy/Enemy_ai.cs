using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ai : MonoBehaviour
{
    public List<GameObject> create_object = new List<GameObject>();
    public List<Transform> create_position = new List<Transform>();
    public Transform up;
    public Transform down;
    Vector2 dir;
    private Quaternion rotation;
    RaycastHit2D player_ray_wall_check;
    Vector3 up_direction;
    Vector3 down_direction;
    Rigidbody2D rgd;
    public float attack_time;
    public bool attack_status;
    bool weight_dir;
    GameObject Player;
    float weight;
    public bool jump_check;
    bool onground;
    bool down_corutine;
    float dowwn_time = 0.5f;
    float upbuffer = 0.4f;
    float upbufferTime;
    bool upcheck;
    float downbuffer = 0.4f;
    float downbufferTime;
    bool downcheck;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        attack_status = true;
        rgd = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        weight = Random.Range(-1.0f, 5.0f);
        dir =  Player.transform.position- this.transform.position;
        up_direction= Player.transform.position - up.position;
        down_direction = Player.transform.position - down.position;
        ray_to_player();//높이 18 너비 28

       
        if (player_ray_wall_check.collider != null)
        {
            //Debug.Log("벽에 막힘");
            move_ai(2);
        }
        else
        {
            move_ai(2);
            if (attack_status)
            {
                StartCoroutine(attack());
            }
        }
    }
    void ray_to_player()
    {
        float length = Mathf.Log(Mathf.Pow(18, 2) + Mathf.Pow(28, 2))*2;
        Debug.DrawLine(transform.position, Player.transform.position.normalized*length, Color.red);
        player_ray_wall_check = Physics2D.Raycast(transform.position, dir, length, LayerMask.GetMask("platform_can't_passs"));
        Debug.DrawLine(down.position, Player.transform.position.normalized * length, Color.green);
     
        
        Debug.DrawLine(up.position, Player.transform.position.normalized * length, Color.green);
    }
    public void move_ai(int i)//이동 방식
    {
        switch (i)
        {
            case 0://움직임 없음
                break;
            case 1://플레이어 추격(지형지물 무시)
                
                this.transform.Translate(dir * 2 * Time.deltaTime);
                break;
            case 2://플레이어 추격(지형지물 영향o)
            
                
                if (this.transform.position.x > Player.transform.position.x + 4)
                {
                    weight_dir = true;
                }
                else if (this.transform.position.x < Player.transform.position.x - 4)
                {
                    weight_dir = false;
                }

                if (weight_dir)
                {
                    transform.Translate(2 * Vector3.left * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * 2 * Time.deltaTime);
                }
                if (up_direction.y>0&&upcheck==false) {
                    upbufferTime = upbuffer;
                    upcheck = true;
                }//레이캐스트 위로 쏴서 통과가능 플랫폼있으면 점프

                if (upbufferTime<0&&upcheck==true) { 
                    Debug.DrawLine(transform.position, transform.position+Vector3.up*20, Color.blue);
                    RaycastHit2D up_ray = Physics2D.Raycast(this.transform.position, Vector2.up, 20.0f, LayerMask.GetMask("platform_can_pass"));
                    //RaycastHit2D up_ray = Physics2D.Raycast(this.transform.position, Vector2.up, 20.0f);
                    if (up_ray.collider)
                    {
                        if (!jump_check)
                        {
                            jump();
                        }
                    }
                    /*if (up_ray.collider)
                    {
                        Debug.Log(up_ray.transform.gameObject.layer);
                    }*/
                    /*if (up_ray.collider)
                    {
                        Debug.Log("점프준비");
                        if (!jump_check)
                        {
                            
                            jump();
                        }
                    }*/
                    upcheck = false;
                }else if (upcheck == true)
                {
                    upbufferTime -= Time.deltaTime;
                }

                if (down_direction.y<0 && downcheck == false&&!jump_check)
                {
                    downbufferTime = downbuffer;
                    downcheck = true;
                }
                if(downbufferTime < 0 && downcheck == true)
                {
                   
                    if (!down_corutine)
                    {
                        StartCoroutine(down_platform());
                    }
                    downcheck = false;
                }
                else if (downcheck == true)
                {
                    downbufferTime -= Time.deltaTime;
                }
                break;
            default:
                break;
        }
    }
    void jump()
    {
        Debug.Log("점프");
        //rgd.velocity = new Vector2(rgd.velocity.x, minimum_jump_vec3.y);
        rgd.AddForce(new Vector2(rgd.velocity.x,20.0f), ForceMode2D.Impulse);
        jump_check = true;
    }
    /*public bool move_condition(int i)//이동에 조건이 있는가?
    {
        switch (i)
        {
            case 0://없다
                return true;
            case 1:
                return false;
            default:
                return false;
        }

    }*/
    public void attack_ai(int i)//공격 방식
    {
        switch (i)
        {
            case 0://공격하지 않음
                break;
            case 1://0번 오브젝트를 0번 위치에서 플레이어에게 발사
              
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                GameObject obj = Instantiate(create_object[0], create_position[0]. position,rotation );
                attack_effect1 a = obj.GetComponent<attack_effect1>();
                a.dir=this.dir;
                break;
            case 2://근접 오브젝트 생성
                
                float dir_= Mathf.Log(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));
                if (dir_ < 2)
                {
                   rotation = Quaternion.LookRotation(dir.normalized);
                    GameObject atk=Instantiate(create_object[0], create_position[0].position,rotation);
                }
                break;
            case 3://자기 자신에게서 오브젝트 생성
                GameObject created = Instantiate(create_object[0], create_position[0].position, Quaternion.identity);
                break;
            default:

                break;
        }
    }
    IEnumerator attack()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(attack_time+weight);

        attack_ai(1);
        attack_status = false;
        yield return wait;
        attack_status = true;

    }
    IEnumerator down_platform()
    {
        var wait = new WaitForSeconds(dowwn_time);
        this.gameObject.layer = 13;
        down_corutine = true;
        yield return wait;
        down_corutine = false;
        this.gameObject.layer = 7;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (rgd.velocity.y == 0)
            {
                if (jump_check)
                {
                    jump_check = false;
                }
            }
        }
    }
    /* public bool attack_condition(int i)//공격에 조건이 있는가?
     {
         switch (i)
         {
             case 0://없다
                 return true;
             case 1:
                 return false;
             default:
                 return false;
         }

     }*/
}
