using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDownInventory : MonoBehaviour
{
    public GameObject Inven;
    static public bool check;     //true이면 인벤토리가 켜져있다.   인벤 열면 공격못하게 하는 역할
    public Tooltip tooltip;
    public int select;
    public List<Slot> sl = new List<Slot>();
    public Animator InvenAni;

    // Start is called before the first frame update
    void Start()
    {
        
        check = false;
    }
    public void btnSystem(List<Slot> a)
    {
        for (int i = 0; i < a.Count; i++)
        {
            if (i == select)
            {
                Item item = sl[select].GetComponent<Slot>().item;
                if (sl[select].GetComponent<Slot>().FullCheck)
                {
                    //Debug.Log("으잉?" + item.Name + item.Rarity);
                    tooltip.gameObject.SetActive(true);
                    tooltip.SetupTooltip(item);
                    tooltip.set_pos();
                }
                else
                {
                    Debug.Log("비어있는 곳입니다.");
                }
                //선택중
            }
            else
            {
                //선택안됨
            }
        }
        //tooltip 구조 어떤지 몰루
        //슬룻이 비었다면 건너뛰기 추가 (비었는지아닌지 판단하는 방법 물어보기)
        //슬룻선택시 변화하는 그래픽 요청
        float vr = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Vertical"))
        {
            if (vr > 0)
            {
                select -= 2;
                if (select < 0)    // 0->-2_>4   1->-1->5
                    select = select + a.Count;

            }
            else
            {
                select += 2;
                if (select > a.Count - 1)//5->7->1
                    select = select - a.Count;
            }
        }
       
        if (Input.GetButtonDown("Horizontal")) //0>1  1>0  2>3 3>2
        {
            int b=0;
            if (select % 2 == 0)
            {
                b = select + 1;
            }
            else
            {
                b = select - 1;
            }
           

            select = b;
        }
        
        if (Input.GetButtonDown("active"))
        {
            a[select].Item_eqiq();
            
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(check)
        btnSystem(sl);
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("버튼누름" + check);
        }
           if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.INVENTORY]))
        {
            if (!check)     //킴
            {
                select = 0;
                InvenAni.SetBool("Check", true);
                check = true;
                Gamemanager.GM.can_handle = false;
            }
            else    //끔
            {
                InvenAni.SetBool("Check", false);
                tooltip.gameObject.SetActive(false);
                check = false;
                Gamemanager.GM.can_handle = true;
            }
        }

        if(check&&  Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.PAUSE]))
        {
            InvenAni.SetBool("Check", false);
            tooltip.gameObject.SetActive(false);
            check = false;
        }
    }
}
