using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sp_Item
{
    //기본변수
    public int Foreignkey;    //아이템코드
    public Sprite Sprite;   //이미지
    public string Rarity;      //레어도
    public string Name;     //이름
    public string Description;   //설명


    public void ChangeSp_Item(Sp_Item Sp_item)
    {
        this.Foreignkey = Sp_item.Foreignkey;
        this.Rarity = Sp_item.Rarity;
        this.Sprite = Sp_item.Sprite;
        this.Name = Sp_item.Name;
        this.Description = Sp_item.Description;
    }

    public Sp_Item CreateSp_Item()
    {
        Sp_Item a = new Sp_Item();
        a.Foreignkey = this.Foreignkey;
        a.Rarity = this.Rarity;
        a.Sprite = this.Sprite;
        a.Name = this.Name;
        a.Description = this.Description;
        return a;
    }
}
