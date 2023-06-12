using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class Main : MonoBehaviour
{
    public GameObject CharacterSelectWindow;
    public Animator CSW;
    public GameObject Opening;
    public bool CSWCheck;
    public GameObject AAO;
    public GameObject Settong;
    public bool setting_check;
    public GameObject Esc;
    public bool esccheck;
    public bool aaocheck;
    public int select;
    public List<Button> b = new List<Button>();
    public bool Opening_end;
    public List<Button> CSW_b = new List<Button>();
    //float vert;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Debug.Log("스타트");
        CharacterSelectWindow.SetActive(false);
        CharacterSelectWindow.SetActive(true);
        CSWCheck = false;
        CSW.SetBool("Out", false);
        select = 0;
        Esc=GameObject.Find("Esc_Canvas").transform.GetChild(0).gameObject;
    }
    public void opening_ening()
    {

        Opening_end = true;
        select = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Settong == null)
        {
            Settong = GameObject.Find("Setting_UI");
            Settong.SetActive(false);
        }
        //if (!Opening.activeSelf)
        //{
        //    Opening_end = true;
        //}
        //else
        //{
        //    Opening_end = false;
        //}
      
        if (AAO == null)
            AAO = GameObject.Find("achievement_Panel");
        if (!aaocheck)
            AAO.SetActive(false);
        else
            AAO.SetActive(true);
        if (CSWCheck)
        {
            BtnSystem(CSW_b);
            if (Input.GetButtonDown("escape"))
            {
                CSWCheck = false;
                select = 0;
                CSW.SetBool("Check", false);
            }
        }
        
        

        
        if (Opening_end||!CSWCheck||!aaocheck||!esccheck||!setting_check)
        {
            BtnSystem(b);
        }
        if (!Settong.activeSelf && setting_check)
        {
            setting_check = false;
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
        }
       
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {

            ButtonClickedEvent btn = a[select].onClick;
            btn.Invoke();
        }
    }
    public void EscWindow()
    {
        Esc.SetActive(true);
    }
    public void StartGame()
    {
        if (!setting_check)
        {
            Debug.Log("스타트 버튼 누름!");
            CSWCheck = true;
            CSW.SetBool("Check", true);
            select = 0;
        }
    }
    public void setting_UI()
    {
        Settong.SetActive(true);
        setting_check = true;
    }
    public void AA()
    {
        aaocheck = true;
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Setting()
    {

    }

    public void ExitGame()
    {
        if (!setting_check)
            Application.Quit();
    }

}
