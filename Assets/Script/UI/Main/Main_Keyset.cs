using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Keyset : MonoBehaviour
{
    public GameObject ButtonPlace;
    public List<Toggle> toggles = new List<Toggle>();
    public GameObject SavePlace;
    public List<Toggle> Saves = new List<Toggle>();

    public float vr;
    public float hr;
    public int P_select;
    public int P_S_select;
    public int Page;

    public SaveSelect save_select;
    public Setting_Main setting_main;

    AudioManage_Main M_AudioMange;
    // Start is called before the first frame update
    void Start()
    {
        ButtonsAni.ButAni_End = false;
        M_AudioMange = AudioManage_Main.instance;
        for (int i = 0; i < ButtonPlace.transform.childCount; i++)
        {
            toggles.Add(ButtonPlace.transform.GetChild(i).GetComponent<Toggle>());
        }
        for (int i = 0; i < SavePlace.transform.childCount; i++)
        {
            Saves.Add(SavePlace.transform.GetChild(i).GetComponent<Toggle>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonsAni.ButAni_End)
        {
            if (Page == 0)
            {
                MainButton();
            }
            else if (Page == 1 && toggles[P_select].name == "Start")
            {
                SaveSlot();
            }
            else if (Page == 1 && toggles[P_select].name == "Setting")
            {
                Setting();
            }
        }
    }

    public void MainButton()
    {
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))      //위
        {
            Debug.Log("위");
            if (P_select == 0)
            {
                P_select = ButtonPlace.transform.childCount - 1;
            }
            else
            {
                P_select--;
            }
            M_AudioMange.UI_Chose();
            toggles[P_select].SetIsOnWithoutNotify(true);
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))    //아래
        {
            Debug.Log("아래");
            if (P_select == ButtonPlace.transform.childCount - 1)
            {
                P_select = 0;
            }
            else
            {
                P_select++;
            }
            M_AudioMange.UI_Chose();
            toggles[P_select].SetIsOnWithoutNotify(true);
        }
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {
            M_AudioMange.UI_Open();
            toggles[P_select].onValueChanged.Invoke(true);
            Page++;
        }
    }

    public void SaveSlot()
    {

        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))    //오른쪽
        {
            Debug.Log("오른쪽");
            if (P_S_select == SavePlace.transform.childCount - 1)
            {
                P_S_select = 0;
            }
            else
            {
                P_S_select++;
            }
            M_AudioMange.UI_Chose();
            Saves[P_S_select].SetIsOnWithoutNotify(true);
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))    //왼쪽
        {
            Debug.Log("왼쪽");
            if (P_S_select == 0)
            {
                P_S_select = SavePlace.transform.childCount - 1;
            }
            else
            {
                P_S_select--;
            }
            M_AudioMange.UI_Chose();
            Saves[P_S_select].SetIsOnWithoutNotify(true);

        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {
            M_AudioMange.UI_Select();
            Saves[P_S_select].onValueChanged.Invoke(true);
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            M_AudioMange.UI_Close();
            save_select.SelectOff();
            Page--;
        }
    }

    public void Setting()
    {

        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
        {
            M_AudioMange.UI_Close();
            setting_main.SelectOff();
            Page--;
        }
    }
}
