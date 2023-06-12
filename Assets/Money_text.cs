using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Money_text : MonoBehaviour
{
    Text t;
    Text get_T;
    Text lose_T;
    void Start()
    {
        t = GetComponent<Text>();
        get_T=transform.GetChild(0).GetComponent<Text>();
        lose_T = transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_status.p_status.Money != 0)
        {
            t.text = GetThousandCommaText(Player_status.p_status.Money) + "G";
        }
        else
        {
            t.text =   "0G";
        }
        if (Gamemanager.GM.get_money_chk)
        {
            lose_T.gameObject.SetActive(false);
            get_T.gameObject.SetActive(true);
            get_T.GetComponent<money_text_2>().Timer_zero();
            Gamemanager.GM.get_money_chk = false;
        }
        if (Gamemanager.GM.lose_money_chk)
        {
            lose_T.gameObject.SetActive(true);
            get_T.gameObject.SetActive(false);
            lose_T.GetComponent<money_text_3>().Timer_zero();
            Gamemanager.GM.lose_money_chk = false;
        }
    }
    
    public string GetThousandCommaText(int data) { return string.Format("{0:#,###}", data); }

}
