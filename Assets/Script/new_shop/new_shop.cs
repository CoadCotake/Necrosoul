using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_shop : MonoBehaviour
{
    public Transform[] item_create_pos=new Transform[5];
    
    public GameObject refresh_obj;
    public int re_price;
   public int curren_re_price;
    public int discount_max_num;
    int discount_num;
    int re_price_add;
    public Transform item_list;
    public GameObject shopitem;
    public int N_num;
    public int C_num;
    public int S_num;
    public List<GameObject> consumable_intansi=new List<GameObject>();
    public List<GameObject> item_intansi = new List<GameObject>();
    public List<GameObject> sp_item_intansi = new List<GameObject>();
    public List<GameObject> total_item = new List<GameObject>();
    GameObject choose_obj;
    private GameObject buy_particle;

    void create_item()
    {
        for(int i = 0; i < item_create_pos.Length; i++)//N_num+C_num+s_num=item_create_pos.Length
        {

            if (i < C_num)
            {
                choose_obj = item_pulling(consumable_intansi);
               
            }else if (i < C_num + N_num)
            {
                choose_obj = item_pulling(item_intansi);
            }
            else if (i < C_num + N_num+S_num)
            {
                choose_obj = item_pulling(sp_item_intansi);
            }
            else
            {

            }
            choose_obj.transform.position = item_create_pos[i].position;
            if (discount_num != discount_max_num)
            {
                if (choose_obj.GetComponent<shop_item>().discount_chk())
                {
                    discount_num++;
                }
            }

            choose_obj.GetComponent<shop_item>().set_item();
            choose_obj.SetActive(true);


        }
    }
    GameObject item_pulling(List<GameObject> l)
    {
        for(int i = 0; i < l.Count; i++)
        {
            if (!l[i].activeSelf)
            {
                return l[i];
            }
        }
        return null;
    }
    void Start()
    {
        curren_re_price = re_price;
        re_price_add = Mathf.RoundToInt(re_price);
        for(int i = 0; i < 3; i++)
        {
            var a = Instantiate(shopitem, item_list);
            var b = a.GetComponent<shop_item>();
            b.item_element = shop_item.element.consumable;
            consumable_intansi.Add(a);
            total_item.Add(a);
            a.SetActive(false);
           
        }
        for (int i = 0; i < 3; i++)
        {
            var a = Instantiate(shopitem, item_list);
            var b = a.GetComponent<shop_item>();
            b.item_element = shop_item.element.normal;
            item_intansi.Add(a);
            total_item.Add(a);
            a.SetActive(false);
        }
        for (int i = 0; i < 2; i++)
        {
            var a = Instantiate(shopitem, item_list);
            var b = a.GetComponent<shop_item>();
            b.item_element = shop_item.element.special;
            sp_item_intansi.Add(a);
            total_item.Add(a);
            a.SetActive(false);
        }
        create_item();
        GameObject m_pos = Instantiate(Gamemanager.GM.shop_minimap_pos, this.transform);
        m_pos.transform.position = this.transform.position;
    }
    public void refresh()
    {
        if (Player_status.p_status.Money >= curren_re_price)
        {
            discount_num = 0;
            //if(Inventory.)
            for (int i = 0; i < total_item.Count; i++)
            {
                if (total_item[i].activeSelf)
                {
                    total_item[i].GetComponent<shop_item>().refresh_item();
                    if (discount_num != discount_max_num)
                    {
                        if (total_item[i].GetComponent<shop_item>().discount_chk())
                        {
                            discount_num++;
                        }
                    }
                }
            }
            Gamemanager.GM.game_ev.when_lose_money(curren_re_price);
            if (buy_particle == null)
            {
                GameObject a = Instantiate(Gamemanager.GM.money_used_particle.gameObject, this.transform);
                buy_particle = a;
                a.SetActive(false);
                ParticleSystem.MainModule d = a.GetComponent<ParticleSystem.MainModule>();
                if (curren_re_price > 30)
                {
                    d.maxParticles = 10;
                }
                else if (curren_re_price> 20)
                {
                    d.maxParticles = 6;
                }
                else
                {
                    d.maxParticles = 3;
                }
                a.transform.position = this.transform.position;
                a.SetActive(true);
            }
            else
            {
                if (buy_particle.activeSelf)
                {
                    buy_particle.SetActive(false);
                }
                ParticleSystem.MainModule d = buy_particle.GetComponent<ParticleSystem.MainModule>();
                if (curren_re_price > 30)
                {
                    d.maxParticles = 10;
                }
                else if (curren_re_price > 20)
                {
                    d.maxParticles = 6;
                }
                else
                {
                    d.maxParticles = 3;
                }
                buy_particle.SetActive(true);
            }
            curren_re_price = re_price_add;
            re_price_add = Mathf.RoundToInt(curren_re_price);

        }
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
