using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready_Book : MonoBehaviour
{
    
    public Text PageNumber;
    public int ActiveButton;
    [SerializeField]int PageNumberlimit=5;
    [SerializeField]int PageNow=1;
    [SerializeField]int PageTotal;

    [Header("流立持绢具窍绰巴")]
    public List<GameObject> Shop = new List<GameObject>();
    public GameObject ShopPlace;
    public Ready_C_Content RCB;

    // Start is called before the first frame update
    void Start()
    {
        GetChild();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetChild()
    {
        for(int i=0; i < ShopPlace.transform.childCount;i++)
        {
            Shop.Add(ShopPlace.transform.GetChild(i).gameObject);
        }
    }

    public void ChangeElement()
    {
        ActiveButton = 0;
       
    }
}
