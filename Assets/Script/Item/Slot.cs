using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{

    public Item item=null;
    public bool FullCheck=false;      //자리가 있는지 체크
    public bool UseSlot=false;      //파츠슬롯인지 체크
    public GameObject Inven;
    public GameObject ToolTip;
    public bool NullPlace = false;

    public Inventory InvenScirpt;

    [Header("아이템")]
    public int ItemCode;


    Color Cl;
    //파츠슬롯은 100부터 시작
    [SerializeField] int SlotNumber;


    private void Awake()
    {
        ToolTip = GameObject.Find("Inven_Canvas").gameObject.transform.Find("Tooltip").gameObject;
        Inven = GameObject.Find("InventorySystem");
        InvenScirpt = GameObject.Find("InventorySystem").GetComponent<Inventory>();
        if (!NullPlace)
        {
            if (UseSlot)
            {
                Debug.Log("사용슬롯에 넣습니다" + SlotNumber);
                InvenScirpt.UseInveSlot[SlotNumber - 100]=this;
            }
            else
            {
                
                Debug.Log("인벤슬롯에 넣습니다" + SlotNumber);
                InvenScirpt.InvenSlot[SlotNumber] = this;
            }
        }
        item = null;
    }
    private void Start()
    {
        if (!UseSlot)
        {
            ItemMake(ItemCode);
            ClassCheck();
            item.parent = this.gameObject;
        }

    }

    private void Update()
    {
        if (!NullPlace)
        {
            if (FullCheck)
            {
                Cl.r = 255 / 255f;
                Cl.g = 255 / 255f;
                Cl.b = 255 / 255f;
                Cl.a = 1;
                this.gameObject.GetComponent<Image>().color = Cl;
            }
            else
            {
                Cl.r = 101 / 255f;
                Cl.g = 74 / 255f;
                Cl.b = 89 / 255f;
                Cl.a = 1;
                this.gameObject.GetComponent<Image>().color = Cl;
            }
        }
        
    }
  
    public void ItemMake(int i)     //슬롯에 아이템 만들기
    {
        //if (i<ItemDatabase.itemDatabase.Rare)
        //{
        //    item = ItemDatabase.itemDatabase.Nomal_Item[i];
        //}
        //else if (i<ItemDatabase.itemDatabase.Epic)
        //{
        //    item = ItemDatabase.itemDatabase.Rare_Item[i- ItemDatabase.itemDatabase.Rare];
        //}
        //else if(i<ItemDatabase.itemDatabase.End)
        //{
        //    item = ItemDatabase.itemDatabase.Epic_Item[i- ItemDatabase.itemDatabase.Epic];
        //}
        //else
        //{
        //    item = ItemDatabase.itemDatabase.item_list[i - ItemDatabase.itemDatabase.Nomal];
        //}
        item=ItemDatabase.itemDatabase.item_list[i];
    }

    public void ChoseItem(Item i)
    {
        item=i;
    }
    
    public void FullCheckManger()       //드래그 드롭 뒤 적용 작업
    {
        //들어온 아이템을 자기슬롯번호를 넣는다.
        
        Debug.Log("슬롯자리:"+this.gameObject.name);
        if (item == null)  //만약 빈공간이면
        {
            Debug.Log("??뭐야2");
            FullCheck = false;   //사용가능 체크
        }
        else
        {
            item.SlotNumber = SlotNumber;
            Debug.Log("??뭐야333333");
            FullCheck = true;
        }
    }

    // 클래스 체크후 슬롯체크, 이미지, 슬롯번호 넣기   즉 적용시키는 단계
    public void ClassCheck()
    {
        Item item = this.gameObject.GetComponent<Slot>().item;

        if (item==null)         //아이템이 없으면
        {
            FullCheck = false;
        }
        else                    //있으면
        {
            FullCheck = true;
            this.gameObject.GetComponent<Image>().sprite = item.Sprite;
            item.SlotNumber = SlotNumber;
        }
    }

    public void UseAndItemChange()      //서로 바꾸는 것
    {

        if (item.SlotNumber >= 100) //만약 자신이 장착 슬롯에 있다.
        {

            Debug.Log("장착" + item.Sprite.name);
            Inventory.Use_InvenData.Add(item);      //장착
            Inventory.Item_InvenData.Remove(item);  //인벤슬롯해제

        }
        else        //아니다
        {
            Debug.Log("인벤" + item.Sprite.name);
            Inventory.Item_InvenData.Add(item);     //인벤슬롯넣기
            Inventory.Use_InvenData.Remove(item);   //장착해제
        }
    }

    //------------------------------클릭이벤트

    public PointerEventData.InputButton btn1 = PointerEventData.InputButton.Left;
    public PointerEventData.InputButton btn2 = PointerEventData.InputButton.Right;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == btn2)       //우클릭했을 때
        {
            if (item != null)
            {
                if (SlotNumber < 100)   //인벤슬롯이라면
                {
                    if (!Inven.GetComponent<Inventory>().CheckUseFull()&&!item.Item_Useing && item.num>0)
                    {
                        Debug.Log("우클릭");
                        item.Item_Useing = true;
                        Inven.GetComponent<Inventory>().Add_UseInvetory(item);
                        ItemEffect0.item0to10.uneffect(item);     //효과해제
                        Debug.Log("효과가 적용되고 있습니다.");
                        //ClassCheck();
                        ToolTip.gameObject.SetActive(false);
                        
                    }
                }
                else if (SlotNumber>=100)   //장착된 아이템이라면
                {
                    Debug.Log("우클릭으로 인하여 아이템이 삭제 되었습니다");
                    
                    //슬롯 아이템삭제
                    Inventory.Use_InvenData.Remove(item);
                    this.gameObject.GetComponent<Image>().sprite=null;
                    item.parent.GetComponent<Slot>().item.Item_Useing = false;
                    ItemEffect0.item0to10.uneffect(item);     //효과해제
                    item = null;
                    ClassCheck();
                }
            }
            else
            {
                Debug.Log("빈곳 우클릭 하는중");
            }
        }
    }

    public int getslotnumber()
    {
        return SlotNumber;
    }
    public void Item_eqiq()
    {
        if (item != null)
        {
            if (SlotNumber < 100)   //인벤슬롯이라면
            {
                if (!Inven.GetComponent<Inventory>().CheckUseFull() && !item.Item_Useing && item.num > 0)
                {
                    Debug.Log("우클릭");
                    item.Item_Useing = true;
                    Inven.GetComponent<Inventory>().Add_UseInvetory(item);
                    ItemEffect0.item0to10.uneffect(item);     //효과해제
                    Debug.Log("효과가 적용되고 있습니다.");
                    //ClassCheck();
                    ToolTip.gameObject.SetActive(false);

                }
            }
            else if (SlotNumber >= 100)   //장착된 아이템이라면
            {
                Debug.Log("우클릭으로 인하여 아이템이 삭제 되었습니다");

                //슬롯 아이템삭제
                Inventory.Use_InvenData.Remove(item);
                this.gameObject.GetComponent<Image>().sprite = null;
                item.parent.GetComponent<Slot>().item.Item_Useing = false;
                ItemEffect0.item0to10.uneffect(item);     //효과해제
                item = null;
                ClassCheck();
            }
        }
    }

}
