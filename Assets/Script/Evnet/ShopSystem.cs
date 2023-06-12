using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    private List<Item> ShopData;
    
    public bool CheckData(Item item)
    {
        if (ShopData.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
