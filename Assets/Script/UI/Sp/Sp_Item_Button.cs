using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sp_Item_Button : MonoBehaviour
{

    public int P_select;
    public BookButtonManger B_Bm;

    [Header("타이틀")]
    [SerializeField] List<GameObject> Sp_Item_slot = new List<GameObject>();
    public GameObject Sp_place;


    private void OnEnable()
    {
        Gamemanager.GM.can_handle = false;
    }
    private void OnDisable()
    {
        Gamemanager.GM.can_handle = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        B_Bm = BookButtonManger.bookButtonManger;
        for(int i=0;i<Sp_place.transform.childCount;i++)
        {
            Sp_Item_slot.Add(Sp_place.transform.GetChild(i).gameObject);
        }

    }
    
    private void FixedUpdate()
    {
        ButtonMange();

    }
    // Update is called once per frame

    public void ButtonMange()
    {
        Sp_Item_slot[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);

        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT])&& !B_Bm.buttoncheck)        //오른쪽
        {
            B_Bm.ButtonTimerON();
            if (P_select >= Sp_Item_slot.Count - 1)
            {
                Debug.Log("특수오른쪽");
                P_select = Sp_Item_slot.Count - 1;
            }
            else
            {
                Debug.Log("특수오른쪽");
                P_select++;
            }
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]) && !B_Bm.buttoncheck)       //왼쪽
        {
            B_Bm.ButtonTimerON();
            if (P_select <= 0)
            {
                Debug.Log("특수왼쪽");
                P_select = 0;
            }
            else
            {
                Debug.Log("특수왼쪽");
                P_select--;
            }
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) && !B_Bm.buttoncheck)
        {
            B_Bm.ButtonTimerON();
            Debug.Log("특수아이템 선택");
            Sp_Item_slot[P_select].GetComponent<Sp_item_slot>().SelectItem();
        }
    }
}
