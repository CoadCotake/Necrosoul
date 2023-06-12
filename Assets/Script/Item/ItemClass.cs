using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : MonoBehaviour
{
    int itemcode;
    string item_name;
    Sprite Image;
    int rarity;

    List<Dictionary<string, object>> Data = CSVReader.Read("ItemDB");
    private bool effecting;

    public int get_Itemcode()
    {
        return itemcode;
    }
    public int get_rarity()
    {
        return rarity;
    }
    public void set_Itemcode(int item)
    {
        itemcode = item;
    }
    public void set_rarity(int item)
    {
        rarity = item;
    }
    public ItemClass(int code,int rarity)
    {
        set_Itemcode(code);
        set_rarity(rarity);
        item_name = Data[code]["Name"].ToString();
        Image = Resources.Load(Data[code]["Image"].ToString(), typeof(Sprite)) as Sprite;
    }
    public void set_effecting_on()
    {
        effecting = true;
    }
    public bool get_effecting()
    {
        return effecting;
    }
}
