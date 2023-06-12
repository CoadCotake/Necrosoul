using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End_Panel_Keymanger : MonoBehaviour
{

    GameObject TogglePlace;
    AudioManage_Main M_AudioMange;
    [SerializeField] List<Toggle> End_Toggle_gruop;
    public float vr;
    public float hr;
    public int P_select;

    public GameObject Ach_panel;
    public GameObject End_Penal;
    public BookMange BookMange;
    // Start is called before the first frame update
    void Start()
    {
        M_AudioMange = AudioManage_Main.instance;
        TogglePlace = this.gameObject;
        for (int i=0; i<TogglePlace.transform.childCount;i++)
        {
            End_Toggle_gruop.Add(TogglePlace.transform.GetChild(i).GetComponent<Toggle>());
        }
        P_select = 0;
        End_Toggle_gruop[P_select].SetIsOnWithoutNotify(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (hr > 0)     //坷弗率
            {
                Debug.Log("坷弗率");
                if (P_select == 0)
                {
                    P_select = TogglePlace.transform.childCount - 1;
                }
                else
                {
                    P_select--;
                }
            }
            else        //哭率
            {
                Debug.Log("哭率");
                if (P_select == TogglePlace.transform.childCount - 1)
                {
                    P_select = 0;
                }
                else
                {
                    P_select++;
                }

            }
            M_AudioMange.UI_Chose();
            End_Toggle_gruop[P_select].SetIsOnWithoutNotify(true);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            M_AudioMange.UI_Select();
            End_Toggle_gruop[P_select].onValueChanged.Invoke(true);
        }
    }

    public void Ach_Button()
    {
        Ach_panel.SetActive(true);
    }
    
    public void Go_Home_Button()
    {
        //End_Penal.SetActive(false);
        //BookMange.SceneStageSelect();
        DonDestoryManage.DDM.Reset_All();
        SceneManager.LoadScene("Main 1");
        
    }
}

