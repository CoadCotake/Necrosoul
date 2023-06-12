using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_04 : MonoBehaviour
{
    public GameObject no_shopkeeper_shop;
    public GameObject Enemy_wave;
    bool enemy_chk;
 GameObject first_event;
    GameObject second_event;
    private void Start()
    {
        first_event = transform.GetChild(0).gameObject;
        second_event = transform.GetChild(1).gameObject;
        first_event.SetActive(true);
        second_event.SetActive(false);
    }
    private void Update()
    {
        if (enemy_chk)
        {
            enemy_wave_end();
        }
    }
    public void first_button_on()
    {
        no_shopkeeper_shop.SetActive(true);
        no_shopkeeper_shop.transform.SetParent(this.transform.parent.parent);
        event_obj_disable();
    }
    public void second_button_on()
    {
        first_event.SetActive(false);
        second_event.SetActive(true);
    }
    public void enemy_wave_on()
    {
        Enemy_wave.transform.position = Gamemanager.GM.Player_obj.transform.position;
        Enemy_wave.SetActive(true);
        enemy_chk = true;
    }
    public void enemy_wave_end()
    {
        var b = Enemy_wave.GetComponent<Enemy_group>();
       if (b.enemy.Count == 0)
        {
            Enemy_wave.SetActive(false);
            enemy_chk = false;
            no_shopkeeper_shop.SetActive(true);
            no_shopkeeper_shop.transform.SetParent(this.transform.parent.parent);
            no_shopkeeper_shop.GetComponent<shop_steal_event>().make_shop_price_zero();
            event_obj_disable();
        }
    }
    public void event_obj_disable()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
