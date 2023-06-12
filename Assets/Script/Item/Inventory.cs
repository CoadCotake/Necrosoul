using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    static public List<Item> Item_InvenData = new List<Item>();    //인벤
    static public List<Item> Use_InvenData = new List<Item>();     //파츠 장착

         //돈

    public List<Slot> InvenSlot;           //슬롯자리
    public List<Slot> UseInveSlot;         //슬롯자리

    public int Item_InvenDataCount = 16;     //인벤토리 슬롯 갯수
    public int Use_InvenDataCount=8;      //사용되고 있는 슬롯 갯수

    public GameObject Item_get_UI;
    public List<Item> ui_item = new List<Item>();
    public List<Item_get_ui> ui_list = new List<Item_get_ui>();
    
    public void get_item(Item i)
    {
        InvenSlot[i.Foreignkey].item.num++;    //아이템 추가
        ui_item.Add(i);
       
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < InvenSlot.Count; i++)
        {
            if (InvenSlot[i] != null)
            {
                if (InvenSlot[i].item.num != 0)
                {
                    ItemEffect0.item0to10.effect(InvenSlot[i].item);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckList();
        }
        if (ui_list.Count == 0 && ui_item.Count != 0)
        {
            GameObject a = Instantiate(Item_get_UI);
            a.GetComponent<Item_get_ui>().item = ui_item[0];
            a.GetComponent<Item_get_ui>().inv = this;
            ui_item.RemoveAt(0);
            ui_list.Add(a.GetComponent<Item_get_ui>());
        }
    }


    public void Refresh()       //먹은 아이템 슬롯에 집어넣기
    {
        
        for(int i=0;i< InvenSlot.Count;i++)     //슬롯검사
        {
            if(!InvenSlot[i].FullCheck)
            {
                InvenSlot[i].item = Item_InvenData[Item_InvenData.Count - 1];   //슬롯을 찾아 넣는다 (가장 마지막에 들어온 아이템)
                InvenSlot[i].ClassCheck();
                break;
            }
        }
    }

    public void Use_Refresh()       //먹은 아이템 슬롯에 집어넣기
    {

        for (int i = 0; i < UseInveSlot.Count; i++)     //슬롯검사
        {
            if (!UseInveSlot[i].FullCheck)
            {
                UseInveSlot[i].item = Use_InvenData[Use_InvenData.Count - 1];   //슬롯을 찾아 넣는다 (가장 마지막에 들어온 아이템)
                UseInveSlot[i].ClassCheck();
                break;
            }
        }
    }

    public bool CheckItemOver(Item item)         //중복체크
    {

        //for (int i = 0; i < Item_InvenData.Count; i++) 인벤사용x
        //{
        //    if (Item_InvenData[i].Foreignkey==item.Foreignkey) 
        //    {
        //        return true;
        //    }
          
        //}

        for (int i = 0; i < Use_InvenData.Count; i++)   
        {
            if (Use_InvenData[i].Foreignkey == item.Foreignkey)
            {
                return true;
            } 
        }

        return false;
    }

    

    public void Add_UseInvetory(Item item)
    {
        if (Use_InvenData.Count < Use_InvenDataCount)  //슬롯 자리가 있을 때
        {
            int i = item.SlotNumber;
            Debug.Log("슬롯넘버:" + i);
            Use_InvenData.Add(item);    //아이템 추가
            /*Item_InvenData.Remove(item);
            InvenSlot[i].GetComponent<Image>().sprite = null;
            InvenSlot[i].item = null;*/
            ItemEffect0.item0to10.effect(item);
            Use_Refresh();      //빈슬롯에 아이템 넣기
        }
        else
        {
            Debug.Log("꽉 찼습니다.");
        }
    }


    public bool CheckUseFull()
    {
        if(Use_InvenData.Count>=Use_InvenDataCount)
        {
            Debug.Log("꽉찼음");
            return true;
        }
        return false;
    }



    //-----------테스트 용 함수 
    public void CheckList()     //리스트 확인하는 함수
    {
        Debug.Log("인벤 아이템");
        for (int i = 0; i < Item_InvenData.Count; i++)
        {

            Debug.Log("아이템 데이터 번호: " + i + "슬롯: " + Item_InvenData[i].SlotNumber + "이름" + Item_InvenData[i].Foreignkey + "이미지" + Item_InvenData[i].Sprite);
        }
        Debug.Log("사용중인 아이템");
        for (int i = 0; i < Use_InvenData.Count; i++)
        {

            Debug.Log("아이템 데이터 번호: " + i + "슬롯: " + Use_InvenData[i].SlotNumber + "이름" + Use_InvenData[i].Foreignkey + "이미지" + Use_InvenData[i].Sprite);
        }

    }

}
