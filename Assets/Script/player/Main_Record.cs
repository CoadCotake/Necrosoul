using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Record : MonoBehaviour
{
    public static Main_Record M_Reord= new Main_Record();

    public KeepActionRecord Keep=new KeepActionRecord();

    // Start is called before the first frame update
    void Awake()
    {
        M_Reord = this;
        Keep.KeepLoad();
        Debug.Log("현재 수치" + Keep.T_EnemyKill);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Check_Ach(string name,string req)
    {
        Keep.KeepLoad();
        if (name=="힘")
        {
            Debug.Log("요구조건" + int.Parse(req) + "현재" + Keep.T_EnemyKill);
            if(Keep.T_EnemyKill>=int.Parse(req))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            return false;
        }
    }

    
}
