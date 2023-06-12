using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    public GameObject end_portal;
    public int enemy_count;
    public Transform consumable_drop;
   // public List<GameObject> consumable_created=new List<GameObject>();
    public GameObject Inven;
    public float room_width;
    public float room_height;
    public door[] door;
    public bool event_clear;
    public int room_element;
    public bool room_cleared;//방을 가봤는지
    public GameObject Itemsprite;
    [Header("normal_contents")]
    public GameObject[] enemy;
    public bool delay_check;
    public float delaytime=1.0f;
    public float delaytimer;
    int count;
    int Itemtype;
    public bool on_player;
    private bool open_check;
    bool item_given_check;
    public Item item = new Item();  //아이템
    public ItemDatabase ItemDatabase;
    public int door_num;
    public Vector2 XY;
    public List<room> r_connect;
    public List<room> r_connected;
    public int r_length;
    public int connect_num;
   // public GameObject end_door;
    public Transform[] spawn_door_pos;
    public GameObject exit_portal;
    public bool move_chk;
    public bool move_chk2;
    public float active_timer;
    public GameObject item_get_ui;
    public Vector2 size;
    public GameObject camera_point;
    public GameObject n_contents;
    public bool x_pin;
    public bool y_pin;
    public float tim = 1.2f;
    AudioManage_Main m_audio;
    float clear_slow_timer = 1.5f;
    float clear_slow_time;
    public room()
    {
        room_element = 1;
    }
    public room(int a)
    {
        room_element = a;
    }
    public room(int a,int b,int c)
    {
        room_element = a;
        r_length = b;
        connect_num = Random.Range(1, c+1);
        if (connect_num == c + 1)
            connect_num = c;
       
    }
    public void make_clear_room()
    {
        if (r_connect.Count != 0)
        {
            for (int i = 0; i < r_connect.Count; i++)
            {

                // GameObject a = Instantiate(end_door);
                spawn_door_pos[i].gameObject.SetActive(true);
                spawn_door_pos[i].GetComponent<portallV2>().connenct_room = r_connect[i].gameObject;
                spawn_door_pos[i].GetComponent<portallV2>().this_room = this.gameObject;


            }
        }
        else
        {
            Instantiate(end_portal, spawn_door_pos[0].position, Quaternion.identity);
        }
    }
    public room(room a)
    {
        room b = this;
        b = a;
    }
    public void set_room(room r)
    {

    }
    public void set_connect_num(int a)
    {
        connect_num = a;
    }
    public void set_door(List<room> a, List<room> d,int b,int c)
    {
        r_connect = a;
        r_connected = d;
        room_element = b;
        r_length = c;
    }
    public void set_door(int a, int b, int c)
    {
        connect_num = a;
        room_element = b;
        r_length = c;
    }
    public void set_XY(float x, float y)
    {
        XY.x=x;
        XY.y=y;
    }
    public Vector2 get_XY()
    {
        return XY;
    }
    public int get_XY_x()
    {
        return (int)XY.x;
    }
    public int get_XY_y()
    {
        return (int)XY.y;
    }

    void Start()
    {
        tim = 1.2f;
        clear_slow_timer = 0.5f;
        clear_slow_time = Time.deltaTime;
        if (room_element == 1)
        {
            on_player = true;
            move_chk = true;
        }
       // exit_portal.SetActive(false);
        delaytimer = delaytime;
        open_check = true;
       if (room_element == 2)
        {
            

           
     
        }
        else
        {
            //Itemsprite.SetActive(false);
        }
    }
    private void Awake()
    {
        m_audio = AudioManage_Main.instance;

    }
    public void active_room_element()
    {
        room_contents_controller r = this.gameObject.GetComponent<room_contents_controller>();
        r.content.SetActive(true);
    }
    void Update()
    {
        make_wall();
   
        if (on_player)
        {
            if (room_element == 2)//일반 전투 방
            {
                Gamemanager.GM.last_enemy = enemy_count;
                if (move_chk &&! Gamemanager.GM.fade_init.activeSelf)
                {
                    move_chk2 = true;
                  
                }
                if (move_chk2&&active_timer>0)
                {
                    active_timer-=Time.deltaTime;
                }
                if (active_timer<=0)
                {
                    active_timer = 0;
                    if (!room_cleared)
                    {
                        // if (collision_door_to_player_check())
                        //{
                        Gamemanager.GM.Onbattle = true;
                        battle_mode();

                        //}
                    }
                    else
                    {
                        if (Gamemanager.GM.Onbattle)
                        {
                            Gamemanager.GM.Onbattle = false;
                            make_clear_room();
                        }

                        //Gamemanager.GM.room_num--;
                        if (!item_given_check)
                        {
                            int rand = Random.Range(0, 100);
                            if (rand < 40)
                            {
                                
                               var a= Instantiate(Gamemanager.GM.drop_consumable_item,Gamemanager.GM.lastest_enemy_point, Quaternion.identity);
                                a.transform.parent = this.transform;
                            }
                            Gamemanager.GM.get_item();
                        }

                        item_given_check = true;
                        active_timer = 0;
                        move_chk2 = false;
                    }
                }
                
            }
            else if (room_element==4)
            {
                if (event_clear)
                {
                    make_clear_room();
                    event_clear = false;
                }
            }
            else
            {
                if (!room_cleared)
                {
                    make_clear_room();
                    room_cleared = true;
                }
                

            }
            /*if (!link_check)
            {
                check_door();
            }*/
        }

        if (!on_player && !room_cleared)
        {
            //this.gameObject.SetActive(false);
        }
    }

   void battle_mode()
    {
        
        
        normal_contents a = n_contents.GetComponent<normal_contents>();
     
        if (a.room_cleared)
        {

            if (clear_slow_timer > 0)
            {
                clear_slow_timer -= clear_slow_time;
                Time.timeScale = 0.5f;
            }
            else
            {
                if (Time.timeScale != 1)
                    Time.timeScale = 1;
                room_cleared = true;
            }
        }
        else
        {
            if (tim > 0)
            {
                tim -= Time.deltaTime;
            }
            else
            {
                if (a.choose_cycle != null && a.choose_cycle.choose_group != null)
                {
                    enemy_count = a.choose_cycle.choose_group.GetComponent<Enemy_group>().enemy.Count;
                   
                }
                if (a.cycle_chk && a.start_chk&&tim<=0)
                {
                    a.acitve_enemy();
             
                }
            }
           
        }
          /*  if (delay_check)
            {
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
                //적생성한번만
                if (enemy.Length == 0)
                {
                room_cleared = true;

                }
            }
            else
            {
                delaytimer -= Time.deltaTime;
                if (delaytimer <= 0)
                    delay_check = true;
            }*/
        
    }
    
    bool collision_door_to_player_check()
    {
        int count = 0;
        for (int i = 0; i < door.Length; i++)
        {
            if (door[i].link != null&&door[i].col_Player)
            {
                count++;
            }
        }
        if (count == 0)
        {
            return true;
        }
        else
            return false;
    }
    public void open_door()
    {
        for (int i = 0; i < door.Length; i++)
        {
            if (door[i].link != null)
            {
                door[i].open_door();
            }
            
        }
        open_check = true;
    }
    public void close_door()
    {
        for (int i = 0; i < door.Length; i++)
        {
            if (door[i].link != null)
            {
                door[i].close_door();
            }
        }
        open_check = false;
    }
    public void random_door()
    {
        while (count < 2)// 연결되지 않은 문이 하나라도 생길 때 까지
        {
            for (int i = 0; i < door.Length; i++)
            {
                if (door[i].link == null)
                    count += door[i].make_door_random();
            }
        }
    }
    public void make_wall()
    {
      
      // var room_controller = this.transform.parent.GetComponent<room_controller>();
        for(int i = 0; i < door.Length; i++)
        {
            if (door[i].link == null)
            {
                door[i].active_wall();
            }
            else
            {
                door[i].active_door();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Enemy"))
        {
            enemy.Add(other.gameObject);
        }*/
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //on_player = true;
            //CameraFollow.center = new Vector2(this.transform.position.x, this.transform.position.y);
        }
        if (collision.CompareTag("Enemy"))
        {
            //enemy.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //on_player = false;
            
        }
        if (collision.CompareTag("Enemy"))
        {
           // enemy.Remove(collision.gameObject);
        }
    }
    public void move_player()
    {
        GameObject p = Gamemanager.GM.Player_obj;
        p.transform.position = this.exit_portal.transform.position;
        Gamemanager.GM.fade_in();
        m_audio.portalExit();
        if (
        Gamemanager.GM.fade_Outit.activeSelf)
            Gamemanager.GM.fade_Outit.SetActive(false);
        Gamemanager.GM.game_ev.when_room_enter();
        Gamemanager.GM.can_handle=true;
        move_chk = true;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (camera_point!=null)
        {
            Gizmos.DrawWireCube(camera_point.transform.position, size);
        }
        else
        {
            Gizmos.DrawWireCube(this.gameObject.transform.position, size);
        }
      
    }
}
