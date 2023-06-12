using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    static public bool ShopLock;

    public GameObject Shop;
    public GameObject Lock;

    public Button ShopLockCheck;

    // Start is called before the first frame update
    void Start()
    {
        ShopLock = true;
        Shop.SetActive(false);
        Lock.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!ShopLock)
        {
            ShopLockCheck.interactable = true;
            Lock.SetActive(false);
            Shop.SetActive(true);
        }
        else
        {
            ShopLockCheck.interactable = false;
            Shop.SetActive(false);
            Lock.SetActive(true);
        }
    }


}
