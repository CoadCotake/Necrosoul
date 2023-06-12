using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_item_manager : MonoBehaviour
{
    public Transform a;
    public GameObject Prefab;
    public GameObject adsf;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Font_manager.DN.SpawnText(11, "GET",this.transform);
            adsf =Instantiate(Prefab,a);
            adsf.transform.position = a.transform.position;
        }
    }
}
