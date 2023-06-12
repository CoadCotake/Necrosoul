using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Synergy
{
    public string Sy_Name;
    public List<string> Sy_item = new List<string>();
    public List<string> Sy_effect = new List<string>();

    public void Change_sy(Synergy sy)
    {
        Sy_Name = sy.Sy_Name;
        Sy_item = sy.Sy_item;
        Sy_effect = sy.Sy_effect;
    }

    public Synergy CreateSy()
    {
        Synergy S = new Synergy();
        S.Sy_Name = this.Sy_Name;

        for(int i=0;i<Sy_item.Count;i++)
        {
            S.Sy_item.Add(this.Sy_item[i]);
        }
        for (int i = 0; i < Sy_effect.Count; i++)
        {
            S.Sy_effect.Add(this.Sy_effect[i]);
        }
        return S;
    }
}



public class Inven_Content_Sy : MonoBehaviour
{
    Synergy Sy;
    
    public List<Text> Sy_item = new List<Text>();
    public List<Text> Sy_effect = new List<Text>();

    public Toggle My_toggle;

    [Header("流立 持绢具 窍绰巴")]
    public Text Sy_Name;
    public GameObject Sy_item_place;
    public GameObject Sy_effect_place;
    public GameObject Select;

    // Start is called before the first frame update
    void Start()
    {
        My_toggle = this.gameObject.GetComponent<Toggle>();

        for(int i=0;i<Sy_item_place.transform.childCount;i++)
        {
            Sy_item.Add(Sy_item_place.transform.GetChild(i).GetComponent<Text>());
        }
        for (int i = 0; i < Sy_effect_place.transform.childCount; i++)
        {
            Sy_effect.Add(Sy_effect_place.transform.GetChild(i).GetComponent<Text>());
        }
    }

    private void Update()
    {
        //if(My_toggle.isOn)
        //{
        //    Select.SetActive(false);
        //}
        //else
        //{
        //    Select.SetActive(true);
        //}
    }

    public void ChangeSy(Synergy S)
    {
        Sy_Name.text = S.Sy_Name;
        for (int i=1;i< Sy_item.Count;i++)
        {
            if (i < S.Sy_item.Count+1)
            {
                Sy_item[i].text = S.Sy_item[i-1];
                Sy_item[i].gameObject.SetActive(true);
            }
            else
            {
                Sy_item[i].gameObject.SetActive(false);
            }
        }
        for(int i=1;i< Sy_effect.Count;i++)
        {
            if (i < S.Sy_effect.Count+1)
            {
                Sy_effect[i].text = S.Sy_effect[i-1];
                Sy_effect[i].gameObject.SetActive(true);
            }
            else
            {
                Sy_effect[i].gameObject.SetActive(false);
            }
        }
    } 
}
