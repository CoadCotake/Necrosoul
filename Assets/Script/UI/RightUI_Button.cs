using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightUI_Button : MonoBehaviour
{
    public GameObject Status;
    public GameObject Inventory;

    // Start is called before the first frame update
    void Start()
    {
        Status.SetActive(false);
        Inventory.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Status_Up()
    {
        Debug.Log("스테이터스창");
        Status.SetActive(true);
        Inventory.SetActive(false);
    }
    public void Inventory_Up()
    {
        Debug.Log("인벤토리창");
        Status.SetActive(false);
        Inventory.SetActive(true);
    }
}
