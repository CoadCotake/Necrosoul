using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spEF_11_illusion : MonoBehaviour
{
    public int HP;
    public float untouchable_time;
    public float untouchable_timer;
    Animator ani;
    public float life_timer;
    public float life_time;
    List<Unit> Enemies;
    GameObject[] E;
    private void Awake()
    {
        ani = this.GetComponent<Animator>();
        HP = 10;
        untouchable_time = Player_status.p_status.get_untouchable_time();
    }
   public void charm_enemy()
    {
        E = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < E.Length; i++)
        {
            if (E[i].GetComponent<Unit>() != null)
            {
                Enemies.Add(E[i].GetComponent<Unit>());
            }
        }

    }
    private void OnEnable()
    {
        HP = 10;
        life_time = 10f;
        life_timer = life_time;
        
    }
    void Update()
    {
        if (HP < 0)
        {
            this.gameObject.SetActive(false);
        }
        if (untouchable_timer > 0)
        {
            untouchable_timer -= Time.deltaTime;
            ani.SetBool("hitted", true);
        }
        else
        {
            ani.SetBool("hitted", true);
        }
        if (life_timer >0)
        {
            life_timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (untouchable_timer <= 0)
            {
                HP--;
                untouchable_timer = untouchable_time;
                
            }
        }else if (collision.CompareTag("Enemy_bullet"))
        {
            if (untouchable_timer <= 0)
            {
                HP--;
                untouchable_timer = untouchable_time;
                Destroy(collision.gameObject);
            }
            
        }
    }

}
