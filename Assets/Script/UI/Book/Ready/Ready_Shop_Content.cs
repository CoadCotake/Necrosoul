using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready_Shop_Content : MonoBehaviour
{
    [Header("ÄÁÅÙÃ÷ ¿ä¼Ò")]
    public Text Title;
    public Text Content;
    public Text Money;
    public GameObject UnLock;

    Toggle my_toggle;
    // Start is called before the first frame update
    void Start()
    {
        my_toggle = this.gameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(my_toggle.isOn)
        {
            UnLock.SetActive(false);
        }
        else
        {
            UnLock.SetActive(true);
        }
    }
}
