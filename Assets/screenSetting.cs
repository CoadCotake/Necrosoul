using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenSetting : MonoBehaviour
{
    public bool selected;
    public bool selected_2;
    public bool full_scren;
    public Toggle t;
    public int screen_resolution_size_X;
    public int screen_resolution_size_Y;
    public List<int> screen_resol_x = new List<int>();
    public List<int> screen_resol_Y = new List<int>();
    public GameObject check_UI;
    public Text screen_resolution_size;
    public bool check_ui_select;
    float timer;
    public setting_window_V2 w;
    bool chk;
    int select;
    void Start()
    {
       
        if (t == null)
        {
            t = transform.GetChild(2).GetComponent<Toggle>();
            if (Screen.fullScreen)
            {
                full_scren = true;
                t.SetIsOnWithoutNotify(true);
                Debug.Log("a");
            }
            else
            {
                full_scren = false;
                t.SetIsOnWithoutNotify(false);
                Debug.Log("b");
            }
        }
        else
        {
            if (Screen.fullScreen)
            {
                full_scren = true;
                t.SetIsOnWithoutNotify(true);
                Debug.Log("a");
            }
            else
            {
                full_scren = false;
                t.SetIsOnWithoutNotify(false);
                Debug.Log("b");
            }
        }
       
        timer = 0.2f;
        
    }
    public void set_fullscreen_setting_button()
    {
        Toggle a = t;
        if (a.isOn)
        {
            a.isOn = false;
            full_scren_change(false);
        }
        else
        {
            a.isOn = true;
            full_scren_change(true);
        }
    }
    public void full_scren_change(bool a)
    {
        if (t.isOn)
        {
            full_scren = true;
        }
        else
        {
            full_scren = false;
        }
        if (!a)
            full_scren = false;
        else
            full_scren = true;

        set_screen_resol_size();
    }
    // Update is called once per frame
    void Update()
    {
        if (!chk)
        {
            select = setting_manager.s_manger.S.screen_resol_index;
            full_scren = setting_manager.s_manger.S.full_scren;
            if (setting_manager.s_manger.S.full_scren)
            {
                full_scren = true;
                t.SetIsOnWithoutNotify(true);
                Debug.Log("a");
            }
            else
            {
                full_scren = false;
                t.SetIsOnWithoutNotify(false);
                Debug.Log("b");
            }
            chk = true;
        }
       // full_scren = setting_manager.s_manger.S.full_scren;
       // Debug.Log(setting_manager.s_manger.S.full_scren);
        if (t == null)
        {
            t = transform.GetChild(2).GetComponent<Toggle>();
            if (Screen.fullScreen)
            {
                full_scren = true;
                t.SetIsOnWithoutNotify(true);
                Debug.Log("aa");
            }
            else
            {
                full_scren = false;
                t.SetIsOnWithoutNotify(false);
                Debug.Log("bb");
            }
        }
        
        screen_resolution_size.text = screen_resol_x[select].ToString() + "¡¿" + screen_resol_Y[select].ToString();
        if (!screen_resolution_size.GetComponent<Button>().interactable)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                if (!check_ui_select)
                {
                    if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.LEFT]))
                    {
                        select_minus();
                    }
                    if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.RIGHT]))
                    {
                        select_plus();
                    }
                    if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
                    {
                        set_screen_resol_size();
                    }
                    if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.PAUSE]) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.JUMP]) || Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]) 
               )
                    {
                        return_select();
                    }
                    if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]) || Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        return_select();
                    }
                }
            }
        }
        else
        {

            return_select();
        }
    }
    public void select_plus()
    {
        select++;
        if (select > screen_resol_x.Count - 1)
        {
            select = 0;
        }
    }
    public void select_minus()
    {
        select--;
        if (select < 0)
        {
            select = screen_resol_x.Count - 1;
        }
    }
    public void return_select()
    {
        if(select!= setting_manager.s_manger.S.screen_resol_index)
        select = setting_manager.s_manger.S.screen_resol_index;
    }
    void set_screen_resol_size()
    {
        
        int a = screen_resolution_size.text.IndexOf("¡¿");
        screen_resolution_size_X = int.Parse(screen_resolution_size.text.Substring(0, a));
        if (screen_resolution_size.text.Length > 8)
        {
            screen_resolution_size_Y = int.Parse(screen_resolution_size.text.Substring(screen_resolution_size.text.Length - a));
            setting_manager.s_manger.screen_resol(screen_resolution_size_X, screen_resolution_size_Y, full_scren);
        }
        else
        {
            screen_resolution_size_Y = int.Parse(screen_resolution_size.text.Substring(screen_resolution_size.text.Length - a+1));
            setting_manager.s_manger.screen_resol(screen_resolution_size_X, screen_resolution_size_Y, full_scren);
        }
        setting_manager.s_manger.S.screen_resolution_size_X = this.screen_resolution_size_X;
        setting_manager.s_manger.S.screen_resolution_size_Y = this.screen_resolution_size_Y;
        setting_manager.s_manger.S.full_scren = this.full_scren;
        setting_manager.s_manger.S.screen_resol_index = this.select;
        setting_manager.s_manger.S.save_setting();
        /*  
          check_UI.SetActive(true);
          check_ui_select = true;*/
    }
    public void set_screen_resol_size_2()
    {
        int a = screen_resolution_size.text.IndexOf("¡¿");
        screen_resolution_size_X = int.Parse(screen_resolution_size.text.Substring(0, a ));
        if (screen_resolution_size.text.Length > 8)
        {
            screen_resolution_size_Y = int.Parse(screen_resolution_size.text.Substring(screen_resolution_size.text.Length - a));
            setting_manager.s_manger.screen_resol(screen_resolution_size_X, screen_resolution_size_Y, full_scren);
        }
        else
        {
            screen_resolution_size_Y = int.Parse(screen_resolution_size.text.Substring(screen_resolution_size.text.Length - a + 1));
            setting_manager.s_manger.screen_resol(screen_resolution_size_X, screen_resolution_size_Y, full_scren);
        }
        if (screen_resolution_size_X!= setting_manager.s_manger.S.screen_resolution_size_X || screen_resolution_size_Y != setting_manager.s_manger.S.screen_resolution_size_Y)
        {
            setting_manager.s_manger.screen_resol(screen_resolution_size_X, screen_resolution_size_Y, full_scren);
            setting_manager.s_manger.S.screen_resolution_size_X = this.screen_resolution_size_X;
            setting_manager.s_manger.S.screen_resolution_size_Y = this.screen_resolution_size_Y;
            setting_manager.s_manger.S.full_scren = this.full_scren;
            setting_manager.s_manger.S.screen_resol_index = this.select;
            setting_manager.s_manger.S.save_setting();
            /*  check_UI.SetActive(true);
              check_ui_select = true;*/
        }
        else
        {
            w.but_unselected();
            timer = 0.2f;
        }
    }
        public void checl_UI_OK()
    {
        setting_manager.s_manger.S.screen_resolution_size_X = this.screen_resolution_size_X;
        setting_manager.s_manger.S.screen_resolution_size_Y = this.screen_resolution_size_Y;
        setting_manager.s_manger.S.full_scren = this.full_scren;
        setting_manager.s_manger.S.screen_resol_index = this.select;
      setting_manager.s_manger.S.save_setting();

        check_UI.SetActive(false);
        check_ui_select = false;
    }
    public void checl_UI_FALSE()
    {
        setting_manager.s_manger.screen_resol(setting_manager.s_manger.S.screen_resolution_size_X , setting_manager.s_manger.S.screen_resolution_size_Y, setting_manager.s_manger.S.full_scren);
        check_UI.SetActive(false);
        check_ui_select = false;
        
    }
    public void select_true()
    {
        selected = true;
    }
    }
