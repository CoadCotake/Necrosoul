using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_attack : MonoBehaviour
{
    public ParticleSystem sword_effect;
    public bool set_effect_rotate;
   public float effect_rotation=-444;
    public int Damage;
    public int index = 0;
    public float melee_force;
    public bool Double_attack_on;
    public bool on_crit;
    public bool disable_hit;
    public List<Unit> E = new List<Unit>();
    
    private void Awake()
    {

        if (sword_effect == null)
        {
            GameObject a = Instantiate(Gamemanager.GM.p_sword_effect.gameObject);
            sword_effect = a.GetComponent<ParticleSystem>();
            a.SetActive(false);
            var b = a.GetComponent<p_sword_hitted_particle>();
            if (set_effect_rotate)
            {
                b.setting_rotation = effect_rotation;
            }
        }
    }
    private void OnEnable()
    {
       // this.transform.rotation = Quaternion.identity;
       
        switch (index)
        {
            case 0:
                Damage = Mathf.RoundToInt(Damage * 2f);
                break;
            default:

                break;

        }
    }
    private void FixedUpdate()
    {
        
    }
    private void OnDisable()
    {
        if (Double_attack_on)
        {
            double_attack_system();
        }
        else
        {
            int n = E.Count;
            for (int i = 0; i < n; i++)
            {
                E.RemoveAt(0);
            }

        }
    }
   
    void double_attack_system()
    {
        Double_attack_on = false;
        disable_hit = true;
        int n = E.Count;
        for (int i = 0; i <n; i++)
        {
            if (Player_status.p_status.critical())
            {
                E[0].character_lose_health(Mathf.RoundToInt((Damage * Player_status.p_status.get_critical_damage())), E[i].DNP,gameObject.transform);
            }
            else
            {
                E[0].character_lose_health(Damage, E[i].DNP, gameObject.transform);
            }
            E.RemoveAt(0);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Gamemanager.GM.game_ev.P_Attack_col_effect(collision);
        
    }
}
