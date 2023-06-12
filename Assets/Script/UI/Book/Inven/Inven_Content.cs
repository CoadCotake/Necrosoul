using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven_Content : MonoBehaviour
{
    [Header("인벤 요소")]
    public Text Title;
    public Image I_Image;
    public GameObject Use;

    [Header("툴팁 요소")]
    public Text T_Title;
    public Text T_Content;
    public Image T_Image;

    [Header("직접 넣어야할 부분")]
    public GameObject Select;

    ItemDatabase I_DB;
    public Item Item;

    public Toggle My_toggle;
    // Start is called before the first frame update
    void Start()
    {
        I_DB = ItemDatabase.itemDatabase;
        My_toggle = this.gameObject.GetComponent<Toggle>();
    }

    private void Update()
    {
        if(My_toggle.isOn)
        {
            Select.SetActive(false);
            ButtonOn();
        }
        else
        {
            Select.SetActive(true);
        }
    }

    public void ChangeAch(Item item)
    {

        Title.text = item.Name;
        I_Image.sprite = item.Sprite;
        this.Item = item;

        if (Item.Item_Useing)
        {
            Use.SetActive(true);
        }
        else
        {
            Use.SetActive(false);
        }
    }
    public void ButtonOn()
    {
        T_Title.text = Item.Name + "x" + Item.num;
        T_Content.text = Item.Description;
        T_Image.sprite = Item.Sprite;

    }

    public void TakeItem()
    {
        if (!Item.Item_Useing)
        {
            Debug.Log("장착중");
            NewInven.newinven.UseItemApply(Item);
            Use.SetActive(true);
        }
        else
        {
            Debug.Log("이미 장착되어 있음");
            NewInven.newinven.UseItemRemove(Item);
            //Destroy(NewInven.newinven.UseItem[NewInven.newinven.UseItem.FindIndex(Item)]);
            Use.SetActive(false);
        }
    }
}
