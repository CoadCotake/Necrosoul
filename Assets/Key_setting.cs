using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class Key_setting : MonoBehaviour
{
    public List<Button> key = new List<Button>();
    public List<Button> key_set_but = new List<Button>();//0:up 1:down 2:left 3:right  4:attack 5:jump 6:dash 7:inventory 8:pause
    int key_set_but_num = 9;
    float delay_check;
    Key_manager km;
    float timer;
    int select=0;
    bool setting_mode;
    public setting_window_V2 w;
    // Start is called before the first frame update
    void Start()
    {
        load_key_text();
        delay_check = 0.25f;
    }
  
    public void keychange_off()
    {
     
        select = 0;
        setting_manager.s_manger.Key_setting(km);
       /* for (int i = 0; i < key_set_but.Count; i++)
        {
            key_set_but[i].transform.parent.GetChild(0).gameObject.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (setting_mode )
        {
            
        }
        else
        {
            BtnSystem(key_set_but);
            if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.PAUSE]) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]))
            {
                exit_setting();
            }
        }
    }
  
    public void key_load_original()
    {
        setting_manager.s_manger.S.Keys[0] = KeyCode.UpArrow;
        setting_manager.s_manger.S.Keys[1] = KeyCode.DownArrow;
        setting_manager.s_manger.S.Keys[2] = KeyCode.LeftArrow;
        setting_manager.s_manger.S.Keys[3] = KeyCode.RightArrow;
        setting_manager.s_manger.S.Keys[4] = KeyCode.Z;
        setting_manager.s_manger.S.Keys[5] = KeyCode.X;
        setting_manager.s_manger.S.Keys[6] = KeyCode.LeftShift;
        setting_manager.s_manger.S.Keys[7] = KeyCode.Tab;
        setting_manager.s_manger.S.Keys[8] = KeyCode.Escape;
        setting_manager.s_manger.Key_setting(km);
        load_key_text();

    }//ㅇ원래 키값 복귀
    void load_key_text()
    {
        for(int i = 0; i < key_set_but_num; i++)
        {
            string s="";
            switch (i)
            {
                case 0:
                    s = Key_manager.Keys[Key_manager.KeyAction.UP].ToString();
                    
                    break;
                case 1:
                   s = Key_manager.Keys[Key_manager.KeyAction.DOWN].ToString();
                    break;
                case 2:
                   s= Key_manager.Keys[Key_manager.KeyAction.LEFT].ToString();
                    break;
                case 3:
                    s = Key_manager.Keys[Key_manager.KeyAction.RIGHT].ToString();
                    break;
                case 4:
                   s= Key_manager.Keys[Key_manager.KeyAction.ATTACK].ToString();
                    break;
                case 5:
                    s = Key_manager.Keys[Key_manager.KeyAction.JUMP].ToString();
                    break;
                case 6:
                    s = Key_manager.Keys[Key_manager.KeyAction.DASH].ToString();
                    break;
                case 7:
                    s = Key_manager.Keys[Key_manager.KeyAction.INVENTORY].ToString();
                    break;
                case 8:
                    s= Key_manager.Keys[Key_manager.KeyAction.PAUSE].ToString();
                    break;
            }
            switch (s)
            {
                case "UpArrow":
                    s = "↑";
                    break;
                case "LeftArrow":
                    s = "←";
                    break;
                case "RightArrow":
                    s = "→";
                    break;
                case "DownArrow":
                    s = "↓";
                    break;
                case "Escape":
                    s = "Esc";
                    break;
                case "LeftShift":
                    s = "LShift";
                    break;
            }
            
            key_set_but[i].transform.GetChild(0).GetComponent<Text>().text = s;
        }
    }
    public void exit_setting()
    {
        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
            timer = 0.2f;
            w.but_unselected();
            select = 0;
        }
    }
    private void OnEnable()
    {
        w.selected = true;
    }
    void BtnSystem(List<Button> a)
    {
        for (int i = 0; i < a.Count; i++)
        {

          
                if (i == select)
                {

                    if (a[i].IsInteractable() == true)
                    {
                        a[i].interactable = false;
                    }

                }
                else
                {
                    if (a[i].IsInteractable() == false)
                    {
                        a[i].interactable = true;
                    }
                }
            
        }
      
            float vr = Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Vertical"))
            {
                if (vr > 0)
                {
                    
                    select--;
                    if (select < 0)
                        select = a.Count - 1;
                }
                else
                {
                    select++;
                    if (select > a.Count - 1)
                        select = 0;
                }
            AudioManage_Main.instance.UI_Page();
        }
       

        if ((Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK])|| Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Z))&&!setting_mode&&timer<=0)
        {
            AudioManage_Main.instance.UI_Click();
            ButtonClickedEvent btn = a[select].onClick;
            btn.Invoke();
        }
    }
    public void setting_on_click()
    {
        setting_mode = true;
        key_set_but[select].transform.GetChild(0).GetComponent<Text>().text= " ";
        timer = 0.2f;
        
    }
    private void OnGUI()
    {
        if (setting_mode)
        {
            if (delay_check > 0)
            {
                delay_check -= Time.deltaTime;
            }
            else
            {
                Event e = Event.current;
                if (e.isKey)
                {
                    if (setting_manager.s_manger.S.Keys.Contains(e.keyCode)&& setting_manager.s_manger.S.Keys.IndexOf(e.keyCode) != select)
                    {
                        
                            Debug.Log("중복키 에러!");
                        delay_check = 0.25f;

                    }
                    else {
                        if (e.keyCode != KeyCode.None)
                        {
                            switch (select)
                            {
                                case 0:
                                    setting_manager.s_manger.S.Keys[0] = e.keyCode;

                                    break;
                                case 1:
                                    setting_manager.s_manger.S.Keys[1] = e.keyCode;
                                    break;
                                case 2:
                                    setting_manager.s_manger.S.Keys[2] = e.keyCode;
                                    break;
                                case 3:
                                    setting_manager.s_manger.S.Keys[3] = e.keyCode;
                                    break;
                                case 4:
                                    setting_manager.s_manger.S.Keys[4] = e.keyCode;
                                    break;
                                case 5:
                                    setting_manager.s_manger.S.Keys[5] = e.keyCode;
                                    break;
                                case 6:
                                    setting_manager.s_manger.S.Keys[6] = e.keyCode;
                                    break;
                                case 7:
                                    setting_manager.s_manger.S.Keys[7] = e.keyCode;
                                    break;
                                case 8:
                                    setting_manager.s_manger.S.Keys[8] = e.keyCode;
                                    break;
                            }

                            setting_manager.s_manger.Key_setting(km);
                            load_key_text();
                            setting_mode = false;
                            delay_check = 0.25f;
                            timer = 0.2f;
                        }
                        else
                        {
                            delay_check = 0.25f;
                        }
                    }
                }
            }
        }
    }
}
