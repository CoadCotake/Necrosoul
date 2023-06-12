using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_obj : MonoBehaviour
{
    public GameObject shop_UI;
    bool shop_collide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shop_collide)
        {
            open_shop();
        }
        else
        {
            close_shop();
        }
    }

    void open_shop()
    {
        if (shop_collide&&Input.GetKeyDown(KeyCode.E))
        {
            shop_UI.SetActive(true);
        }
    }
    
    void close_shop()
    {
        shop_UI.SetActive(false);
    }
        private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shop_collide = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shop_collide = false;
        }
    }
}
