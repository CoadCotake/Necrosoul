using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_steal_event : MonoBehaviour
{
   public void make_shop_price_zero()
    {
        var b = GetComponent<new_shop>();
        for(int i = 0; i < b.total_item.Count; i++)
        {
            b.total_item[i].GetComponent<shop_item>().price = 0;
        }
    }
}
