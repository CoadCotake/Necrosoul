using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_controller : MonoBehaviour
{
    public int room_create_number;//방생성갯수
   
    int a;
    public GameObject[] start_room;
    public GameObject[] room_database;//방
    public GameObject[] event_room;
    public GameObject[] shop_room;
    public int shop_num;
    public int event_num;
    int event_count;
    public int floor_event_num;
    public int shop_room_percent;
    public int event_room_percent;
    public List<GameObject> room = new List<GameObject>();
    public List<Vector2> room_pos = new List<Vector2>();
    public List <room> room_database_V3 =new List<room>();
    public List<List<room>> room_database_V4 = new List<List<room>>();
    private int r_count;
    public bool make_wall_check;
    public bool map_making_complete;
    public bool make_map_elemental;
    public int x = 9;
    public int x_weight;
    int x_Min = 2;
    int x_Max = 7;
    public int y = 9;
    public int y_weight;
    int y_Min = 2;
    int y_Max = 7;
    public int[,] mini_map;
    public int event_room_num;
    int event_room_count;

    public float event_room_P;
    public int shop_room_num;
    int shop_room_int;
    int shop_room_count;
    minimap m_map;
    public List<room> r_List_n = new List<room>();//연결시킬 방(연결 안됨)
    public List<room> r_List_n2 = new List<room>();//연결시킬 방(연결됨);
    public Item item = new Item();  //아이템
    public ItemDatabase ItemDatabase;
    public List<int> r_length_count=new List<int>();
    //List<Dictionary<string, object>> m_Data = CSVReader.Read("map_making");
    // Start is called before the first frame update
    public void make_room_V3()//room 리스트를 만들고 룸리스트의 문 개수를 읽어서 그에 맞춰서 불러넣기
    {
        
        int make_room_num = room_create_number + 1;
        event_room_num = 1;
        int num = 0;
        for(int i = 0; i < make_room_num; i++)
        {
            Debug.Log(i);
            if (i == 0)
            {
                r_length_count.Add(1);
                room r = new room();
                int r_door_num = 1;
                r.set_connect_num(r_door_num);
                room_database_V3.Add(r);
                List<room> r_list = new List<room>();
               r_list.Add(r);
               room_database_V4.Add(r_list);
                r_length_count.Add(r_door_num);
                
            }else if(i== 1)
            {
                List<room> r_list = new List<room>();
                for (int o = 0; o <r_length_count[1]; o++)
                {
                    room r = new room(2, i, r_length_count[1]);
                    
                    room_database_V3.Add(r);
                    r_list.Add(r);
                }
                room_database_V4.Add(r_list);
                
            }
            else if (i == make_room_num - 1)
            {
                List<room> r_list = new List<room>();
                room r = new room(3, i, 0);
      
                r_list.Add(r);
                room_database_V4.Add(r_list);
                r_length_count.Add(1);
            }else if(i== (make_room_num - 1) * 0.5f)
            {
                int r_door_num;

                r_door_num = Random.Range(3, 6);
                List<room> r_list = new List<room>();
                room r = new room(3, i, r_door_num);
                r_list.Add(r);
                room_database_V4.Add(r_list);
                r_length_count.Add(1);
            }
            
            else
            {
                int r_door_num;
               
                    r_door_num = Random.Range(3,6);
                int e_num = 0;
               List<room> r_list = new List<room>();
                
                for (int o=0;o< r_door_num; o++)
                {
                    int rand=100;
                    if (event_count < event_num && e_num < floor_event_num)
                    {
                        rand = Random.Range(0, 100);
                    }
                        room r;
                    
                    if (rand < event_room_percent&&event_count<event_num&&e_num<floor_event_num)
                    {
                        r = new room(4, i, r_door_num);
                        event_count++;
                        e_num++;
                    }
                    else
                    {

                        r = new room(2, i, r_door_num);
                    }
                        room_database_V3.Add(r);
                        r_list.Add(r);
                    
                   //r.set_door(r_door_num);
                    
                }
                room_database_V4.Add(r_list);

                    r_length_count.Add(r_door_num);

               // num += room_database_V3[num].connect_num;
               
            }
            
        }
       make_room_V3_2(room_database_V4);
        connect_room_V3(room_database_V4);
       set_room_pos(room_database_V4);
        map_making_complete = true;
    }
   //생성->연결->재배치
    public void connect_room_V3(List<List<room>> r)
    {
        Debug.Log(r.Count - 1);
        for (int i = 0;i< r.Count-1; i++)
        {
            r_List_n = r[i + 1];//연결시킬 방(연결 안됨)
            r_List_n2 = new List<room>();//연결시킬 방(연결됨);
                                         // Debug.Log(r[i].Count);
                                         // Debug.Log(r[i+1].Count);
            for (int n = 0; n < r[i].Count; n++)
            {

                List<int> r_no_multiple = new List<int>();
               
                for (int a = 0; a < r[i][n].connect_num; a++)
                {
                    if (r_List_n.Count != 0)
                    {
                       
                        r[i][n].r_connect.Add(r_List_n[0]);
                        r_List_n[0].r_connected.Add(r[i][n]);
                        r_List_n2.Add(r_List_n[0]);
                        r_List_n.RemoveAt(0);
                    }
                    
                   /* else
                    {
                        Debug.Log("LLLK");
                        int r_num = Random.Range(0, r_List_n2.Count + 1);
                        int num = 0;
                        while (r_no_multiple.Contains(r_num))
                        {

                            r_num = Random.Range(0, r_List_n2.Count + 1);
                            num++;
                            if (num == 100)
                            {
                                Debug.Log("무한루프");
                                break;
                            }
                        }
                            r_no_multiple.Add(r_num);
                            r[i][n].r_connect.Add(r_List_n2[r_num]);
                            r_List_n2[r_num].r_connected.Add(r[i][n]);

                        

                    }*/
                }
                Debug.Log("r_list2:" + r_List_n2.Count);
                for (int ab = r_List_n2.Count; ab > 0; ab--)
                {
                    int r_num = Random.Range(0, ab - 1);
                    r_List_n.Add(r_List_n2[r_num]);
                    r_List_n2.RemoveAt(r_num);
                }
              
                

            }
//int mugen= 0;
       /*      while(r_List.Count != 0)
            {
                
                int r_num=Random.Range(0, r[i].Count+1);
                Debug.Log(r_num);
                r[i][r_num].r_connect.Add(r_List[0]);
                r_List2.Add(r_List[0]);
                r_List.RemoveAt(0);
                mugen++;
                if (mugen == 100)
                {
                    Debug.Log("무한루프2");
                    break;
                }
            }
            mugen = 0;
            while (r[i + 1].Count == 0)
            {
                r[i + 1].RemoveAt(0);
                mugen++;
                if (mugen == 100)
                {
                    Debug.Log("무한루프3");
                    break;
                }
            }
            r[i + 1] = r_List2;*/



        }
        
            
    }
    public void set_room_pos(List<List<room>> r)
    {
        int ac = 0;
        for (int i = 0; i < r.Count; i++)
        {
            for(int n = 0; n < r[i].Count; n++)
            {
                room a = r[i][n];
                float r_x = a.room_width * a.r_length;
                float r_y = a.room_height / (r_length_count[a.r_length] +1);
                if (ac == 0)
                {
                    ac = r_length_count[r[i][n].r_length] - 1;
                    //num++;
                }
                else
                {
                    ac--;
                }
                a.transform.SetParent(this.transform);
               
                    a.transform.position = new Vector3(r_x,r_y*(ac+1),this.transform.position.z);
                if (i != 0)
                {
                    a.gameObject.SetActive(false);
                }
                
                
            }
        }

    }
    public void make_room_V3_2(List<List<room>> r)
    {
        int ac = 0;
        int num = 0;
        int r_num = r.Count;
        for (int i = 0; i < r_num; i++)
        {
            int r_r_num = r[i].Count;
            for (int n = 0; n < r_r_num; n++)
            {
                int random_number;
                GameObject a = null;
                if (r[i][n].room_element == 1)
                {
                    if (Gamemanager.GM.stage == 1)
                    {
                        a = Instantiate(start_room[0]);
                    }
                    else
                    {
                        a = Instantiate(start_room[1]);
                    }
                }
                else if (r[i][n].room_element == 2)
                {
                    random_number = Random.Range(0, room_database.Length);
                    a = Instantiate(room_database[random_number]);
                }
                    if (i == 0 && n == 0)
                {
                    random_number = 0;
                }
                else if(r[i][n].room_element==3)
                {
                    a = Instantiate(shop_room[0]);
                }else  if(r[i][n].room_element == 4)
                {
                    a = Instantiate(event_room[0]);
                }

                 

                    a.GetComponent<room>().set_door(r[i][n].connect_num, r[i][n].room_element, r[i][n].r_length);
                
              
                /* while (r[i].Count == 0)
                 {
                     r[i + 1].RemoveAt(0);
                     mugen++;
                     if (mugen == 100)
                     {
                         Debug.Log("무한루프3");
                         break;
                     }
                 }*/
                
                room_database_V3.RemoveAt(0);
                r[i].Add(a.GetComponent<room>());
                room_database_V3.Add(a.GetComponent<room>());
                float r_x = a.GetComponent<room>().room_width * r[i][n].r_length;
                //float r_y = a.GetComponent<room>().room_height / (r_length_count[r[i].r_length] +1);
                if (ac == 0)
                {
                    ac = r_length_count[r[i][n].r_length] - 1;
                    //num++;
                }
                else
                {
                    ac--;
                }
                a.transform.SetParent(this.transform);
                //a.transform.position = new Vector2(r_x, 0);

                //a.GetComponent<room>().on_player = true;

               // Debug.Log("ac=" + ac + "i:" + i + "num:" + num);
            }
            for (int b = r_r_num; b > 0; b--)
            {
                if (r[i].Count != 0)
                    r[i].RemoveAt(0);
            }
            


        }
    }
    private void Awake()
    {
       // m_map = this.GetComponent<minimap>();

    }
    void Start()
    {
        if (shop_room_num != 0)
        {
            shop_room_int = (room_create_number - 3) / shop_room_num;
        }

       
        //make_all_link(room);
    }

    // Update is called once per frame
    void Update()
    {
        if (!map_making_complete&& !Gamemanager.GM.boss)
        {
            make_room_V3();
        }
       /* if (!map_making_complete)
        {
            if (room.Count == room_create_number && !make_wall_check)
            {
                room[room.Count - 1].GetComponent<room>().room_element = 4;
                close_no_link_door(room);
                make_wall_check = true;

            }
            else
            {
                if (!Gamemanager.GM.boss)
                    create_room_V2();
            }
        
            if (make_wall_check)
            {
                map_making_complete = true;
               // m_map.get_map_data();
            }
        }*/
        /*if (!map_making_complete)
        {
            if (room.Count == room_create_number && !make_wall_check)
            {
                room[room.Count - 1].GetComponent<room>().room_element = 4;
                close_no_link_door(room);
                make_wall_check = true;

            }
            else
            {
                if (!Gamemanager.GM.boss)
                {
                    make_room_V3();
                }
            }
        
            if (make_wall_check)
            {
                map_making_complete = true;
               // m_map.get_map_data();
            }
        }*/

    }

    public void make_map_element(room a)
    {

          
            if (room.Count == room_create_number)
            {
            a.room_element = 4;
            }
            else
            {
            if (shop_room_num != 0)
            {
                if (room.Count % shop_room_int == 0 && shop_room_count < shop_room_num)
                {
                    a.room_element = 6;
                    shop_room_count++;
                }
                else
                {
                    if (event_room_count < event_room_num)
                    {
                        float ran = Random.Range(0, 1);
                        if (ran <= event_room_P)
                        {
                            a.room_element = 3;
                            event_room_count++;
                        }
                        else
                        {
                            a.room_element = 2;
                        }
                    }
                    else
                    {
                        a.room_element = 2;
                    }
                }
            }
            else
            {
                a.room_element = 2;
            }
            }
        
    }
  
    public GameObject make_room_V2()//아무 방이나 만든다
    {
        mini_map = new int[x, y];
        int random_number = Random.Range(0, room_database.Length);
        GameObject a = Instantiate(room_database[random_number]);
        a.GetComponent<room>().set_XY(x / 2 , y / 2 );
        room.Add(a);
        a.GetComponent<room>().room_element = 1;
        a.GetComponent<room>().on_player = true;
        a.transform.SetParent(this.transform);
        mini_map[x /2, y /2] = 2;
   
        room_pos.Add(a.GetComponent<room>().get_XY());
        //a.GetComponent<room>().open_check_door();
        //a.gameObject.transform.position = new Vector3(0, 0, 0);
        return a;
    }

    door find_door(door[] a, int b)
    {

        List<door> room_ = new List<door>();
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].direct == b)
            {
                room_.Add(a[i]);
            }
        }
        int random = Random.Range(0, room_.Count);
        return room_[random];
    }
    
    public GameObject make_room_V2(GameObject r)//아무 방을 생성해 이어붙인다
    {
        room room_ = r.GetComponent<room>();
        room_.random_door();
        Vector2 r_XY = room_.get_XY();
        bool destroy=false;
        List<door> open_door = new List<door>();
        int room_x = room_.get_XY_x();
        int room_y = room_.get_XY_y();
        for (int i = 0; i < room_.door.Length; i++)
        {
            if (room_.door[i].door_active && room_.door[i].link == null) {
                open_door.Add(room_.door[i]);
                
            }
        }
        
        //방의 문을 체크해서 문이면서 연결되지 않은 문을 리스트에 담는다
        for (int i = 0; i < open_door.Count; i++)
        {
            if (room.Count < room_create_number)
            {
                Vector3 pos1 = r.transform.position;
                int random_number = Random.Range(0, room_database.Length);
                GameObject a = Instantiate(room_database[random_number]);
                a.transform.SetParent(this.transform);
                room a_room = a.GetComponent<room>();
                
                make_map_element(a_room);
                door b;
                switch (open_door[i].direct)
                {
                    case 0:

                        a_room.set_XY(r_XY[0] - 3, r_XY[1]);
                        b = find_door(a.GetComponent<room>().door, 1);
                        a.gameObject.transform.position = new Vector3(pos1.x - r.GetComponent<room>().room_width, pos1.y, pos1.z);
                        for (int p = 0; p < room_pos.Count; p++)
                        {
                            if (a_room.get_XY() == room[p].GetComponent<room>().get_XY())
                            {
                                Debug.Log("방이 겹쳐서 파괴됨" + (r_XY[0] -3) + r_XY[1]);
                                if (a_room.room_element == 3)
                                {
                                    event_room_count--;
                                }
                                Destroy(a);
                                destroy = true;
                                break;
                            }
                        }
                        if (!destroy)
                        {
                            room_pos.Add(new Vector2(r_XY[0] - 3, r_XY[1]));//생성 시키는 방의 좌표 가져오기
                            room.Add(a);
                            if (x_Min > r_XY[0]-3)
                            {
                                x_Min -= 3;
                                x_weight += 3;
                                int new_x = x + 3;
                                mini_map = new int[new_x, y];

                                for (int x_ = 0; x_ < x; x_++)
                                {
                                    for (int y_ = 0; y_ < y; y_++)
                                    {
                                        mini_map[x_, y_] = mini_map[x_ + 3, y_];

                                    }
                                }
                                mini_map[(int)r_XY[0] - 1 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] - 2 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] - 3 + x_weight, (int)r_XY[1] + y_weight]=2;
                                x = new_x;
                            }
                            else
                            {
                                mini_map[(int)r_XY[0] - 1 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] - 2 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] - 3 + x_weight, (int)r_XY[1] + y_weight] = 2;
                            }
                        }
                        b.link = open_door[i].gameObject;
                        open_door[i].link = b.gameObject;
                        break;

                    case 1:
                        a.GetComponent<room>().set_XY(r_XY[0] + 3, r_XY[1]);
                        b = find_door(a.GetComponent<room>().door, 0);
                        a.gameObject.transform.position = new Vector3(pos1.x + r.GetComponent<room>().room_width, pos1.y, pos1.z);
                        for (int p = 0; p < room_pos.Count; p++)
                        {
                            if (a.GetComponent<room>().get_XY() == room[p].GetComponent<room>().get_XY())
                            {
                                
                                if (a_room.room_element == 3)
                                {
                                    event_room_count--;
                                }
                                Destroy(a);

                                destroy = true;
                                break;
                            }
                        }
                        if (!destroy)
                        {
                            room_pos.Add(new Vector2(r_XY[0] + 3, r_XY[1]));
                            room.Add(a);
                            if (x_Max < r_XY[0]+3)
                            {
                                x_Max += 3;
                                int new_x = x + 3;
                                mini_map = new int[new_x, y];

                                
                                mini_map[(int)r_XY[0] + 1 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] + 2 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] + 3 + x_weight, (int)r_XY[1] + y_weight] = 2;
                                x = new_x;
                            }
                            else
                            {
                                mini_map[(int)r_XY[0] + 1 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] + 2 + x_weight, (int)r_XY[1] + y_weight] = 1;
                                mini_map[(int)r_XY[0] + 3 + x_weight, (int)r_XY[1] + y_weight] = 2;
                            }
                        }

                        b.link = open_door[i].gameObject;
                        open_door[i].link = b.gameObject;
                        break;

                    case 2:
                        a.GetComponent<room>().set_XY(r_XY[0], r_XY[1] + 3);
                        b = find_door(a.GetComponent<room>().door, 3);
                        a.gameObject.transform.position = new Vector3(pos1.x, pos1.y + r.GetComponent<room>().room_height, pos1.z);
                        for (int p = 0; p < room_pos.Count; p++)
                        {
                            if (a.GetComponent<room>().get_XY() == room[p].GetComponent<room>().get_XY())
                            {
                                Debug.Log("방이 겹쳐서 파괴됨" + r_XY[0] + (r_XY[1] + 3));
                                if (a_room.room_element == 3)
                                {
                                    event_room_count--;
                                }
                                Destroy(a);
                                destroy = true;
                                break;
                            }
                        }
                        if (!destroy)
                        {
                            room_pos.Add(new Vector2(r_XY[0], r_XY[1] + 3));
                            room.Add(a);
                            if (y_Max < r_XY[1]+3)
                            {
                                y_Max += 3;
                                int new_y = y + 3;
                                mini_map = new int[x, new_y];


                                mini_map[(int)r_XY[0]  + x_weight, (int)r_XY[1]+1 + y_weight] = 1;
                                mini_map[(int)r_XY[0] + x_weight, (int)r_XY[1]+2 + y_weight] = 1;
                                mini_map[(int)r_XY[0]  + x_weight, (int)r_XY[1]+3 + y_weight] = 2;
                                y = new_y;
                            }
                            else
                            {
                                mini_map[(int)r_XY[0] + x_weight, (int)r_XY[1]+1 + y_weight] = 1;
                                mini_map[(int)r_XY[0]  + x_weight, (int)r_XY[1]+2 + y_weight] = 1;
                                mini_map[(int)r_XY[0]  + x_weight, (int)r_XY[1]+3 + y_weight] = 2;
                            }
                        }
                        b.link = open_door[i].gameObject;
                        open_door[i].link = b.gameObject;
                        break;

                    case 3:
                        a.GetComponent<room>().set_XY(r_XY[0], r_XY[1] - 3);
                        b = find_door(a.GetComponent<room>().door, 2);
                        a.gameObject.transform.position = new Vector3(pos1.x, pos1.y - r.GetComponent<room>().room_height, pos1.z);
                        for (int p = 0; p < room_pos.Count; p++)
                        {
                            if (a.GetComponent<room>().get_XY() == room[p].GetComponent<room>().get_XY())
                            {
                               
                                Debug.Log("방이 겹쳐서 파괴됨"+ r_XY[0]+ (r_XY[1] - 3));
                                if (a_room.room_element == 3)
                                {
                                    event_room_count--;
                                }
                                Destroy(a);
                                destroy = true;
                                break;
                            }
                        }
                        if (!destroy)
                        {
                            room_pos.Add(new Vector2(r_XY[0], r_XY[1] - 3));
                            room.Add(a);
                            if (y_Min > r_XY[1] - 3)
                            {
                                y_Min-= 3;
                                y_weight += 3;
                                int new_y = y + 3;
                                mini_map = new int[x, new_y];
                                for (int x_ = 0; x_ < x; x_++)
                                {
                                    for (int y_ = 0; y_ < y; y_++)
                                    {
                                        mini_map[x_, y_] = mini_map[x_, y_+3];

                                    }
                                }

                                mini_map[(int)r_XY[0] + x_weight, (int)r_XY[1] - 1 + y_weight] = 1;
                                mini_map[(int)r_XY[0] + x_weight, (int)r_XY[1] - 2 + y_weight] = 1;
                                mini_map[(int)r_XY[0] + x_weight, (int)r_XY[1] - 3 + y_weight] = 2;
                                y = new_y;
                            }
                            else
                            {
                                mini_map[(int)r_XY[0]  + x_weight, (int)r_XY[1] - 1 + y_weight] = 1;
                                mini_map[(int)r_XY[0] + x_weight, (int)r_XY[1] - 2 + y_weight] = 1;
                                mini_map[(int)r_XY[0]  + x_weight, (int)r_XY[1] - 3 + y_weight] = 2;
                            }
                        }
                        b.link = open_door[i].gameObject;
                        open_door[i].link = b.gameObject;
                        break;
                    default:
                        Debug.Log("fatal error!");
                        break;
                }
            }
        }
        //리스트의 길이 만큼 방을 만들고 문을 연결시킨다

        //생성된 방은 또 문을 체크할수 있게 제귀로 빠진다
        //start가 awake보다 느리다는 점을 이용해서 생성이 다된 후에 다시 방을 만드는 방식으로 재귀한다 가능?
        //start awake보다 재귀 방식을 list에 담긴 방을 순차적으로 불려오는 방식으로 바꾸면 가능하다! 이건 가능
        return null;

    }
    public GameObject create_room_V2()//방 하나 생성 후 이어붙이는 방식
    {
        //room[room_count] = make_room_V2();//방 만들고
        GameObject b = make_room_V2();//방 만들고
        a++;
        if (room.Count< room_create_number)
        {
            return create_room_V2(room);//아니면 아어붙이기
            
        }
        else
        {
            return null;//반환값이 필요하면 채우기

        }


    }
    public GameObject create_room_V2(List<GameObject> room)//지금은 사용 금지
    {
        if (room.Count < room_create_number)//통로가 뚫려있으면
            {
            if (r_count >= room.Count)
            {
                
                r_count = room.Count-1;
                room[r_count].GetComponent<room>().random_door();
            }
            GameObject r2 = make_room_V2(room[r_count]);//방 생성후 이어붙이기
            r_count++;

            //room[room_count] = make_room_V2(r);
            
                if (room.Count> room_create_number)//다차면 스톱
                    return null;
                else
                    create_room_V2(room);//그리고 그방의 통로를 검사함
            }
        return null;
       
    }
    void close_no_link_door(List<GameObject> d)
    {
        
        for(int i = 0; i < d.Count; i++)
        {
            d[i].GetComponent<room>().make_wall();
        }
    }
    void reset_all()
    {
        while (room.Count != 0)
        {
            room.RemoveAt(room.Count - 1);
        }
        while (room_pos.Count != 0)
        {
            room_pos.RemoveAt(room_pos.Count - 1);
        }
    }

   /* public void make_wall_to_minimap(int x,int y,int dir)//x,y=방의 좌표 dir=바꿀 문의 방향
    {
        switch (dir)
        {
            case 0:
                mini_map[x+x_weight-2, y+y_weight] = 0;
                mini_map[x + x_weight - 1, y + y_weight] = 0;
                break;
            case 1:
                mini_map[x + x_weight , y + y_weight+2] = 0;
                mini_map[x + x_weight , y + y_weight+1] = 0;
                break;
            case 2:
                mini_map[x + x_weight + 2, y + y_weight] = 0;
                mini_map[x + x_weight + 1, y + y_weight] = 0;
                break;
            case 0:
                mini_map[x + x_weight , y + y_weight-2] = 0;
                mini_map[x + x_weight , y + y_weight-1] = 0;
                break;
        }
    }*/

}
/*방식을 또 바꾼다
 * 이전 방식->방 하나 만들고 랜덤 돌려서 정하고 그러다 보면 충돌체크로 알아서 연결
 * 문제점 만들고 랜덤이라서 서로 연결이 안되는 경우가 생김,일자로 이어지는 느낌이 너무 강함
 *
 * 새 방식-> 방 만들고 그 방의 모든 문에 방을 만들고 그 방이 또 새로운 방을 만들고....
 *방 연결은 충돌체크가 아니라 생성할 때 연결
 * 문은 연결되어있다면 랜덤으로 가지 않고 무조건 열린 문
 * 
 */