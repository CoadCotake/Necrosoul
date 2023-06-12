using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shadow_anim_event : MonoBehaviour
{
    public GameObject melee_1;
    public GameObject melee_1_instani;
    public GameObject melee_2;
    public GameObject melee_2_instani;
    public GameObject melee_3;
    public GameObject melee_3_instani;
    public GameObject air_melee_;
    public GameObject air_melee_instani;
    public float melee_1_reaction;
    public float melee_2_reaction;
    public float melee_3_reaction;
    public Player_shadow p_sh;
    
    public void Destroy_self()
    {
        Destroy(this.transform.parent.parent.gameObject);
    }
    void Melee_1_on()
    {
        
    }
    
    public void Melee_1_off()
    {
        

    }
    void air_melee_on()
    {
       
    }
    public void air_Melee_off()
    {

       
        
    }
    void Melee_2_on()
    {
       
    }
    public void Melee_2_off()
    {
      
       
    }
    public void Start()
    {
        p_sh = this.transform.parent.GetComponent<Player_shadow>();
    }
        void Melee_3_on()
    {
       
        // melee_3.SetActive(true);
    }
    public void Melee_3_off()
    {
        
        
    }
}
