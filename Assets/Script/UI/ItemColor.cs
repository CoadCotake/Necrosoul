using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColor : MonoBehaviour
{
    public Item item;
    Color Cl;
    public Image aa;
    void Update()
    {
        item = this.gameObject.GetComponent<Slot>().item;
        if (item.num <= 0)
        {
            Cl.r = 100 / 255f;
            Cl.g = 100 / 255f;
            Cl.b = 100 / 255f;
            Cl.a = 1f;
            this.gameObject.GetComponent<Image>().color = Cl;
        }
        else
        {
            Cl.r = 255 / 255f;
            Cl.g = 255 / 255f;
            Cl.b = 255 / 255f;
            Cl.a = 1f;
            this.gameObject.GetComponent<Image>().color = Cl;
        }
    }

    
}
