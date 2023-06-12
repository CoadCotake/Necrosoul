using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven_Content_Sp : MonoBehaviour
{
    [Header("인벤 요소")]
    public Text Title;
    public Image I_Image;
    public GameObject Use;
    public int item_forginkey;

    [Header("툴팁 요소")]
    public Text T_Title;
    public Text T_Content;
    public Image T_Image;


    public Sp_Item Item;

    public Toggle My_toggle;
    // Start is called before the first frame update
    void Start()
    {
        My_toggle = this.gameObject.GetComponent<Toggle>();
    }

    private void Update()
    {
        if(My_toggle.isOn)
        {
            ButtonOn();
        }
    }

    public void ChangeAch(Sp_Item item)
    {
        Title.text = item.Name;
        I_Image.sprite = item.Sprite;
        this.Item = item;
        //if (ItemDatabase.itemDatabase.item_list[item_forginkey].Item_Useing)
        //{
        //    Use.SetActive(true);
        //}
        //else
        //{
        //    Use.SetActive(false);
        //}
    } 
    public void ButtonOn()
    {
        T_Title.text = Item.Name;
        T_Content.text = Item.Description;
        T_Image.sprite = Item.Sprite;
    }

    public void TakeItem()
    {
        if (!ItemDatabase.itemDatabase.item_list[item_forginkey].Item_Useing)
        {
            Debug.Log("장착중");
            //NewInven.newinven.UseItemApply(item_forginkey);
            Use.SetActive(true);
        }
        else
        {
            Debug.Log("이미 장착되어 있음");
            //NewInven.newinven.UseItemRemove(item_forginkey);
            //Destroy(NewInven.newinven.UseItem[NewInven.newinven.UseItem.FindIndex(Item)]);
            Use.SetActive(false);
        }
    }
}
