using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_02 : MonoBehaviour
{
    Event_system e;
    Item[] soul=new Item[3];
   Text[] btn_T = new Text[3];

    void Start()
    {
        e = this.GetComponent<Event_system>();

        for (int i = 1; i < 4; i++)
        {
            soul[i-1] = ItemDatabase.itemDatabase.get_item_by_rarity(ItemDatabase.itemDatabase.item_list).CreateItem();
            btn_T[i-1] = e.a[i - 1].transform.GetChild(1).GetComponent<Text>();
            btn_T[i - 1].text = i.ToString() + ". " + soul[i - 1].Name + "를 가져간다.";
        }
        
    }
    public void get_item()
    {
        
        Gamemanager.GM.get_item(soul[e.select]);
    }
    
   
}
