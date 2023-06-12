using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_07_AI_anim : MonoBehaviour
{
    Unit unit;
    E_07_AI E_07;
    E_07_range E_range;
    public GameObject e_07_melee;
    GameObject melee_instansi;
    Animator ani;
    
    void attack_on()
    {
        melee_pulling();
        E_07.on_attack = true;
       
    }
    void attack_end()
    {
        E_07.on_attack = false;
        unit.can_attack = false;


    }
    void attack_start()
    {
        E_07.on_attack = true;
        E_07.attack_delay = E_07.attack_time;
       
    }
    void attack_off()
    {


        melee_instansi.SetActive(false);
        ani.SetBool("attack_delay", true);
        unit.can_attack = false;

    }
    void idle()
    {
        // E_07.on_attack = false;
        melee_instansi.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
       
        ani = this.GetComponent<Animator>();
        E_07 =transform.parent.GetComponent<E_07_AI>();
        
        unit = transform.parent.GetComponent<Unit>();
        E_range = transform.GetChild(1).GetComponent<E_07_range>();
        melee_instansi = Instantiate(e_07_melee, e_07_melee.transform.position, this.transform.parent.parent.GetChild(1).rotation);
        melee_instansi.transform.SetParent(this.transform.parent.parent.GetChild(1));
        melee_instansi.transform.localScale = this.transform.parent.parent.GetChild(1).localScale;
        melee_instansi.SetActive(false);
        melee_instansi.GetComponent<enemy_melee>().damage = unit.Attack_point;
    }
    GameObject melee_pulling()
    {
        if (!melee_instansi.activeSelf)
        {
            melee_instansi.SetActive(true);
            return melee_instansi;
        }
        else
        {
            return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
