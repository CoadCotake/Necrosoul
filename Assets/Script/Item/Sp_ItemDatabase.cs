using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_ItemDatabase : MonoBehaviour
{
    public static Sp_ItemDatabase Sp_itemDatabase;


    [Header("Sp_Item")]
    public List<Sp_Item> Sp_Nomal_Item;
    public List<Sp_Item> Sp_Rare_Item;
    public List<Sp_Item> Sp_Epic_Item;
    public List<Sp_Item> Sp_SoldOut_Item;
    public List<Sp_Item> Sp_item_all;

    [Header("ItemCode")]
    [SerializeField] public int Sp_Nomal = 0;
    [SerializeField] public int Sp_Rare = 100;
    [SerializeField] public int Sp_Epic = 200;
    [SerializeField] public int Sp_End = 300;


    Sp_Item it = new Sp_Item();

    int RandomNum;      //·£´ý¼ö
    public int Sp_Item_total;   //ÃÑ °³¼ö

    [Header("°¢ µî±ÞÀÇ È®·ü")]
    [SerializeField]
    float nomal=50f;
    [SerializeField]
    float rare=30f;
    [SerializeField]
    float epic=20f;
    float percent;


    private void Awake()
    {
        Sp_itemDatabase = this;
        CreateSpItem();
    }


    public void CreateSpItem()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Sp_item");

        for (int i = 0; i < Data.Count; i++)
        {
            it.Foreignkey = int.Parse(Data[i]["Foreignkey"].ToString());
            it.Sprite = Resources.Load(Data[i]["Image"].ToString(), typeof(Sprite)) as Sprite;  //ÀÌ¹ÌÁöÁÖ±â
            it.Name = Data[i]["Name"].ToString();
            it.Description = Data[i]["Description"].ToString();
            it.Rarity = Data[i]["Rarity"].ToString();
            Sp_Item_total++;
            
            Classify(it.CreateSp_Item());
        }

        for (int i = 0; i < Data.Count; i++)
        {
            
        }
    }


    public void Classify(Sp_Item i)    //¾ÆÀÌÅÛ ºÐ·ùÀÛ¾÷
    {
        Sp_item_all.Add(i);
        if (i.Rarity=="nomal")
        {
            Sp_Nomal_Item.Add(i);
        }
        else if (i.Rarity == "rare")
        {
            Sp_Rare_Item.Add(i);
        }
        else if (i.Rarity == "epic")
        {
            Sp_Epic_Item.Add(i);
        }
        else                        //Æ¯¼ºÀÌ ÀüºÎ ¶³¾îÁ³À» °æ¿ì
        {
            Debug.Log("»ÌÀ»°Ô ¾ø´Ù");
        }
        
    }

    public Sp_Item Random_Sp_Item(bool infinity=false)
    {
        if (!infinity) 
        {
            percent = Random.Range(0f, 100f);
        }
        else
        {
            percent = 0;
        }

        Sp_Item item = new Sp_Item();

        
        if (percent < nomal  && Sp_itemDatabase.Sp_Nomal_Item.Count!=0)              //³ë¸»»Ì
        {
            RandomNum = Random.Range(0, Sp_itemDatabase.Sp_Nomal_Item.Count);
            item = Sp_itemDatabase.Sp_Nomal_Item[RandomNum];
            Sp_itemDatabase.Sp_Nomal_Item.RemoveAt(RandomNum);
            Debug.Log(percent + "È®·ü·Î " + item.Name + "À» »ÌÀ¸¼Ì½À´Ï´Ù."+"³ë¸»");
            return item;
        }
        else if ((percent < nomal+rare && Sp_itemDatabase.Sp_Rare_Item.Count != 0) || (Sp_itemDatabase.Sp_Nomal_Item.Count == 0&& Sp_itemDatabase.Sp_Rare_Item.Count != 0))      //·¹¾î»Ì
        {
            RandomNum = Random.Range(0, Sp_itemDatabase.Sp_Rare_Item.Count);
            item = Sp_itemDatabase.Sp_Rare_Item[RandomNum];
            Sp_itemDatabase.Sp_Rare_Item.RemoveAt(RandomNum);
            Debug.Log(percent + "È®·ü·Î " + item.Name + "À» »ÌÀ¸¼Ì½À´Ï´Ù."+"·¹¾î");
            return item;
        }
        else if ((percent < 100 && Sp_itemDatabase.Sp_Epic_Item.Count != 0) || (Sp_itemDatabase.Sp_Rare_Item.Count == 0&& Sp_itemDatabase.Sp_Epic_Item.Count != 0))      //¿¡ÇÈ»Ì
        {
            RandomNum = Random.Range(0, Sp_itemDatabase.Sp_Epic_Item.Count);
            item = Sp_itemDatabase.Sp_Epic_Item[RandomNum];
            Sp_itemDatabase.Sp_Epic_Item.RemoveAt(RandomNum);
            Debug.Log(percent + "È®·ü·Î " + item.Name + "À» »ÌÀ¸¼Ì½À´Ï´Ù."+"¿¡ÇÈ");
            return item;
        }
        else if(!infinity)   //¹Ýº¹Àü
        {
            return Random_Sp_Item(true);
        }
        else
        {
            item = Sp_itemDatabase.Sp_SoldOut_Item[0];
            return item;
        }
    }
}


