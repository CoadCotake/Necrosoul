using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class setting_window_V2 : MonoBehaviour
{
    int select;
    public bool selected;
    public float timer;
    public screenSetting scren_reol_but;
    public GameObject key_setting_UI;
    public GameObject audio_but;
    public List<Button> a = new List<Button>();
    // Start is called before the first frame update
    private void Awake()
    {
        /*a.Add(scren_reol_but.GetComponent<Button>());
        a.Add(key_setting_but.GetComponent<Button>());
        a.Add(audio_but.GetComponent<Button>());*/

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (!selected)
        {
            BtnSystem(a);
        }
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.PAUSE])&&timer<=0)
        {
            if (!key_setting_UI.activeSelf)
            {
                if (selected)
                    but_unselected();
                else
                {
                    setting_ui_off();
                }
            }
        }
       
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
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP])|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            select--;
            if (select < 0)
                select = a.Count - 1;
            AudioManage_Main.instance.UI_Page();
        }
        else if(Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            select++;
            if (select > a.Count - 1)
                select = 0;
            AudioManage_Main.instance.UI_Page();
        }
      /*  float vr = Input.GetAxis("Vertical");
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
        }*/
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
            {
            AudioManage_Main.instance.UI_Click();
            ButtonClickedEvent btn = a[select].onClick;
                btn.Invoke();
            }
        }


        public void but_selected()
        {
           /* if (!selected)
                selected = true;*/
        }

        public void but_unselected()
        {
        if (selected)
        {
            selected = false;
            timer = 0.2f;
        }
        }
    public void initializ_setting()
    {
        setting_manager.s_manger.new_setting();
    }
        public void key_setting_UI_on()
        {
            key_setting_UI.SetActive(true);
        }

        public void setting_ui_off()
        {
        select = 0;
        this.gameObject.SetActive(false);
       
        }
    }

