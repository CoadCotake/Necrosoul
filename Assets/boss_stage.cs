using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_stage : MonoBehaviour
{
    public GameObject End_door;
    public bool room_cleared;
    public GameObject[] enemy;
    public GameObject contents;
    public GameObject camera_point;
    public Vector2 size;
    public bool sp_item_chk;
    bool active_check;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamemanager.GM.spawn_check)
        {
            contents.SetActive(true);
            active_check = true;
        }
        if (active_check)
        {
            
            if (room_cleared)
            {
                if (!sp_item_chk)
                {
                    if (Gamemanager.GM.get_sp_item())
                    {
                        sp_item_chk = true;
                    }
                }
                End_door.SetActive(true);
                Gamemanager.GM.boss_clear = true;
            }
            else
            {

            }
        }
    }
}
