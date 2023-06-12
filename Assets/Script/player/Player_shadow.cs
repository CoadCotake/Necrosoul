using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shadow : MonoBehaviour
{
    public PlayerCharacter p;
    public bool p_created;
    public int attack_num;
    public Vector2 dash_direction;
    public float dash_time = 1f;
    public float dash_timer = 0;
    public Player_shadow_animator p_anim;
    public float dash_force;
    public int animation_level;
    public bool shadow_type;//그림자 종류 true면:시간에따라 존재하다가 사라짐 false면:애니메이션 한번 실행 후 사라짐
    public float shadow_time;//지속 시간
    private bool on_dash;
    private Vector3 move_vector;
    private Vector2 direct_vector;
    private float move_speed;
    private float move_weight;
    public int direction;
    
    private int jump_count;
    private bool dash_recover_check;
    private Rigidbody2D rgd;
    private bool raycheck;
    private bool onground;
    private int air_attack_num;
    private float jumpbuffertimer;
    private float hangTimer;
    private float jumpbuffertime;
    private bool on_corutine_1;
    private bool on_platform;
    Color c;
    float c_alpha;
    public float shadow_original_timer;
    public float Downbuffertime;
    public float Downbuffertimer;
    public bool once_chk;
    public bool anim_chk;
    SpriteRenderer s_r;
    public float Player_Y;
    internal bool can_move;
    float once_timer;
    public void Start()
    {
        s_r = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        c = s_r.color;
        c_alpha = c.a;
        p_anim = this.transform.GetChild(0).GetComponent<Player_shadow_animator>();
        rgd = this.GetComponent<Rigidbody2D>();
        // make_stat();
    }
    public void make_stat()
    {
        
        if(p==null)
            p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        Player_Y = p.Player_Y;
        dash_time = p.dash_time;
        dash_force = p.dash_force;
        move_speed = p.move_speed;
        direction = 1;
        /*if (p.direction == -1)
            direction_change();*/
        jump_count = 1;

    }
    private void FixedUpdate()
    {
        
        if (p == null)
        {
            make_stat();
        }
        if (p_created)
        {
            this.transform.position = p.transform.position;
        }
        if (on_dash)
        {
            dash_timer -= Time.deltaTime;
            rgd.gravityScale = 0;
            if (dash_timer < 0)
            {
                dash_end();
               
            }
        }
       /* if (once_chk && anim_chk)
        {
            once_timer += Time.deltaTime;
            if (once_timer > 0.15f)
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }*/
        if (shadow_type)
        {
            if (shadow_time >= 0)
            {
                c.a =c_alpha* shadow_time / shadow_original_timer;
                s_r.color = c;
                shadow_time -= Time.deltaTime;
                animation_work();
            }
            else
            {
                this.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            animation_work();
        }
        if (rgd.velocity.y != 0)
        {
            if (onground)
            {

                onground = false;
            }
        }
        else
        {
            if (!onground)
            {

                onground = true;
            }
        }
        p_anim.set_ground(onground);
        if (rgd.velocity.y <= 0 && !raycheck)
        {
            Debug.DrawRay(this.transform.position, Vector2.down * Player_Y, Color.red);
            RaycastHit2D down_ray_1 = Physics2D.Raycast(this.transform.position - Vector3.up, Vector2.down, Player_Y, LayerMask.GetMask("platform_can't_pass"));
            RaycastHit2D down_ray_2 = Physics2D.Raycast(this.transform.position - Vector3.up, Vector2.down, Player_Y, LayerMask.GetMask("platform_can_pass"));
            if (down_ray_1.collider)
            {
                groundcollision(down_ray_1.transform.gameObject);

                Debug.Log("플랫폼");
            }
            if (down_ray_2.collider)
            {
                groundcollision(down_ray_2.transform.gameObject);

                Debug.Log("단방향");
            }
            else
            {

            }
        }
    }
    public void animation_work()
    {
        if (once_chk)
        {
            if (!anim_chk)
            {
                switch (animation_level)
                {
                    case 1:
                        attack(1);
                        break;
                    case 2:
                        attack(2);
                        break;
                    case 3:
                        attack(3);
                        break;
                    case 4:
                        if (onground)
                        {
                            jump();
                        }


                        break;
                    case 5:
                        move_left();
                        break;
                    case 6:
                        Move_right();
                        break;
                    case 7:
                        StartCoroutine(downplatform());
                        break;
                    case 8:
                        if (!on_dash)
                        {
                            dash();
                        }

                        break;
                    case 9:
                        attack_combo();
                        break;
                    case 10:
                        if (!on_dash)
                        {
                            dash_mirror();
                        }
                        break;
                    case 11:
                        air_attack();
                        break;
                }
               
            }
        }
        else
        {
            switch (animation_level)
            {
                case 1:
                    attack(1);
                    break;
                case 2:
                    attack(2);
                    break;
                case 3:
                    attack(3);
                    break;
                case 4:
                    if (onground)
                    {
                        jump();
                    }


                    break;
                case 5:
                    move_left();
                    break;
                case 6:
                    Move_right();
                    break;
                case 7:
                    StartCoroutine(downplatform());
                    break;
                case 8:
                    if (!on_dash)
                    {
                        dash();
                    }
                    break;
                case 9:
                    attack_combo();
                    break;
                case 10:
                    if (!on_dash)
                    {
                        dash_mirror();
                    }
                    break;
                case 11:
                    air_attack();
                    break;
            }
        }
    }

    private void air_attack()
    {
        anim_chk = true;
        p_anim.air_attack_anim();
    }

    void dash()
    {
        
        
            rgd.velocity = Vector2.zero;


            p_anim.dash();

            on_dash = true;
            dash_timer = dash_time;

            //dash_direction =  Vector2.right*direction * Player_status.p_status.get_dash_force();
            dash_direction = Vector2.right * direction * dash_force;
            Debug.Log(dash_direction);
            rgd.AddForce(dash_direction * Time.deltaTime, ForceMode2D.Impulse);
  

            dash_recover_check = false;
        

    }
    void dash_mirror()
    {


        rgd.velocity = Vector2.zero;


        p_anim.dash();

        on_dash = true;
        dash_timer = dash_time;
        if (this.transform.GetChild(0).transform.rotation.y != 180)
        {
            
            this.transform.GetChild(0).transform.Rotate(0, 180, 0);
        }
        direction = -1;
        //dash_direction =  Vector2.right*direction * Player_status.p_status.get_dash_force();
        dash_direction = Vector2.right * direction * dash_force;
        Debug.Log(dash_direction);
        rgd.AddForce(dash_direction * Time.deltaTime, ForceMode2D.Impulse);


        dash_recover_check = false;


    }
    void dash_end()
    {
        rgd.velocity = Vector2.zero;

        p_anim.dash_end();

        anim_chk = true;
        rgd.gravityScale = 1;
        on_dash = false;
    }

    public void down_platform()
    {
        if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.DOWN]) && on_platform && Downbuffertimer <= 0 && !p_anim.sword_delay && !on_dash)
        {
            Downbuffertimer = Downbuffertime;
        }
        if (Downbuffertimer > 0)
        {
            Downbuffertimer -= Time.deltaTime;
            if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.DOWN]) && on_platform && Downbuffertimer >= 0 && !p_anim.sword_delay && !on_dash)
            {
                //layer_change_timer = layer_change_time;
                if (!on_corutine_1)
                    StartCoroutine(downplatform());
            }
        }
    }
        IEnumerator downplatform()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(Downbuffertime);
        on_corutine_1 = true;
        this.gameObject.layer = 10;
        yield return wait;

        on_corutine_1 = false;
        this.gameObject.layer = 6;
     
    }
    public void Move_right()
    {
        p_anim.move_state = true;
        if (direction == -1)
        {
            direction_change();
        }

        move_vector = new Vector3(direction * move_speed , 0, 0);


        RaycastHit2D wall_ray = Physics2D.Raycast(this.transform.position, Vector3.right, p.Player_X, LayerMask.GetMask("platform_can't_pass"));

        Debug.DrawRay(this.transform.position, Vector3.right * p.Player_X, Color.red);
        if (wall_ray.collider)
        {

            move_weight *= 0.93f;
        }
        else
        {
            move_weight = 1;
        }


        transform.Translate(move_vector * move_weight * Time.deltaTime);
    }
    public void attack_combo()
    {
        if (onground)
        {
            p_anim.attack_combo();
           
        }
    }
    public void move_left()
    {
        p_anim.move_state = true;
        if (direction == 1)
        {
            direction_change();
        }

        move_vector = new Vector3(direction * move_speed * Time.deltaTime * -1, 0, 0);
        direct_vector = new Vector2(-1, direct_vector.y);


        RaycastHit2D wall_ray = Physics2D.Raycast(this.transform.position, Vector3.left, p.Player_X, LayerMask.GetMask("platform_can't_pass"));
        Debug.DrawRay(this.transform.position, Vector3.left * p.Player_X, Color.red);
        if (wall_ray.collider)
        {

            move_weight *= 0.93f;
        }
        else
        {
            move_weight = 1;
        }


        transform.Translate(move_vector * move_weight);

    }
    private void OnDestroy()
    {
       // p_anim.melee_initialize();
    }
    public void jump()
    {
        if (jump_count > 0)
        {
            jump_count--;
            p_anim.jump_anim();
            rgd.AddForce(new Vector2(rgd.velocity.x, Player_status.p_status.get_jump_force()), ForceMode2D.Impulse);
            raycheck = false;
            anim_chk = true;
        }
    }
    public void jump_system()
    {
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]) && !p_anim.sword_delay && !on_dash)
        {
            jumpbuffertimer = jumpbuffertime;
        }
        else
        {
            if (jumpbuffertimer > 0)
            {
                jumpbuffertimer -= Time.deltaTime;
            }
        }
        if (jumpbuffertimer > 0 && jump_count == Player_status.p_status.get_jump_count() && hangTimer > 0)
        {
            jumpbuffertimer = 0;
           jump();
            p_anim.jump_anim();
            //jump();
        }
        else if (jumpbuffertimer > 0 && jump_count != 0)
        {
            jumpbuffertimer = 0;
           jump();
            p_anim.jump_anim();
            //jump();
        }
        if (Input.GetKeyUp(Key_manager.Keys[Key_manager.KeyAction.JUMP]) && rgd.velocity.y > 0 && !on_dash)
        {
            jumpbuffertimer = 0;
            rgd.velocity = new Vector2(rgd.velocity.x, rgd.velocity.y * 0.5f);
        }
    }
    void attack()
    {
       

        

                if (onground)
                    p_anim.sword_attack_anim();
                else if (!onground && air_attack_num > 0)
                {
                    p_anim.air_attack_anim();
                   air_attack_num--;
                }
                
            

        }
    void attack(int i)
    {




        if (onground)
            p_anim.sword_attack_anim(i-1);
       



    }
    public void groundcollision(GameObject a)
    {
        if (a.layer != 11)
        {

            //Debug.Log("점프회복");
            jump_count = Player_status.p_status.get_jump_count();
            dash_recover_check = true;
            onground = true;
          air_attack_num = Player_status.p_status.air_attack_num_orignal + Player_status.p_status.air_attack_num_bonus;
            raycheck = true;

        }
        else
        {

            if (onground)
            {

                // Debug.Log("점프회복");
                jump_count = Player_status.p_status.get_jump_count();
                dash_recover_check = true;
                onground = true;
               air_attack_num = Player_status.p_status.air_attack_num_orignal + Player_status.p_status.air_attack_num_bonus;
                raycheck = true;

            }



        }

    }
    public void stop_move()
    {
        direct_vector = new Vector2(0, direct_vector.y);
        p_anim.move_state = false;
    }
    void direction_change()
    {
        direction *= -1;
        transform.Rotate(0, 180, 0);
        if (transform.rotation.y > 360)
            transform.Rotate(0, -360, 0);
    }
    public void Move_player()
    {
        if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.RIGHT]) && !p_anim.sword_delay && !on_dash)
        {
            Move_right();

        }
        else if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.LEFT]) && !p_anim.sword_delay && !on_dash)
        {
            move_left();

        }
        else
        {
            stop_move();
        }
    }
}
