using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewInven : MonoBehaviour
{
    static public NewInven newinven;
    public GameObject UseSlot;
    public GameObject UseSlotprefab;
    public int UseSlot_limit;

    public List<Item> UseItemList = new List<Item>();

    [Header("ÀåÂø Ç¥½Ã")]
    public Text UseText;

    // Start is called before the first frame update
    void Start()
    {
        newinven = this;
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject prefab;
    public void UseItemApply(Item i)
    {
        if (UseItemList.Count < UseSlot_limit)
        {
            prefab=Instantiate(UseSlotprefab, UseSlot.transform);
            prefab.GetComponent<UseItem_slot>().item = i;
            i.parent = prefab;
            prefab.GetComponent<UseItem_slot>().image.sprite = i.Sprite;

            i.Item_Useing = true;
            ItemEffect0.item0to10.uneffect(i);
            ItemEffect0.item0to10.effect(i);
            UseItemList.Add(i);
            TextUpdate();
        }
    }
    public void UseItemRemove(Item i)
    {
        i.Item_Useing = false;
        ItemEffect0.item0to10.uneffect(i);
        ItemEffect0.item0to10.effect(i);
        Destroy(i.parent);
        i.parent = null;
        UseItemList.Remove(i);
        TextUpdate();
    }

    public void TextUpdate()
    {
        UseText.text = "ÀåÂø°¡´É (" + UseItemList.Count.ToString()+"/"+ UseSlot_limit.ToString()+")";
    }
}
