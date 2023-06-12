using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_content : MonoBehaviour
{
    room r;
    // Start is called before the first frame update
    void Start()
    {
        r = transform.parent.gameObject.GetComponent<room>();
    }

    // Update is called once per frame
    void Update()
    {
        if (r.on_player)
        {
            ShopButton.ShopLock = false;
            /////////////여기다 상점 ㅗ픈 시키기 열기
        }
    }
}
