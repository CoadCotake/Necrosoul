using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_contents : MonoBehaviour
{
    public List<enemy_cycle> cycle = new List<enemy_cycle>();
    public List<enemy_cycle> index_cycle = new List<enemy_cycle>();
    
    public bool index_chk;
    public int index;
    int rand;
    public bool start_chk;
    public bool cycle_chk;
    public enemy_cycle choose_cycle;
    public bool room_cleared;
    private void Awake()
    {
       
    }
    void Start()
    {
        if (Gamemanager.GM.stage == 2)
        {
            index = 2;
        }
        else
        {
            index = 1;
        }

        set_cycle_index(index);
        for (int i = 0; i < cycle.Count; i++)
        {
            cycle[i].gameObject.SetActive(false);
        }
       

    }
    public void set_cycle_index(int i)
    {
        
        int n = i;
        Debug.Log(n);
        for (int a = 0; a < cycle.Count; a++)
        {
            Debug.Log("Level:"+cycle[a].Level);
            if (cycle[a].Level == n)
            {
                index_cycle.Add(cycle[a]);
            }
        }
        index_chk = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Gamemanager.GM.fade_in_complete)
        {
            start_chk = true;
        }
        if (index_chk)
        {
           
                rand = Random.Range(0, index_cycle.Count);

                choose_cycle = index_cycle[rand];
 
                cycle_chk = true;
                index_chk = false;
            
            
        }
        
    }
    
    public void acitve_enemy()
    {
        if (choose_cycle.choose_group == null && choose_cycle.enemy_group.Count != 0)
        {
            choose_cycle.gameObject.SetActive(true);
            choose_cycle.acitve_enemy();
        }
        



    }
   
}
