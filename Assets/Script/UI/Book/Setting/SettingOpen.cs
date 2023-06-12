using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingOpen : MonoBehaviour
{
    public Toggle toggle;
    public GameObject Key_setting;
    public GameObject Book;
    public Setting_Main setting_Main;
    // Start is called before the first frame update
    void Start()
    {
        
        toggle = this.gameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
        {
            if (BookButtonManger.bookButtonManger.GetIndex() == 1)
            {
                Book.SetActive(true);
                if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]) && BookButtonManger.bookButtonManger.GetIndex() == 1&&!Key_setting.activeSelf)
                {
                   // setting_Main.SelectOff();
                    BookButtonManger.bookButtonManger.SetIndex(0);
                }
            }
        }
        else
        {
            Book.SetActive(false);

        }
    }
}
