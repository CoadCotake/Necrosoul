using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestoryManage : MonoBehaviour
{
    static public DonDestoryManage DDM;
    public List<GameObject> DDM_List = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DDM = this;
    }


    public void Reset_All()
    {
        foreach (var item in DDM_List)
        {
            Destroy(item);
        }
        Destroy(this.gameObject);
    }
}
