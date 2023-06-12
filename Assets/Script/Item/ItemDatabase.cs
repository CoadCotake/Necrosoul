using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase itemDatabase; 

    [Header("Item")]
    public List<Item> Nomal_Item;
    public List<Item> Rare_Item;
    public List<Item> Epic_Item;

    public List<Item> item_list;//혼
    public List<Item> consumable_list;//소모품
    public List<Item> sp_list;//특수

    public float normal_percent;
    public float rare_percent;
    //public float epic_percent;

    [Header("획득한 아이템 리스트")]
    public List<Item> GetItemList = new List<Item>();
    public bool Item_Have_Check;
    int GetItemCount;

    List<Item> rarity_list=new List<Item>();
    Item it = new Item();
    int num;
    private void Update()
    {
        for (int i = 0; i < item_list.Count; i++)
        {
           
                if (item_list[i].num != 0)
                {
                Debug.Log(i + "아이템 적용중");
                    ItemEffect0.item0to10.effect(item_list[i]);
                }
            
        }

    }
    public void Make_Get_item(Item i)
    {
        Debug.Log("아니 도대체 뭐가 문제야?" + i.Name);
        Item_Have_Check = false;
        num = 0;
        GetItemCount = 0;

        if (GetItemList != null)
        {
            for (int n = 0; n < GetItemList.Count; n++)     //인벤토리에 있는지 체크
            {
                if (GetItemList[n].Foreignkey == i.Foreignkey)
                {
                    num = n;
                    Item_Have_Check = true;
                }
            }
        }

        if(Item_Have_Check) //아이템창에 있다면
        {
            GetItemList[num].num++;
            ItemEffect0.item0to10.uneffect(GetItemList[num]);
            ItemEffect0.item0to10.effect(GetItemList[num]);
        }
        else //없다면
        {
            GetItemList.Add(i);
            GetItemCount = GetItemList.Count-1;
            GetItemList[GetItemCount].num++;
            ItemEffect0.item0to10.uneffect(GetItemList[GetItemCount]);
            ItemEffect0.item0to10.effect(GetItemList[GetItemCount]);
        }
    }

    public void rarity_list_initialize()
    {
        for(int i = 0; i < rarity_list.Count; i++)
        {
            rarity_list.RemoveAt(0);
        }
    }
    public Item get_item_by_rarity(List<Item> i_list)
    {
        if (rarity_list.Count != 0)
        {
            rarity_list_initialize();
        }
        Item i=new Item();
        int rand = Random.Range(0, 100) + 1;//1~100까지
        if (rand<normal_percent)
        {
            for(int n=0;n < i_list.Count; n++)
            {
                if (i_list[n].Rarity == "common")
                {
                    rarity_list.Add(i_list[n]);

                }
            }
        }else if (rand < normal_percent+rare_percent)
        {
            for (int n = 0; n < i_list.Count; n++)
            {
                if (i_list[n].Rarity == "uncommon")
                {
                    rarity_list.Add(i_list[n]);

                }
            }
        }
        else
        {
            for (int n = 0; n < i_list.Count; n++)
            {
                if (i_list[n].Rarity == "rare")
                {
                    rarity_list.Add(i_list[n]);

                }
            }
        }

        if (rarity_list.Count != 0)
        {
            rand = Random.Range(0, rarity_list.Count);
            
            return rarity_list[rand].CreateItem();
        }
        else
        {
            rand = Random.Range(0, i_list.Count);
            return i_list[rand].CreateItem();
        }

        
    }
    public Item get_item_by_rarity_upper_rare(List<Item> i_list)
    {
        if (rarity_list.Count != 0)
        {
            rarity_list_initialize();
        }
        Item i = new Item();
        int rand = Random.Range(0, 100) + 1;//1~100까지
        for (int n = 0; n < i_list.Count; n++)
        {
            if (i_list[n].Rarity == "uncommon" || i_list[n].Rarity == "rare")
            {
                rarity_list.Add(i_list[n]);

            }
        }
            if (rarity_list.Count != 0)
            {
                rand = Random.Range(0, rarity_list.Count);
                return rarity_list[rand].CreateItem();
            }
            else
            {
                rand = Random.Range(0, i_list.Count);
                return i_list[rand].CreateItem();
            }
        }
    
        public Item get_item_by_rarity(List<Item> i_list,string rarity)
    {
        if (rarity_list.Count != 0)
        {
            rarity_list_initialize();
        }
        Item i = new Item();
        int rand = Random.Range(0, 100) + 1;//1~100까지
        if (rarity== "common")
        {
            for (int n = 0; n < i_list.Count; n++)
            {
                if (i_list[n].Rarity == "common")
                {
                    rarity_list.Add(i_list[n]);

                }
            }
        }
        else if (rarity == "uncommon")
        {
            for (int n = 0; n < i_list.Count; n++)
            {
                if (i_list[n].Rarity == "uncommon")
                {
                    rarity_list.Add(i_list[n]);

                }
            }
        }
        else
        {
            for (int n = 0; n < i_list.Count; n++)
            {
                if (i_list[n].Rarity == "rare")
                {
                    rarity_list.Add(i_list[n]);

                }
            }
        }
        if (rarity_list.Count != 0)
        {
            rand = Random.Range(0, rarity_list.Count);
            return rarity_list[rand].CreateItem();
        }
        else
        {
            rand = Random.Range(0, i_list.Count);
            return i_list[rand].CreateItem();
        }


    }
    private void Awake()
    {
        itemDatabase = this;
        CreateItem();
        
    }
    

    public void CreateItem()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("ItemTree");
        
        for (int i = 0; i < Data.Count; i++)
        {
            it.Foreignkey = int.Parse(Data[i]["Foreignkey"].ToString());
            it.Sprite = Resources.Load(Data[i]["Image"].ToString(), typeof(Sprite)) as Sprite;  //이미지주기
            it.Name = Data[i]["Name"].ToString();
            it.Description = Data[i]["Description"].ToString();
            it.Rarity = Data[i]["Rarity"].ToString();
            it.ItemType =int.Parse(Data[i]["ItemType"].ToString());
            it.Money = int.Parse(Data[i]["Money"].ToString());
            Classify(it);
        }
    }
    public void Classify(Item i)    //아이템 분류작업
    {
        if (i.ItemType == 1)
            item_list.Add(i.CreateItem());
        else if (i.ItemType == 2)
            sp_list.Add(i.CreateItem());
        else
            consumable_list.Add(i.CreateItem());


    }
   /* public void Classify(Item i)    //아이템 분류작업
    {
        if(i.Rarity=="일반")
        {
            Nomal_Item.Add(i.CreateItem());
        }
        else if(i.Rarity=="희귀")
        {
            Rare_Item.Add(i.CreateItem());
        }
        else
        {
            Epic_Item.Add(i.CreateItem());
        }
            
            
    }*/
}
