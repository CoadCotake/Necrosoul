using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftUi_Button : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject Shop;

    // Start is called before the first frame update
    void Start()
    {
        Weapon.SetActive(true);
        Shop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wepon_Up()
    {
        Weapon.SetActive(true);
        Shop.SetActive(false);
    }
    public void Shop_Up()
    {
        Weapon.SetActive(false);
        Shop.SetActive(true);
    }
}
