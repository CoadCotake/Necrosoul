using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerCharacter p_chr;
    public GameObject prefab;
    public GameObject Gun;
    public GameObject Shoot;
    Rigidbody2D rgd;
    Quaternion mouse_rotation;
    public Vector2 direction;
    float angle;
    public Player_animator p_anim;
    bool rotate_chk;
    public int direct;//-1or 1
    private bool FireState; // 미사일 발사 속도를 제어할 변수
    public float attack_buffer_time;
    public float attack_buffer_timer;
    public float hang_time;
    public float hang_timer;
    public Player_animator p_ani;
    
    private void Start()
    {
        rgd = this.GetComponent<Rigidbody2D>();
        p_chr = this.gameObject.GetComponent<PlayerCharacter>();
        p_chr.can_attack = true;
        if (p_ani == null)
         p_ani.transform.GetChild(1).GetComponent<Player_animator>();

       
        FireState = true;
        p_anim = GetComponentInChildren<Player_animator>();
    }
    private void FixedUpdate()
    {
        if (OpenDownInventory.check==false || p_chr.can_attack) 
        {
           //S mouse_point();
            if (Gamemanager.GM.can_handle)
            {
                // MouseAttack();
                // X_attack();
                melee_attack();
            }
        }
    }
    void mouse_point()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        mouse_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        MouseAttack();
    }


    void MouseAttack()  //공격 버튼 처리
    {
        Vector3 rotation_num=new Vector3(0,180,0);
        Quaternion rotation__=Quaternion.AngleAxis(180, Vector3.up);
        if (Player_status.p_status.get_volly())
        {
            if (FireState&&!Gamemanager.GM.map_mode)
            {
                if (Input.GetMouseButton(0))
                {
                    //StartCoroutine(FireCycleControl());
                    ShootBullet(Gun, Shoot,Player_status.p_status.get_atk(), Player_status.p_status.get_bullet_speed(),direction.normalized);
                    if (direction.x >= 0)
                    {
                        direct = 1;
                    }
                    else
                    {
                        direct = -1;
                    }

                    if (direct != p_chr.direction)
                    {
                        if (p_chr.onground)
                            p_anim.attack_anim_mirror();
                        else
                            p_anim.air_attack_anim_mirror();
                    }
                    else
                    {
                        if (p_chr.onground)
                            p_anim.attack_anim();
                        else
                            p_anim.air_attack_anim();
                    }
                }
                
            }
        }
        else
        {
            if (FireState)
            {
                if (Input.GetMouseButtonDown(0))
                {
                   // StartCoroutine(FireCycleControl());
                    ShootBullet(Gun, Shoot, Player_status.p_status.get_atk(), Player_status.p_status.get_bullet_speed(),direction.normalized);
                    if (direction.x >= 0)
                    {
                        direct = 1;
                    }
                    else
                    {
                        direct = -1;
                    }
                    if (direct != p_chr.direction)
                    {
                        if (p_chr.onground)
                            p_anim.attack_anim_mirror();
                        else
                            p_anim.air_attack_anim_mirror();
                    }
                    else
                    {
                        if (p_chr.onground)
                            p_anim.attack_anim();
                        else
                            p_anim.air_attack_anim();
                    }
                }
               
            }
        }
    }
    void ShootBullet(GameObject Gun,GameObject Shoot,int damage,float speed,Vector2 direction)    //총 발사
    {
        Gun.transform.rotation = Gun.transform.rotation;
        var myInstance = ObjectPool.GetObject(Gun.transform,Shoot.transform,damage,speed,direction);
    }
    void ShootBullet( GameObject Shoot, int damage, float speed)    //총 발사
    {
        Gun.transform.rotation = Gun.transform.rotation;
        Vector2 v = new Vector2(1, 0);
        Vector2 dir = (Vector2)Shoot.transform.position;
        if (p_chr.direction == 1)
        {
            Vector2 dir_0 = (Vector2)Shoot.transform.position + v;
            dir = dir_0 - dir;
        }
        else
        {
            Vector2 dir_0 = (Vector2)Shoot.transform.position - v;
            dir = dir_0 - dir;
        }
        var myInstance = ObjectPool.GetObject( Shoot.transform, damage, speed,dir.normalized);
    }

  /*  IEnumerator FireCycleControl()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(Player_status.p_status.get_firedelay());

        FireState = false; // FireDelay초 후에
        yield return wait; //FireState를 true로 만든다.
        FireState = true;
    }*/
    void melee_attack()
    {
        if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {
            
            attack_buffer_timer = attack_buffer_time;
        }
        if (attack_buffer_timer > 0)
        {
            attack_buffer_timer -= Time.deltaTime;
        }
       
        if (attack_buffer_timer > 0 && !p_ani.sword_delay&&p_chr.can_attack)
        {

            if (p_chr.onground)
            {
                
                p_anim.sword_attack_anim();
            }
            else if (!p_chr.onground && Player_status.p_status.air_attack_num > 0)
            {
                p_anim.air_attack_anim();
                Player_status.p_status.air_attack_num--;
            }
            attack_buffer_timer = 0;
        }
    }
    void X_attack()
    {
        
            if (Player_status.p_status.get_volly())
        {
            Debug.Log("aa");
            if (FireState && !Gamemanager.GM.map_mode)
            {

                Debug.Log("aaa");
                if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
                {
                    Debug.Log("aaaa");
                    //StartCoroutine(FireCycleControl());
                    //ShootBullet(Shoot, Player_status.p_status.get_atk(), Player_status.p_status.get_bullet_speed());

                    if (p_chr.onground)
                        p_anim.sword_attack_anim();
                    else if (!p_chr.onground && Player_status.p_status.air_attack_num > 0)
                    {
                        p_anim.air_attack_anim();
                        Player_status.p_status.air_attack_num--;
                    }
                    /*  if (direction.x >= 0)
                      {
                          direct = 1;
                      }
                      else
                      {
                          direct = -1;
                      }

                      if (direct != p_chr.direction)
                      {
                          if (p_chr.onground)
                              p_anim.attack_anim_mirror();
                          else
                              p_anim.air_attack_anim_mirror();
                      }
                      else
                      {
                          if (p_chr.onground)
                              p_anim.attack_anim();
                          else
                              p_anim.air_attack_anim();
                      }*/
                }

            }
        }
        else
        {
            //Debug.Log("aa");
            if (FireState && !Gamemanager.GM.map_mode)
            {
                //Debug.Log("aa");
                if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
                {
                   // StartCoroutine(FireCycleControl());
                    //ShootBullet( Shoot, Player_status.p_status.get_atk(), Player_status.p_status.get_bullet_speed());
                    if (p_chr.onground)
                        p_anim.sword_attack_anim();
                    else if (!p_chr.onground && Player_status.p_status.air_attack_num > 0)
                    {
                        p_anim.air_attack_anim();
                        Player_status.p_status.air_attack_num--;
                    }
                    /* if (direction.x >= 0)
                     {
                         direct = 1;
                     }
                     else
                     {
                         direct = -1;
                     }
                     if (direct != p_chr.direction)
                     {
                         if (p_chr.onground)
                             p_anim.attack_anim_mirror();
                         else
                             p_anim.air_attack_anim_mirror();
                     }
                     else
                     {
                         if (p_chr.onground)
                             p_anim.attack_anim();
                         else
                             p_anim.air_attack_anim();
                     }*/
                }

            }
        }
    }
}
