using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{
    
    public GameObject Enemy_destroy_effect;
    public GameObject player_minimap_pos;
    public GameObject enemy_minimap_pos;
    public GameObject portal_minimap_pos;
    public GameObject shop_minimap_pos;
    public GameObject event_minimap_pos;
    public GameObject drop_consumable_item;
    public Vector3 lastest_enemy_point;
    public int lastest_get_money;
    public int lastest_lose_money;
    public bool get_money_chk;
    public Item lastest_get_item;
    public bool get_item_chk;
    public bool lose_money_chk;
    public List<Item> loot_box = new List<Item>();
    public List<GameObject> bad_status_effect = new List<GameObject>();
    public List<GameObject> destroy_particle_instansi = new List<GameObject>();
    public ParticleSystem destroy_particle;
    public ParticleSystem heal_cross_particle;
    public ParticleSystem p_sword_effect;
    public ParticleSystem money_used_particle;
    public ParticleSystem enemy_hitted_particle;
    public ParticleSystem enemy_hitted_critical_particle;
    public GameObject Player;
    public GameObject Player_create;
    public GameObject Player_obj;
    public boss_map_system b_room_controller;
    public room_controller room_controller;
    public GameObject Player_spawn;
    public bool spawn_check;
    public static Gamemanager GM;
    public bool Onbattle;
    public GameObject sp_item_frame;
    public GameObject inventory_ui;
    public bool map_mode;
    public int stage=0;//0 게임 시작 안함 1~....게임중
    public bool boss;
    public GameObject fade_init;
    public GameObject fade_Outit;
    public bool fade_in_complete;
    public bool fade_out_complete;
    public bool can_handle;
    public GameObject End_panel;
    public int stage_end_line;
    public bool boss_clear;
    public bool room_end;
    public int room_num;
    public List<GameObject> Game_ui=new List<GameObject>();
    public List<GameObject> Main_UI = new List<GameObject>();
    public GameObject Game_sys;
    [Header("loop_upper")]
    public float atk_upr;
    public float spd_upr;
    public float hp_upr;
    public float b_spd_upr;
    public Game_Event game_ev=new Game_Event();
    public GameObject sp_item_UI;
    public bool sp_item_check;
    float t = 0;
    bool back_to_main;
    public bool initializing;

    public GameObject Totalbook;
    public int combo;
    public float combo_duration_max;
    float combo_duration;
    public ItemDatabase itemDB;
    public int last_enemy;
    public bool get_sp_item()
    {
        if (!sp_item_frame.activeSelf)
        {
            BookButtonManger.bookButtonManger.ButtonTimerON2();
            sp_item_frame.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void get_item()  //아이템 생성
    {
        if (itemDB == null)
        {
            itemDB = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        }
        var item = itemDB.get_item_by_rarity(itemDB.item_list); //아이템 생성
        itemDB.Make_Get_item(item);   //중복찾기
        Debug.Log(item.Name + "아이템 들어감");
        game_ev.when_player_get_item(item); //아이템 효과 적용


        lastest_get_item = item.CreateItem();
        get_item_chk = true;
    }

    public void get_item(Item i)        //상점
    {
        if (itemDB == null)
        {
            itemDB = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
            ;
        }
        Debug.Log(i.Name + "아이템 들어감2");
        itemDB.Make_Get_item(i);

        game_ev.when_player_get_item(i);

        lastest_get_item = i.CreateItem();
        get_item_chk = true;
    }

    public void get_item_SP(Item i)
    {
        if (itemDB == null)
        {
            itemDB = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
            ;
        }

        itemDB.Make_Get_item(i);
        //ItemEffect0.item0to10.uneffect(itemDB.item_list[n]);      뭔지몰라 일단 주석처리
    }
    private void Awake()
    {
        room_num = 5;
        GM = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        can_handle = true;
        
        DontDestroyOnLoad(this.gameObject);
        DonDestoryManage.DDM.DDM_List.Add(this.gameObject);
        for(int i = 0; i < 5; i++)
        {
            var a = Instantiate(destroy_particle.gameObject, this.transform);
            destroy_particle_instansi.Add(a);
            a.SetActive(false);
        }
    }
    public GameObject destroy_particle_pulling()
    {
        int index = 0;
        for(int i = 0; i < destroy_particle_instansi.Count;i++)
        {
            if (!destroy_particle_instansi[i].activeSelf)
            {
                return destroy_particle_instansi[i];
            }
        }
        return destroy_particle_instansi[index];
    }
    void combo_system()
    {
        if (combo > 0)
        {

            combo_duration -= Time.deltaTime;
            if (combo_duration <= 0)
            {
                stop_combo();
            }
        }
    }
    public void get_combo()
    {
        combo++;
        combo_duration = combo_duration_max;
    }
    public void stop_combo()
    {
        combo = 0;
        combo_duration = 0;
    }
    void loadMain2()
    {
        if (fade_out_complete)
        {
            LoadingSceneManager.l_scenemanager.LoadMain();
            //Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!fade_init.activeSelf)
            fade_in_complete = false;
        if (!fade_Outit.activeSelf)
            fade_out_complete = false;
        if (stage == 0) {
            for(int i = 0; i < Game_ui.Count; i++)
            {
                Game_ui[i].SetActive(false);
            }
            Game_sys.SetActive(false);
        }
        else {
            if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.PAUSE]))
                {
                if (Totalbook.activeSelf)
                {
                    Totalbook.SetActive(false);
                }
                else
                {
                    Totalbook.SetActive(true);
                }

            }

            for (int i = 0; i < Game_ui.Count; i++)
            {
                Game_ui[i].SetActive(true); 
            }
            Game_sys.SetActive(true);
            if (back_to_main)
                {
                    loadMain2();
                }
            //if (stage != 0)
            // {
            if (!boss)
            {
                on_game();
                if (Onbattle)
                {
                    //mapui.SetActive(false);
              
                }
                else
                {
          


                  
                }
            }
            else
            {
                on_game_b();
            }
        }
        //}
    }
    void on_game_b()
    {
        if (b_room_controller == null)
        {
            b_room_controller = GameObject.Find("boss_map_system").GetComponent<boss_map_system>();
        }
        else
        {
            if (b_room_controller.map_making_complete && !initializing)
            {
                
                if (Player_spawn == null && !initializing)
                {
                    //Debug.Log("ez");
                    //Player_spawn = GameObject.Find("Player spawner");
                    Player_spawn = GameObject.FindGameObjectWithTag("Spawner");
                }
                if (Player_create == null&&!spawn_check && !initializing)
                {
                   
                    fade_in();

                    fade_Outit.SetActive(false);
                    fade_out_complete = false;
                    Player_create = Instantiate(Player);
                    Player_obj = Player_create.transform.GetChild(0).gameObject;
                    Player_obj.transform.position = Player_spawn.transform.position;
                    can_handle = true;
                }

                if (!spawn_check && Player_create != null && Player_spawn != null&& !initializing)
                {
                    
               
                   
                    Player_obj.SetActive(true);
                    spawn_check = true;
                }
            }
            
            if (room_end&&fade_out_complete)
            {
                room_end = false;
                if(!initializing)
                    GM_initialize();
            }
            if (stage == stage_end_line&&boss_clear)
            {
                End_panel.SetActive(true);
            }
        }
    }
    public void GM_initialize()
    {
        initializing = true;
            Destroy(Player_spawn);
        
            if (!Gamemanager.GM.boss)
            {
                int a = room_controller.room.Count;
                for (int i = a - 1; i == 0; i--)
                {
                    Destroy(room_controller.room[i]);
                    room_controller.room.RemoveAt(i);
                }
            room_controller.map_making_complete = false;
            LoadingSceneManager.l_scenemanager.Loadboss();
            boss = true;
                
            }
            else
            {
            int b = b_room_controller.boss_room_prefab.Length;
            for(int i = b - 1; i == 0; i--)
            {
                Destroy(b_room_controller.boss_room_prefab[i]);
            }
            LoadingSceneManager.l_scenemanager.LoadStage();
                boss = false;
               
            }
        room_num = 5;
            room_controller = null;
            b_room_controller = null;
            Player_spawn = null;
            Player_create = null;
            spawn_check = false;
            boss_clear = false;
        sp_item_check = false;

        Player_status.p_status.spawn_check = false;
        t = 0;
    }
    public void loadMain()
    {

        fade_out();
        back_to_main = true;
        
       
    }
   
    void on_game()
    {

        if (room_controller == null&& GameObject.Find("map_system")!=null)
            room_controller = GameObject.Find("map_system").GetComponent<room_controller>();
        else
        {
            if (room_controller.map_making_complete && !initializing)
            {
                //Debug.Log("dz");
                if (Player_spawn == null)
                {
                    //Debug.Log("ez");
                    //Player_spawn = GameObject.Find("Player spawner");
                    Player_spawn = GameObject.FindGameObjectWithTag("Spawner");
                    
                }
                if (Player_create == null && !spawn_check && !initializing)
                {
                    Player_create = Instantiate(Player);
                    Player_create.transform.position = Vector3.zero;
                    fade_in();
                    fade_Outit.SetActive(false);
               
                    Player_obj = Player_create.transform.GetChild(0).gameObject;
                    Player_obj.transform.position = Player_spawn.transform.position;
                    can_handle = true;
                }

                if (!spawn_check && Player_create != null && Player_spawn != null && !initializing)
                {
             
                   
                   
                    Player_obj.SetActive(true);
                    spawn_check = true;
                }
                if (room_num == 0)
                {
       
                    fade_out();
                    room_end = true;
                }
                if (room_end && fade_out_complete)
                {
                    room_end = false;
                    if (!initializing)
                        GM_initialize();
                    
                }

            }
        }
    }
    public void fade_in()
    {
        if(Player_obj!=null)
            Player_obj.GetComponent<PlayerCharacter>().Player_canvas.SetActive(true);
        fade_init.SetActive(true);
    }
    public void fade_out()
    {

        can_handle = false;
        if (Player_obj != null)
            Player_obj.GetComponent<PlayerCharacter>().Player_canvas.SetActive(false);
        fade_Outit.SetActive(true);
    }
    public Transform get_player_transform()
    {
        return Player_obj.transform;
    }
    public void choose_sp_item(Item i)
    {
        game_ev.when_sp_item_will_get(i);
    }

}
