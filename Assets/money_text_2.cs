using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money_text_2 : MonoBehaviour
{
    Text t;
    Color c;
    float a = 3;
    float Timer=0;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        c = t.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer < a)
        {
            Timer += Time.deltaTime;
        }
        c.a = 1 - (1 * (Timer/a));
        t.color = c;
        t.text = "+"+Gamemanager.GM.lastest_get_money.ToString()+"G";
        if (c.a == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Timer_zero()
    {
        Timer = 0;
    }
    private void OnEnable()
    {
        Timer = 0;

    }
}
