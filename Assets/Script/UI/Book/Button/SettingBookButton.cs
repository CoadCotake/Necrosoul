using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBookButton : MonoBehaviour
{
    public float vr;
    public float hr;
    public int P_select;

    [Header("타이틀")]
    [SerializeField] List<GameObject> BookTitle = new List<GameObject>();
    public Setting_Book SB;

    bool Act;
    // Start is called before the first frame update
    void Start()
    {
        SB = this.gameObject.GetComponent<Setting_Book>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (BookButtonManger.bookButtonManger.GetIndex() == 1)
        {
            ButtonMange();
        }
    }
    public void ButtonMange()
    {
        SB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        vr = Input.GetAxis("Vertical");
        hr = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Horizontal"))
        {
            if (hr > 0)     //오른쪽
            {
                Debug.Log("오른쪽");
                SB.R_Button();
                P_select = 0;
            }
            else        //왼쪽
            {
                Debug.Log("왼쪽");
                SB.L_Button();
                P_select = 0;
            }
            //SB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            if (vr > 0)     //위
            {
                Debug.Log("위");
                if (P_select == 0)
                {
                    P_select = SB.ActiveButton-1;
                }
                else
                {
                    P_select--;
                }
            }
            else        //아래
            {
                Debug.Log("아래");
                if (P_select == SB.ActiveButton-1)
                {
                    P_select = 0;
                }
                else
                {
                    P_select++;
                }

            }
            //SB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        }
        //if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("엔터침!");
            
            BookButtonManger.bookButtonManger.BookTitle_Select = SB.Title[P_select];
            BookButtonManger.bookButtonManger.SetIndex(2);
            SB.Title[P_select].GetComponent<StageContent>().StageButtonON();
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            Debug.Log("X침!");
            BookButtonManger.bookButtonManger.BookMark_Select = null;
            BookButtonManger.bookButtonManger.SetIndex(0);
            SB.Title[P_select].GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        }
    }



}
