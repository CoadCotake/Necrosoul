using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_map_system : MonoBehaviour
{
    public GameObject[] boss_room_prefab;
    public bool map_making_complete;
    // Start is called before the first frame update
    void Start()
    {
        boss_map_making();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void boss_map_making()
    {
        if (Gamemanager.GM.boss)
        {
           
                    Instantiate(boss_room_prefab[0],this.transform.position,Quaternion.identity);
                    map_making_complete = true;
                
        }
    }
}
