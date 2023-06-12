using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    /// <summary>
    /// 1.그리드를 나눌때 ui크기에 맞춰서 유동적으로 2.그리드를 나눌때 나누는 크기를 일정하게
    /// </summary>
    public GameObject map_image;
    public GameObject mapUI;
    room_controller r_c;
    int m_x;
    int x_image_number;//만들 이미지 개수 용
    float x_image_size;
    int m_y;
    int y_image_number;//만들 이미지 개수 용
    float y_image_size;
    int[,] mini_map;
    List<GameObject> map = new List<GameObject>();

    public int map_ui_x;//맵UI의 크기
    public int map_ui_y;

    public GameObject map_image_close;
    public GameObject map_image_open;
    public GameObject map_image_element;
    private void Awake()
    {
        r_c = this.GetComponent<room_controller>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void get_map_data()
    {
        m_x = r_c.x;
        m_y = r_c.y;
        x_image_number = m_x / 3;
        y_image_number = m_y / 3;
        x_image_size = map_ui_x / x_image_number;
        y_image_size = map_ui_y / y_image_number;
        mini_map = r_c.mini_map;
    }
    void make_map()
    {
        var globalstartpos_x = map_ui_x / 2 - (x_image_size * (x_image_number / 2));
        var globalstartpos_y = map_ui_y / 2 - (y_image_size* (y_image_number / 2));

        /*for(int x = 0; x < x_image_number; x++)
        {
            for(int y = 0; y < y_image_number; y++)
            {




                Vector2 pos = new Vector2(globalstartpos_x + x * x_image_size, globalstartpos_y + y * y_image_size);
                GameObject map_image_data = Instantiate(map_image, pos, Quaternion.identity);
                map_image_data.transform.SetParent(mapUI.transform);
                map.Add(map_image_data);
                    }
        }*/
        //////////////////////////////////
        for(int x = 1; x < m_x; x += 3)
        {
            for(int y = 1; y < m_y; y += 3)
            {
                Vector2 pos = new Vector2(globalstartpos_x + x / 3 * x_image_size, globalstartpos_y + y / 3 * y_image_size);
                if (mini_map[x,y] == 2)
                {
                    
                    for(int i = 0; i < 4; i++)
                    {
                        GameObject map_image_data_element = Instantiate(map_image_element, pos, Quaternion.identity);
                       //만든이미지 다 자식으로 만들기
                        switch (i)
                        {
                            case 0:
                                if (mini_map[x - 1, y] == 0)
                                {
                                    
                                    GameObject map_image_data_left= Instantiate(map_image_close,pos+(Vector2.left*x_image_size/3) , Quaternion.identity);
                                }
                                GameObject map_image_data_left_Up = Instantiate(map_image_close, pos + (Vector2.left * (x_image_size / 3))+(Vector2.up*(y_image_size/3)), Quaternion.identity);
                                break;
                            case 1:
                                if (mini_map[x , y+1] == 0)
                                {

                                    GameObject map_image_data_up = Instantiate(map_image_close, pos + Vector2.up * (y_image_size / 3), Quaternion.identity);
                                }
                                GameObject map_image_data_up_right = Instantiate(map_image_close, pos + (Vector2.right * (x_image_size / 3)) + (Vector2.up * (y_image_size / 3)), Quaternion.identity);
                                break;
                            case 2:
                                if (mini_map[x+1, y ] == 0)
                                {

                                    GameObject map_image_data_right = Instantiate(map_image_close, pos + Vector2.right * (x_image_size / 3), Quaternion.identity);
                                }
                                GameObject map_image_data_right_down = Instantiate(map_image_close, pos + (Vector2.right * (x_image_size / 3)) + (Vector2.down * (y_image_size / 3)), Quaternion.identity);
                                break;
                            case 3:
                                if (mini_map[x + 1, y] == 0)
                                {

                                    GameObject map_image_data_down = Instantiate(map_image_close, pos + Vector2.down * (y_image_size / 3), Quaternion.identity);
                                }
                                GameObject map_image_data_Left_down = Instantiate(map_image_close, pos + (Vector2.left * (x_image_size / 3)) + (Vector2.down * (y_image_size / 3)), Quaternion.identity);
                                break;
                        }
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3( map_ui_x, map_ui_y,0));
    }
}
