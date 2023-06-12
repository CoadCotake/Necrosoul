using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    //아이템 구조

    public int Foreignkey;    //아이템코드
    public int ItemType;     //아이템 타입   1:아이템 2:sp아이템 3:소모품
    public Sprite Sprite;   //이미지
    public int Money;       //가격
    public string Rarity;      //레어도
    public string Name;     //이름
    public string Description;   //설명
    public int num;
    public bool Item_Useing;
    public int SlotNumber;  //들어가있는 슬롯 번호(아이템, 파츠 포함)
    public GameObject parent; //아이템 슬롯부모
    private bool effecting;
    // public int DataNumber;  //들어가있는 리스트 자리 번호 (아이템, 파츠 포함)
    public void ChangeItem(Item item)
    {
        this.Foreignkey = item.Foreignkey;
        this.ItemType = item.ItemType;
        this.Rarity = item.Rarity;
        this.Sprite = item.Sprite;
        this.SlotNumber = item.SlotNumber;
        this.Money = item.Money;
        this.Name = item.Name;
        this.Description = item.Description;
    }
    public void set_effecting_on()
    {
        effecting = true;
    }
    public void set_effecting_off()
    {
        effecting = false;
    }
    public bool get_effecting()
    {
        return effecting;
    }

    public Item CreateItem()
    {
        Item a = new Item();
        a.Foreignkey = this.Foreignkey;
        a.ItemType = this.ItemType;
        a.Rarity = this.Rarity;
        a.Sprite = this.Sprite;
        a.SlotNumber = this.SlotNumber;
        a.Money = this.Money;
        a.Name = this.Name;
        a.Description = this.Description;
        a.parent = null;
        return a;
    }

    ////consumable
    public void consumable_effect()
    {
        if (ItemType == 3)
        {
            switch (Foreignkey)
            {
                case 1:
                    Player_status.p_status.set_hp(Mathf.RoundToInt(Player_status.p_status.get_max_hp() * 0.1f));
                    break;
                case 2:
                    Player_status.p_status.set_hp(Mathf.RoundToInt(Player_status.p_status.get_max_hp() * 0.3f));
                    break;
                case 3:
                    Player_status.p_status.set_hp(Mathf.RoundToInt(Player_status.p_status.get_max_hp() * 0.7f));
                    break;
                case 4:
                    Player_status.p_status.set_hp(Mathf.RoundToInt(Player_status.p_status.get_max_hp() * 1f));
                    break;
            }
        }
    }
    ///special
    public void special_effect()
    {
        if (ItemType == 2)
        {
            switch (Foreignkey)
            {
                case 0:
                    Gamemanager.GM.get_sp_item();
                    break;
            }
        }
    }
}
