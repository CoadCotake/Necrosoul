using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_get_ui : MonoBehaviour
{

    public Item item;
    public Image item_image;
    public Text Item_head;
    public Text Item_desc;
    public float time;
    float timer;
    public bool anim_check;
    public Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        timer = time;
        GameObject a = GameObject.FindGameObjectWithTag("Canvas");
        this.gameObject.transform.SetParent(a.transform);
        //this.gameObject.GetComponent<RectTransform>().position =new Vector2(960, 40);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (item != null)
        {
            item_image.sprite = item.Sprite;
            Item_head.text = item.Name;
            Item_desc.text = item.Description;
        }
        if (anim_check)
        {
            
            if(timer>0)
            timer -= Time.deltaTime;
            else
            {
                Debug.Log("AAAA");
                Animator a=this.GetComponent<Animator>();
                a.SetTrigger("end");
                anim_check = false;
                inv.ui_list.Remove(this);
            }
        }
    }
    public void ani_check()
    {
        Debug.Log("BAAA");
        timer = time;
        anim_check = true;
        
    }
}
