using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : GameCharacter
{
    public Rigidbody2D rgd;
    public int jump_count = 1;
    public bool onground;//땅에 닿았는지

    public GameObject DNP;

    public bool spawn_check;
    Vector3 move_vector;

    [Header("jump")]
     float hangtime = 0.2f;
    float hangTimer;
    float jumpbuffertime = 0.5f;
    float jumpbuffertimer;

    GameObject player_pos;
    float rush_time = 1f;
    float rush_timer;
    public bool on_rush;
    bool raycheck;

    [Header("dash input")]
    int dash_count;
    float after_dash_timer;
    float after_dash_timer_check = 0.05f;
    bool dash_recover_check;
    float dash_recover_timer;
    public float teleport_length= 6.55f;
    public bool on_teleport;
    Vector2 teleport_pos;
    public float teleport_untouable_time = 0.1f;
    public float teleport_untouable_timer;
    public int teleport_direction;//0:right1:left 2:up 3:down
    bool can_dash = true;
    public Vector2 dash_direction;
    public float dash_time = 1f;
    public float dash_timer = 0;

    public AudioManage_Main m_audio;

    [SerializeField]
    public bool on_dash;

    KeyCode lastest_key_input;
   
    [Header("hitted  input")]
     public Vector2 hitted_force;
    public GameObject m_teleport_effect;
    public float hitted_time = 0.5f;
     public float hitted_timer = 0;
     bool on_hitted;
     Vector2 col_pos;
    bool col_on_room_boost;
    public ActionRecord record;
    public bool death_anim_check;
    [Header("platform")]
    bool on_platform;
    bool on_corutine_1;
    public float Downbuffertime = 0.4f;
    RaycastHit2D down_ray_1;
    RaycastHit2D down_ray_2;
    float Downbuffertimer;
    public GameObject Player_canvas;
    float layer_change_time=0.25f;
    float layer_change_timer;
    Vector3 viewport_pos;
    public float move_weight;
    Vector2 direct_vector;
   float minimum_jump_time = 0.15f;
    float minimum_jump_timer;
    public ProgressBarPro HpBar;            //Hp
    public ParticleSystem dash_ghost_effect;
    public List<GameObject> dash_ghost_effect_instansi=new List<GameObject>();
    public GameObject choose_ghost_effect;
    public Player_animator p_anim;
    Attack a;
    public float Player_X;
    public float Player_Y;
    public bool landing_chk;
    public List<GameObject> heal_cross_instansi = new List<GameObject>();
    public float dash_force;
    
    void Start()
    {
        can_attack = true;
        m_audio = AudioManage_Main.instance;
        a = this.GetComponent<Attack>();
        dash_ghost_effect.gameObject.SetActive(false);
        record = GameObject.Find("GameSystem").GetComponent<Record>().ActionRecord;
        p_anim = gameObject.GetComponentInChildren<Player_animator>();
        
        DNP = GameObject.Find("DemoManager");
        Player_status.p_status.set_layout();
        rgd = gameObject.GetComponent<Rigidbody2D>();
        jump_count = Player_status.p_status.get_jump_count();
        spawn_check = true;
        direction = 1;
        can_move = true;
        for (int i = 0; i < 3; i++)
        {
            var a = Instantiate(dash_ghost_effect.gameObject,this.transform.position,Quaternion.identity);
            dash_ghost_effect_instansi.Add(a);
            a.transform.SetParent(this.transform);
            a.SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            var a = Instantiate(Gamemanager.GM.heal_cross_particle.gameObject, this.transform.position, Quaternion.identity);
            heal_cross_instansi.Add(a);
            a.transform.SetParent(this.transform);
            a.SetActive(false);
        }
        if (player_pos == null)
        {
            GameObject ins = Instantiate(Gamemanager.GM.player_minimap_pos);
            ins.transform.position = this.transform.position;
            ins.transform.SetParent(this.transform);
        }
    }
    GameObject player_heal_cross_pulling()
    {
        int index = 0;
        for (int i = 0; i < heal_cross_instansi.Count; i++)
        {
            if (!heal_cross_instansi[i].activeSelf)
            {
                index = i;
            }
        }
        return heal_cross_instansi[index];
    }
    public void Player_heal_cross_particle(int n)
    {
        var obj = player_heal_cross_pulling();
        heal_cross_particle a = obj.GetComponent<heal_cross_particle>();
        a.n = n;
        obj.transform.position = this.transform.position;
        obj.SetActive(true);
    }
    GameObject player_ghost_pulling()
    {
        int index = 0;
        for (int i = 0; i < dash_ghost_effect_instansi.Count; i++)
        {
            if (!dash_ghost_effect_instansi[i].activeSelf)
            {
                index = i;
            }
        }
        return dash_ghost_effect_instansi[index];
    }
    public void set_col_boost(bool a)
    {
        col_on_room_boost = a;
    }
    public bool get_col_boost()
    {
        return col_on_room_boost;
    }
    public void set_can_dash(bool a)
    {
        can_dash = a;
    }
    public void set_can_move(bool a)
    {
        can_move = a;
    }
    void player_camera_fitting()
    {
        viewport_pos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewport_pos.x < 0) viewport_pos.x = 0;
        if (viewport_pos.y < 0) viewport_pos.y = 0;
        if (viewport_pos.x > 1) viewport_pos.x = 1;
        if (viewport_pos.x > 1) viewport_pos.y = 1;
        
        transform.position = Camera.main.ViewportToWorldPoint(viewport_pos);
        if (transform.rotation.y > 360)
            transform.Rotate(0, -360, 0);
    }
    void FixedUpdate()
    {
    
        down_ray_1 = Physics2D.Raycast(this.transform.position+ (Vector3.down* (Player_Y-0.1f)), Vector2.down, 0.1f, LayerMask.GetMask("platform_can't_pass"));
        down_ray_2 = Physics2D.Raycast(this.transform.position + (Vector3.down * (Player_Y - 0.1f)), Vector2.down, 0.1f, LayerMask.GetMask("platform_can_pass"));
        // Debug.Log(this.transform.position);
        max_hp = Player_status.p_status.get_max_hp();
        Health_point = Player_status.p_status.get_hp();
        Defense_point = Player_status.p_status.get_defense_point();
        move_speed = Player_status.p_status.get_speed();
        //스테이터스의 값 동기화
       /* if (this.GetComponent<player_room_boost_mode>().boost_mode_horiz)
        {
            
            RaycastHit2D ray_3 = Physics2D.Raycast(this.transform.position , Vector2.left, Player_X, LayerMask.GetMask("platform_can't_pass"));
            RaycastHit2D ray_4 = Physics2D.Raycast(this.transform.position , Vector2.right, Player_X, LayerMask.GetMask("platform_can't_pass"));
           

            if ( ray_3.collider || ray_4.collider)
            {
                this.gameObject.layer = 6;
                rgd.velocity = Vector3.zero;
                this.GetComponent<player_room_boost_mode>().boost_mode_horiz = false;
                this.GetComponent<player_room_boost_mode>().un_boost();
            }
        }*/
        if (HpBar != null)
        {
            HpBar.SetMaxTextValue(Health_point, max_hp);
            HpBar.SetValue(Health_point, max_hp);
        }
        p_anim.set_ground(onground);

        if (Gamemanager.GM.can_handle||!!death_anim_check)
        {
            character_move();
            if(!Gamemanager.GM.fade_Outit.activeSelf&&Player_status.p_status.get_hp()>0)
                player_camera_fitting();
            //if(rgd.velocity.y

            //if(rgd.velocity.y
            if (down_ray_1.collider || down_ray_2.collider)
            {
                landing_chk = true;
            }
            else
            {
                landing_chk = false; ;
            }

         
            if (untouchable_state)
                untouchable();
            if (Health_point <= 0 && spawn_check)
            {
                if (!death_anim_check)
                {
                    death_anim_check = true;
                    p_anim.death_state = true;
                    Gamemanager.GM.can_handle = false;
                }
          


            }
                
            if (!can_dash)
            {
                after_dash_timer += Time.deltaTime;
                if (after_dash_timer >= after_dash_timer_check)
                {

                    can_dash = true;
                    after_dash_timer = 0;
                }
            }
           if (rgd.velocity.y !=0)
            {
                if (onground)
                {

                    onground = false;
                }
            }
            else
            {
                
            }
            if (!onground)
            {
                if(hangTimer>0)
                    hangTimer -= Time.deltaTime;
            }
            else
            {
                hangTimer = hangtime;
                direct_vector = new Vector2(direct_vector.x, 0);
                
            }

            if (on_hitted)
            {
                hitted_timer -= Time.deltaTime;
                if (hitted_timer <= 0)
                {
                    hitted_end();
                }
            }
           
            if (rgd.velocity.y <= 0.1&&!onground )
            {
               
                
                if (down_ray_1.collider)
                {
                    groundcollision(down_ray_1.transform.gameObject);
                    
                 
                }
                if (down_ray_2.collider)
                {
                    groundcollision(down_ray_2.transform.gameObject);
                    
                   
                }
                else
                {

                }
            }
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]) && !p_anim.sword_delay && !on_dash && !on_teleport)
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
        if (jumpbuffertimer > 0 && jump_count == Player_status.p_status.get_jump_count() && hangTimer > 0 && !p_anim.sword_delay && !on_dash && !on_teleport)
        {
            jumpbuffertimer = 0;
            minimum_jump();
            p_anim.jump_anim();
            //jump();
        }
        else if (jumpbuffertimer > 0 && jump_count != 0 && !p_anim.sword_delay && !on_dash && !on_teleport)
        {
            jumpbuffertimer = 0;
            minimum_jump();
            p_anim.jump_anim();
            //jump();
        }
        if (minimum_jump_timer > 0)
        {
            minimum_jump_timer -= Time.deltaTime;
        }
        if (!Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.JUMP]) && rgd.velocity.y > 0 && !on_dash && minimum_jump_timer <= 0 && !on_teleport)
        {
            jumpbuffertimer = 0;
            rgd.velocity = new Vector2(rgd.velocity.x, rgd.velocity.y * 0.5f);
        }
    }
    void dash_recover()
    {
        if (dash_count != Player_status.p_status.get_dash_count())
        {
            dash_recover_timer += Time.deltaTime;
            if (dash_recover_timer >= Player_status.p_status.get_dash_recover_time())
            {
                dash_count = Player_status.p_status.get_dash_count();
                dash_recover_timer = 0;
            }


        }
    }
    public void teleport()
    {
      
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash_count != 0 && can_dash&&can_move)
        {
            Debug.Log("텔레포트");
            rgd.velocity = Vector2.zero;
            m_audio.teleport();


            untouchable_state = true;
            on_teleport = true;
            teleport_untouable_timer = teleport_untouable_time;
            RaycastHit2D teleport_ray = Physics2D.Raycast(this.transform.position, Vector2.right, teleport_length, LayerMask.GetMask("platform_can't_pass")); ;
            switch (teleport_direction)
            {
                
                case 0:
                    teleport_ray = Physics2D.Raycast(this.transform.position+(Vector3.right*Player_X*0.5f), Vector2.right, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                    break;
                case 1:
                    teleport_ray = Physics2D.Raycast(this.transform.position+(Vector3.left * Player_X * 0.5f), Vector2.left, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                    break;
                case 2:
                    teleport_ray = Physics2D.Raycast(this.transform.position + (Vector3.up * Player_Y * 0.5f), Vector2.up, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                    break;
                case 3:
                    teleport_ray = Physics2D.Raycast(this.transform.position + (Vector3.down * Player_Y * 0.5f), Vector2.down, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                    break;
            }
            if (teleport_ray.collider!=null)
            {
                switch (teleport_direction)
                {

                    case 0:
                        teleport_pos = teleport_ray.point - (Vector2.right * Player_X * 0.5f);
                        break;
                    case 1:
                        teleport_pos = teleport_ray.point - (Vector2.left * Player_X * 0.5f);
                        break;
                    case 2:
                        teleport_pos = teleport_ray.point - (Vector2.up * Player_Y * 0.5f);
                        break;
                    case 3:
                        teleport_pos = teleport_ray.point - (Vector2.down * Player_Y * 0.5f);
                        break;
                }
            }
            else
            {
                switch(teleport_direction)
            {
                
                case 0:
                        teleport_pos = (Vector2)this.transform.position + (Vector2.right * teleport_length);
                    break;
                case 1:
                        teleport_pos = (Vector2)this.transform.position + (Vector2.left * teleport_length);
                        break;
                case 2:
                        teleport_pos = (Vector2)this.transform.position + (Vector2.up * teleport_length);
                        break;
                case 3:
                        teleport_pos = (Vector2)this.transform.position + (Vector2.down * teleport_length);
                        break;
                }
            }
            RaycastHit2D chk_ray1 = Physics2D.Raycast(teleport_pos, Vector2.up, Player_Y * 0.5f, LayerMask.GetMask("platform_can't_pass"));
            RaycastHit2D chk_ray2= Physics2D.Raycast(teleport_pos, Vector2.down, Player_Y * 0.5f, LayerMask.GetMask("platform_can't_pass"));
            switch (teleport_direction)
            {
                case 0:
                case 1:
                    chk_ray1 = Physics2D.Raycast(teleport_pos, Vector2.up, Player_Y * 0.5f, LayerMask.GetMask("platform_can't_pass"));
                    chk_ray2 = Physics2D.Raycast(teleport_pos, Vector2.down, Player_Y * 0.5f, LayerMask.GetMask("platform_can't_pass"));
                    break;
                case 2:
                case 3:
                    chk_ray1 = Physics2D.Raycast(teleport_pos, Vector2.right, Player_X * 0.5f, LayerMask.GetMask("platform_can't_pass"));
                    chk_ray2 = Physics2D.Raycast(teleport_pos, Vector2.left, Player_X * 0.5f, LayerMask.GetMask("platform_can't_pass"));
                    break;
            }



            if (!chk_ray1.collider && !chk_ray2.collider)
            {
                transform.position = (Vector3)teleport_pos + (Vector3.forward * this.transform.position.z);
            }
            else if (!chk_ray1.collider && chk_ray2.collider)
            {
                switch (teleport_direction)
                {
                    case 0:
                    case 1:
                        transform.position =(Vector3)( chk_ray2.point + Player_Y * 0.5f * Vector2.up+Vector2.up*0.2f) + (Vector3.forward * this.transform.position.z);

                        break;
                    case 2:
                    case 3:
                        transform.position = (Vector3)(chk_ray2.point + Player_X * 0.5f * Vector2.right) + (Vector3.forward * this.transform.position.z);

                        break;
                }
            }
            else if (chk_ray1.collider && !chk_ray2.collider)
            {
                switch (teleport_direction)
                {
                    case 0:
                    case 1:
                        transform.position = (Vector3)(chk_ray2.point - Player_Y * 0.5f * Vector2.up - Vector2.up * 0.2f) + (Vector3.forward * this.transform.position.z);

                        break;
                    case 2:
                    case 3:
                        transform.position = (Vector3)(chk_ray2.point - Player_X * 0.5f * Vector2.right) + (Vector3.forward * this.transform.position.z);

                        break;
                }
            }
            else
            {

                switch (teleport_direction)
                {
                    case 0:

                        chk_ray2 = Physics2D.Raycast((Vector2)transform.position - Player_Y * 0.5f * Vector2.up, Vector2.right, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                        teleport_pos = new Vector2(chk_ray2.point.x, transform.position.y);
                        break;
                    case 1:
                       
                        chk_ray2 = Physics2D.Raycast((Vector2)transform.position- Player_Y * 0.5f * Vector2.up, Vector2.left, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                        teleport_pos = new Vector2(chk_ray2.point.x, transform.position.y);
                        break;
                    case 2:
                        chk_ray2 = Physics2D.Raycast((Vector2)transform.position - Player_X * 0.5f * Vector2.right, Vector2.up, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                        teleport_pos = new Vector2( transform.position.x, chk_ray2.point.y);
                        break;
                    case 3:
                        chk_ray2 = Physics2D.Raycast((Vector2)transform.position - Player_X * 0.5f * Vector2.right, Vector2.down, teleport_length, LayerMask.GetMask("platform_can't_pass"));
                        teleport_pos = new Vector2(transform.position.x, chk_ray2.point.y);
                        break;
                }
                transform.position = (Vector3)teleport_pos + (Vector3.forward * this.transform.position.z);
            }

            AE_Teleport();
            can_attack = false;
            dash_count--;
            can_dash = false;
            dash_recover_check = false;
        }
    }
    void teleport_end()
    {
        can_attack = true;
        untouchable_state = false;
        on_teleport = false;
    }
    void dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("대쉬버튼 활성화");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash_count != 0 && can_dash)
        {
            Gamemanager.GM.game_ev.when_dash_key_input(transform);
            Debug.Log("대시");
            m_audio.Dash();
            rgd.velocity = Vector2.zero;
            
            can_attack = false;
            p_anim.dash();
            untouchable_state = true;
            make_dash_ghost();
            on_dash = true;
            dash_timer = dash_time;
            
            Debug.Log(Player_status.p_status.get_dash_force());
            //dash_direction =  Vector2.right*direction * Player_status.p_status.get_dash_force();
            dash_direction = Vector2.right * direction * dash_force;
            Debug.Log(dash_direction);
            rgd.AddForce(dash_direction *  Time.deltaTime, ForceMode2D.Impulse);
            dash_count--;
            can_dash = false;
            dash_recover_check = false;
        }

    }
    void make_dash_ghost()
    {
        var a = player_ghost_pulling();
        choose_ghost_effect=a;
        a.SetActive(true);
        
    }
    void dash_end()
    {
        Gamemanager.GM.game_ev.Dash_End_effect();
        rgd.velocity = Vector2.zero;
        untouchable_state = false;
        p_anim.dash_end();
        can_attack = true;
        can_move = true;
        rgd.gravityScale = 1;
        on_dash =false;
    }
    public void set_teleport_direction()
    {
        if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))
        {
            teleport_direction = 0;
        }else if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.LEFT]))
        {
            teleport_direction = 1;
        }
        else if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.UP]))
        {
            teleport_direction = 2;
        }
        else if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.DOWN]))
        {
            teleport_direction = 3;
        }
        else
        {
            if (direction == 1)
            {
                teleport_direction = 0;
            }
            else
            {
                teleport_direction = 1;
            }
        }
    }
    new void character_move()
    {
        //이동속도를 건들 때 move_speed와 velocity.x의 제한값을 건들자
        if (can_move&&hitted_timer<=0)
        {
            if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))
            {
                lastest_key_input = Key_manager.Keys[Key_manager.KeyAction.RIGHT];
            }
       if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))
            {
                lastest_key_input = Key_manager.Keys[Key_manager.KeyAction.LEFT];
            }
            if (Input.GetKeyUp(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))
            {
                lastest_key_input = Key_manager.Keys[Key_manager.KeyAction.LEFT];
            }
           if (Input.GetKeyUp(Key_manager.Keys[Key_manager.KeyAction.LEFT]))
            {
                lastest_key_input = Key_manager.Keys[Key_manager.KeyAction.RIGHT];
            }
           
            
            if(Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.RIGHT]) && !Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.LEFT])){
                lastest_key_input = Key_manager.Keys[Key_manager.KeyAction.RIGHT];
            }else if(!Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.RIGHT]) && Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.LEFT]))
            {
                lastest_key_input = Key_manager.Keys[Key_manager.KeyAction.LEFT];
            }
            
            if ((Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.RIGHT]) || Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.LEFT]) ||
                    Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]) || Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT])) && !p_anim.sword_delay && !on_dash && !on_teleport && !Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
            {
                if (lastest_key_input == Key_manager.Keys[Key_manager.KeyAction.RIGHT])
                {
                    p_anim.move_state = true;
                    if (direction == -1)
                    {
                        direction_change();
                    }
                    //move_buffer = move_buffer_timer;
                 //   move_vector = new Vector3(direction * move_speed * Time.deltaTime, 0, 0);

                    //direct_vector = new Vector2(1, direct_vector.y);
                    if(move_weight!=0)
                    rgd.velocity = new Vector2(direction * move_speed * move_weight, rgd.velocity.y);
                    
                    RaycastHit2D wall_ray = Physics2D.Raycast(this.transform.position + Vector3.up * Player_Y * 0.5f, Vector3.right, Player_X, LayerMask.GetMask("platform_can't_pass"));
                    RaycastHit2D wall_ray2 = Physics2D.Raycast(this.transform.position - Vector3.up * Player_Y * 0.85f, Vector3.right, Player_X, LayerMask.GetMask("platform_can't_pass"));
                    Debug.DrawRay(this.transform.position - Vector3.up * Player_Y * 0.85f, Vector3.right * Player_X, Color.red);
                    Debug.DrawRay(this.transform.position + Vector3.up * Player_Y * 0.5f, Vector3.right * Player_X, Color.red);
                    if (wall_ray.collider || wall_ray2.collider)
                    {
                        /*Debug.Log("collide");
                        var wall_direction = wall_ray.transform.position - this.transform.position;
                        float wall_direction_size = Mathf.Log(Mathf.Pow(wall_direction.x, 2) + Mathf.Pow(wall_direction.y, 2));
                        Debug.Log(wall_direction_size);
                        if (wall_direction_size < 1)
                        {
                            move_weight = wall_direction_size;
                        }
                        else
                        {
                            move_weight = 1;
                        }*/
                        move_weight *= 0.05f;
                    }
                    else
                    {
                        move_weight = 1;
                    }


                   // transform.Translate(move_vector * move_weight);
                }
                else if (lastest_key_input == Key_manager.Keys[Key_manager.KeyAction.LEFT])
                {
                    p_anim.move_state = true;
                    if (direction == 1)
                    {
                        direction_change();
                    }
                    //move_buffer = move_buffer_timer;
                   // move_vector = new Vector3(direction * move_speed * Time.deltaTime * -1, 0, 0);
                    direct_vector = new Vector2(-1, direct_vector.y);
                    if (move_weight != 0)
                        rgd.velocity = new Vector2(direction * move_speed*move_weight, rgd.velocity.y);

                    RaycastHit2D wall_ray = Physics2D.Raycast(this.transform.position+Vector3.up* Player_Y*0.5f, Vector3.left, Player_X, LayerMask.GetMask("platform_can't_pass"));
                    RaycastHit2D wall_ray2 = Physics2D.Raycast(this.transform.position - Vector3.up * Player_Y * 0.85f, Vector3.left, Player_X, LayerMask.GetMask("platform_can't_pass"));
                    Debug.DrawRay(this.transform.position - Vector3.up * Player_Y * 0.85f, Vector3.left * Player_X, Color.red);
                    Debug.DrawRay(this.transform.position + Vector3.up * Player_Y * 0.5f, Vector3.left * Player_X, Color.red);
                    if (wall_ray.collider||wall_ray2.collider)
                    {
                        /* Debug.Log("collide");
                         var wall_direction = wall_ray.transform.position - this.transform.position;
                         float wall_direction_size = Mathf.Log(Mathf.Pow(wall_direction.x, 2) + Mathf.Pow(wall_direction.y, 2));
                         Debug.Log(wall_direction_size);
                         if (wall_direction_size < 1)
                         {
                             move_weight = wall_direction_size;
                         }
                         else
                         {
                             move_weight = 1;
                         }*/
                        move_weight *= 0.05f;
                    }
                    else
                    {
                        move_weight = 1;
                    }

                   // SpriteRenderer s;

                }
                if (rush_timer < rush_time&&!on_rush)
                {
                    rush_timer += Time.deltaTime;
                }
                else
                {
                    on_rush = true;
                    rush_timer = 0;
                }
            }
            else
            {
                if(!on_dash)
                    rgd.velocity = new Vector2(rgd.velocity.x * 0.65f, rgd.velocity.y);
                if (rgd.velocity.x < 1 && rgd.velocity.x > -1&&on_rush)
                {
                    on_rush = false;
                }
                direct_vector = new Vector2(0, direct_vector.y);
                p_anim.move_state = false;
            }
            jump();
            // rgd.AddForce(new Vector2(direction * move_speed * 0.5f, 0),ForceMode2D.Impulse);
            /* }
             else if (Input.GetKey(Key_manager.Keys[Key_manager.KeyAction.LEFT]) && !p_anim.sword_delay && !on_dash && !on_teleport)
             {
                 p_anim.move_state = true;
                 if (direction == 1)
                 {
                     direction_change();
                 }
                 //move_buffer = move_buffer_timer;
                 move_vector = new Vector3(direction * move_speed * Time.deltaTime * -1, 0, 0);
                 direct_vector = new Vector2(-1, direct_vector.y);

                 //rgd.velocity = new Vector2(direction * move_speed, rgd.velocity.y);
                 RaycastHit2D wall_ray = Physics2D.Raycast(this.transform.position, Vector3.left, Player_X, LayerMask.GetMask("platform_can't_pass"));
                 Debug.DrawRay(this.transform.position, Vector3.left * Player_X, Color.red);
                 if (wall_ray.collider)
                 {
                     /* Debug.Log("collide");
                      var wall_direction = wall_ray.transform.position - this.transform.position;
                      float wall_direction_size = Mathf.Log(Mathf.Pow(wall_direction.x, 2) + Mathf.Pow(wall_direction.y, 2));
                      Debug.Log(wall_direction_size);
                      if (wall_direction_size < 1)
                      {
                          move_weight = wall_direction_size;
                      }
                      else
                      {
                          move_weight = 1;
                      }*/
            // move_weight *= 0.93f;
            // }
            //  else
            //  {
            //      move_weight = 1;
            //   }
            //
            //   SpriteRenderer s;

            // transform.Translate(move_vector * move_weight);
            //rgd.AddForce(new Vector2(direction * move_speed*0.5f, 0), ForceMode2D.Impulse);
            //}








            /* if (Input.GetButtonDown("Down"))
             {
                 p_anim.crouch_state=true;
             }else if (Input.GetButtonUp("Down"))
             {
                 p_anim.crouch_state = false;
             }*/

            if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]) && on_platform&&Downbuffertimer<=0 && !p_anim.sword_delay && !on_dash && !on_teleport)
            {
                Downbuffertimer = Downbuffertime;
            }
            if (Downbuffertimer > 0)
            {
                Downbuffertimer -= Time.deltaTime;
                if(Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]) && on_platform && Downbuffertimer >= 0 && !p_anim.sword_delay && !on_dash && !on_teleport)
                {
                    //layer_change_timer = layer_change_time;
                    if(!on_corutine_1)
                        StartCoroutine(downplatform());
                }
            }
            /*if (layer_change_timer > 0)
            {
                layer_change_timer -= Time.deltaTime;

            }
            else
            {
                this.gameObject.layer = 6;
            }*/
        }
        if (!Sp_ItemEffect.sp_itemeffect.Sp_Ef[7])//여기에 텔레포트 조건
        {
            if (!on_dash)
            {
                dash();
            }
            else
            {
                rgd.gravityScale = 0;
                if(choose_ghost_effect!=null)
                    choose_ghost_effect.transform.position = this.transform.position;
                untouchable_state = true;
                dash_timer -= Time.deltaTime;

                if (dash_timer <= 0)
                {
                    dash_end();
                }
            }
            dash_recover();
        }
        else
        {
            set_teleport_direction();
            if (!on_teleport)
            {
                teleport();
            }
            else
            {
                untouchable_state = true;
                teleport_untouable_timer -= Time.deltaTime;
                if (teleport_untouable_timer <= 0)
                {
                    teleport_end();
                }
            }
            dash_recover();
        }
        
    }
   
    void minimum_jump()
    {
        jump_count--;
        //rgd.velocity = new Vector2(rgd.velocity.x, minimum_jump_vec3.y);
        minimum_jump_timer = minimum_jump_time;
        rgd.velocity = new Vector2(rgd.velocity.x, 0);
        rgd.AddForce(new Vector2(rgd.velocity.x, Player_status.p_status.get_jump_force()), ForceMode2D.Impulse);
        raycheck = false;
    }
    public void death()
    {
        if (Gamemanager.GM.game_ev.is_player_death())
        {
            Debug.Log("죽음!");
            death_check = true;

           // cameraManager.cm.active_main_cam();
            record.Died++;
            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            Debug.Log("부활");
            p_anim.resurrection();
           
        }
    }
   public void ressurection()
    {

       // cameraManager.cm.active_main_cam();
        Player_status.p_status.set_hp(Gamemanager.GM.game_ev.resurrection_hp);
        untouchable_timer = 2f;
        death_anim_check = false;
        Gamemanager.GM.can_handle = true;
    }
    public void player_hitted(int dmg)
    {
        if (!untouchable_state && Player_status.p_status.get_hp() > 0)
        {
            if (Player_status.p_status.get_hp() - dmg <= 0)
            {
                cameraManager.cm.active_action_cam();
                cameraManager.cm.action_cam.a = action_camera.action.p_death_cam;
            }
            int d = dmg;
            Debug.Log("데미지 처리!");
            m_audio.attacked();
            Player_status.p_status.damage_hp(d, DNP, this.gameObject.transform);
            record.Hit += d;
            if (!untouchable_state)
                untouchable_state = true;
        }
    }
    void untouchable()
    {
        if (untouchable_timer >= Player_status.p_status.get_untouchable_time())
        {
            Debug.Log("무적해제!");
            untouchable_state = false;
            untouchable_timer = 0;
        }
        untouchable_timer += Time.deltaTime;
    }
    void player_hiited_force(Vector3 col_pos)
    {
        if (!untouchable_state && Player_status.p_status.get_hp() > 0)
        {
            Gamemanager.GM.game_ev.when_player_hitted(transform);
            Vector3 col_direct = transform.position - col_pos;
            int hit_direct;
            if (col_direct.x > 0)
            {
                hit_direct = -1;
            }
            else
            {
                hit_direct = 1;
            }
            rgd.velocity = Vector2.zero;
            can_move = false;
            can_attack = false;
            //rgd.gravityScale = 1;
            rgd.AddForce(new Vector2(hitted_force.x * hit_direct, hitted_force.y), ForceMode2D.Impulse);
            on_hitted = true;
            p_anim.Hit_state = true;
            hitted_timer = hitted_time;
        }
    }
    public void player_hiited_force(Vector3 col_pos,Vector2 hitted)
    {
        if (!untouchable_state&&Player_status.p_status.get_hp() > 0)
        {
            Gamemanager.GM.game_ev.when_player_hitted(transform);
            Vector3 col_direct = transform.position - col_pos;
            int hit_direct;
            if (col_direct.x > 0)
            {
                hit_direct = 1;
            }
            else
            {
                hit_direct = -1;
            }
            rgd.velocity = Vector2.zero;
            can_move = false;
            //rgd.gravityScale = 1;
            rgd.AddForce(new Vector2(hitted.x * hit_direct, hitted.y), ForceMode2D.Impulse);
            on_hitted = true;
            p_anim.Hit_state = true;
            hitted_timer = hitted_time;
        }
    }
    void hitted_end()
    {
        rgd.velocity = Vector2.zero;
        can_move = true;
        can_attack = true;
        //rgd.gravityScale = 1;
        on_hitted = false;
    }
    public void hitted_event(Vector2 v)
    {
        player_hiited_force(v);
    }
    public void hitted_event(Vector2 v,Vector2 f)
    {
        player_hiited_force(v,f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!on_dash )
        {
            if (!on_teleport)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    if (!untouchable_state)
                    {
                        Gamemanager.GM.stop_combo();
                        col_pos = collision.transform.position;
                        player_hiited_force(col_pos);
                        player_hitted(collision.gameObject.GetComponentInParent<GameCharacter>().Attack_point);

                        Debug.Log("피격됨");

                    }
                }
            }
        }
        else
        {
            Gamemanager.GM.game_ev.On_Dash_col_effect(collision);
        }
    }

    void OnCollisionStay2D(Collision2D other) // trigger? collision?
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            if (other.gameObject.layer == 11)
            {
                on_platform = true;
            }

        }

        
    }
    public void groundcollision(GameObject a)
    {
        if (a.layer != 11)
        {

                Debug.Log("점프회복");
                jump_count = Player_status.p_status.get_jump_count();
                dash_recover_check = true;
                onground = true;
            Player_status.p_status.air_attack_num = Player_status.p_status.air_attack_num_orignal + Player_status.p_status.air_attack_num_bonus;
                raycheck = true;

        }
        else
        {

            if (rgd.velocity.y<1&&rgd.velocity.y>-1)
            {
               
                    Debug.Log("점프회복");
                    jump_count = Player_status.p_status.get_jump_count();
                    dash_recover_check = true;
                    onground = true;
                Player_status.p_status.air_attack_num = Player_status.p_status.air_attack_num_orignal + Player_status.p_status.air_attack_num_bonus;
                raycheck = true;
                
            }



        }
        
    }
    void OnCollisionEnter2D(Collision2D other) // trigger? collision?
    {
        if (other.gameObject.CompareTag("Platform")&&other.gameObject.layer!=11)
        {
            this.gameObject.layer = 6;
           // this.GetComponent<player_room_boost_mode>().un_boost();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.gameObject.layer == 11)
            {
                on_platform = false;
            }
        }
    }
    IEnumerator downplatform()
    { // 처음에 FireState를 false로 만들고
        var wait = new WaitForSeconds(layer_change_time);
        on_corutine_1 = true;
        this.gameObject.layer = 10;
        yield return wait;
       
        on_corutine_1 = false;
        this.gameObject.layer = 6;
        Debug.Log("코루틴해제");
    }
    void AE_Teleport()
    {




        Instantiate(m_teleport_effect, this.transform.position, Quaternion.identity);


    }
    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position-Vector3.up*0.2f + new Vector3(dustXOffset * direction, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(direction, 1, 1);
        }
    }


}



/* 최소 점프 높이
 * getbutton
 * timer 누르는 동안 점프높이=점프높이+%%%
*/