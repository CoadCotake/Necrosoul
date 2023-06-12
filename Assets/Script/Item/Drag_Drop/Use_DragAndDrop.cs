using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Use_DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler //,IPointerClickHandler
{
    public Image data;
    public Item itemdata;
    public GameObject TrshItem;
    public DragAndDropContainer dragAndDropContainer;

    bool isDragging = false;


    // 드래그 오브젝트에서 발생
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!this.gameObject.GetComponent<Slot>().NullPlace)
        {
            itemdata = this.gameObject.GetComponent<Slot>().item;
            Debug.Log(this.gameObject.name + "드래그시작");
            //Debug.Log(this.gameObject.name + "이름" + this.gameObject.GetComponent<Slot>().ReturnNumber());

            if (this.gameObject.GetComponent<Image>().sprite == null)
            {
                Debug.Log(this.gameObject.name + "돌아가리!?");
                return;
            }
            Debug.Log(this.gameObject.name + "드래그 시작 완료");

            // Activate Container
            dragAndDropContainer.gameObject.SetActive(true);

            // Set Data 
            dragAndDropContainer.image.sprite = data.sprite;//this.gameObject.GetComponent<Image>().sprite;
            dragAndDropContainer.item = itemdata;
            dragAndDropContainer.Use = this.gameObject.GetComponent<Slot>().UseSlot;
            isDragging = true;
        }
    }
    // 드래그 오브젝트에서 발생
    public void OnDrag(PointerEventData eventData)
    {
        if (!this.gameObject.GetComponent<Slot>().NullPlace)
        {
            Debug.Log(this.gameObject.name + "드래그중" + isDragging);
            if (isDragging)
            {
                dragAndDropContainer.transform.position = eventData.position;
            }
            else
            {
                return;
            }
        }
    }
    // 드래그 오브젝트에서 발생        마지막 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!this.gameObject.GetComponent<Slot>().NullPlace)
        {
            Debug.Log(this.gameObject.name + "드래그끝");
            //Debug.Log("이름"+data.sprite.name);
            if (isDragging)
            {
                if (dragAndDropContainer.image.sprite != null)
                {
                    // set data from dropped object  
                    data.sprite = dragAndDropContainer.image.sprite;
                    this.gameObject.GetComponent<Slot>().item = dragAndDropContainer.item;

                    if (this.gameObject.GetComponent<Slot>().UseSlot ^ dragAndDropContainer.Use)
                    {
                        Debug.Log("주는 자리에서 이벤트 발생");
                        this.gameObject.GetComponent<Slot>().UseAndItemChange();
                    }

                }
                else
                {
                    // Clear Data
                    data.sprite = null;
                    this.gameObject.GetComponent<Slot>().item = null;
                    itemdata = null;
                    Debug.Log("비어있는 곳");
                }

                this.gameObject.GetComponent<Slot>().FullCheckManger();
            }


            isDragging = false;
            // Reset Contatiner
            itemdata = null;
            dragAndDropContainer.image.sprite = null;
            dragAndDropContainer.item = null;
            dragAndDropContainer.gameObject.SetActive(false);
            dragAndDropContainer.Use = false;
        }
        
    }


    //--------------------------------------------------------------------------------

    // 드롭 오브젝트에서 발생     //OnEndDrag보다 먼저 발생함
    public void OnDrop(PointerEventData eventData)
    {
        if (!this.gameObject.GetComponent<Slot>().NullPlace)
        {
            Debug.Log(this.gameObject.name + "드롭");

            if (dragAndDropContainer.image.sprite != null)
            {
                // keep data instance for swap 
                Sprite tempSprite = data.sprite;
                Item tempitem = this.gameObject.GetComponent<Slot>().item;


                // set data from drag object on Container
                data.sprite = dragAndDropContainer.image.sprite;
                this.gameObject.GetComponent<Slot>().item = dragAndDropContainer.item;
                this.gameObject.GetComponent<Slot>().FullCheckManger();

                if (this.gameObject.GetComponent<Slot>().UseSlot ^ dragAndDropContainer.Use)
                {
                    this.gameObject.GetComponent<Slot>().UseAndItemChange();
                }
                Debug.Log("받는 자리에서 이벤트 발생");
                // put data from drop object to Container. 
                dragAndDropContainer.image.sprite = tempSprite;
                dragAndDropContainer.item = tempitem;
                dragAndDropContainer.Use = this.gameObject.GetComponent<Slot>().UseSlot;
            }
            else
            {
                Debug.Log("아무것도 가지고  있지 않아");
                dragAndDropContainer.image.sprite = null;
                dragAndDropContainer.item = null;
            }
        }
        else
        {
            if (dragAndDropContainer.image.sprite != null)
            {
                Debug.Log("밖으로 버리겠습니다."+ dragAndDropContainer.item.SlotNumber+"현재 스크립트"+this.name);

                if (dragAndDropContainer.item.SlotNumber >= 100)
                {

                    Debug.Log("삭제합니다????");
                    Inventory.Use_InvenData.Remove(dragAndDropContainer.item);
                }
                else
                {
                    
                }
                this.gameObject.GetComponent<Slot>().item.parent.GetComponent<Slot>().item.Item_Useing=false;
                Debug.Log("아이템을 버립니다.");
                isDragging = false;

                // Reset Contatiner
                dragAndDropContainer.image.sprite = null;
                dragAndDropContainer.item = null;
                dragAndDropContainer.gameObject.SetActive(false);
                dragAndDropContainer.Use = false;

                
            }
        }
    }

    public void ChangeSlotNumber()
    {
        
    }

    //public PointerEventData.InputButton btn1 = PointerEventData.InputButton.Left;
    //public PointerEventData.InputButton btn2 = PointerEventData.InputButton.Right;
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if(eventData.button==btn2)
    //    {

    //    }
    //}
}