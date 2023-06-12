using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy_Manager : MonoBehaviour
{
    static public Synergy_Manager Sy_manager;
    public List<Synergy> Sy_total_List=new List<Synergy>();
    Synergy Sy=new Synergy();
    string[] item;
    string[] effect;

    // Start is called before the first frame update
    void Awake()
    {
        Sy_manager = this;
        Get_Synergy();
    }

    public void Get_Synergy()
    {
        List<Dictionary<string, object>> Data = CSVReader.Read("Synergy");

        for (int i = 0; i < Data.Count; i++)
        { 
            Sy.Sy_Name = Data[i]["Name"].ToString();
            item = Data[i]["item"].ToString().Split('/');
            effect= Data[i]["effect"].ToString().Split('/');
            for (int ii = 0; ii < item.Length; ii++)
            {
                Sy.Sy_item.Add(item[ii]);
            }
            for (int ii = 0; ii < effect.Length; ii++)
            {
                Sy.Sy_effect.Add(effect[ii]);
            }
            Sy_total_List.Add(Sy.CreateSy());
            Sy.Sy_item.Clear();
            Sy.Sy_effect.Clear();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
