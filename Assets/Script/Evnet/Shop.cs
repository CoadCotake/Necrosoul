using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IPointerClickHandler
{
    public Item item = new Item();
    public bool ClickCheck;
    public GameObject Inven;

    public Text Money;
    public Image image;


    public Tooltip tooltip;
    public ItemDatabase ItemDatabase;

    // Start is called before the first frame update
    void Awake()
    {
        ClickCheck = true;
    }
    void Start()
    {
        ItemDatabase = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        tooltip = GameObject.Find("Canvas").gameObject.transform.Find("Tooltip").GetComponent<Tooltip>();
        ItemType();
        ItemCreate();
        Inven = GameObject.Find("InventorySystem");
        Money = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        image = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        //Money.gameObject.SetActive(true);
        //image.gameObject.SetActive(true);
        Money.text = item.Money.ToString();
        
    }


    
    public void ItemType()
    {
        int Ran = Random.Range(1, 100);      //1~10 가중치 정하기

        if (1 <= Ran && 60 >= Ran)
        {
            item.ItemType = 1;
        }
        else if (61 <= Ran && 90 >= Ran)
        {
            item.ItemType = 2;
        }
        else if (91 <= Ran && 100 >= Ran)
        {
            item.ItemType = 3;
        }

    }
    public void ItemCreate()
    {
        if (item.ItemType == 1)
        {
            item = ItemDatabase.Nomal_Item[Random.Range(0, ItemDatabase.Nomal_Item.Count - 1)].CreateItem();      //아이템 배정
            this.gameObject.GetComponent<Image>().sprite = item.Sprite;
        }
        else if (item.ItemType == 2)
        {
            item = ItemDatabase.Rare_Item[Random.Range(0, ItemDatabase.Rare_Item.Count - 1)].CreateItem();      //아이템 배정
            this.gameObject.GetComponent<Image>().sprite = item.Sprite;
        }
        else if (item.ItemType == 3)
        {

            item = ItemDatabase.Epic_Item[Random.Range(0, ItemDatabase.Epic_Item.Count - 1)].CreateItem();      //아이템 배정
            this.gameObject.GetComponent<Image>().sprite = item.Sprite;
        }
    }
 

    public void OnPointerClick(PointerEventData eventData)
    {
       
            {
                Debug.Log("꽉 찼습니다.");
            }
        }
        //this.gameObject.SetActive(false);
    }

    

