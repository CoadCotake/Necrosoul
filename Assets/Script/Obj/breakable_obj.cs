  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable_obj : MonoBehaviour
{
    public int breakable_num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (breakable_num <= 0)
        {
            thisactivefalse();
        }
    }
    private void OnDisable()
    {
        Gamemanager.GM.get_item(ItemDatabase.itemDatabase.item_list[3].CreateItem());
    }
    void thisactivefalse()
    {
        this.gameObject.SetActive(false);
    }
    void breakablenumber_minus()
    {
        breakable_num--;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (breakable_num>0)
        {

            if (other.tag == "Bullet")
            {

                breakablenumber_minus();
            }
            else if (other.tag == "melee")
            {
                breakablenumber_minus();
            }
        }
    }
    //만약 애니메이션이 있다면 애니ㅔ이션 실행후 비활성화 
}
