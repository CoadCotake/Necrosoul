using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumable_item : MonoBehaviour
{
    Item i;
    SpriteRenderer s;
    Transform p;
    float dir;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 2);
        i = ItemDatabase.itemDatabase.consumable_list[rand];
        s = this.GetComponent<SpriteRenderer>();
        s.sprite = i.Sprite;
        p = Gamemanager.GM.Player_obj.transform;
    }
    private void FixedUpdate()
    {
        dir = (p.position - this.transform.position).magnitude;
        if (dir < 5)
        {
            transform.Translate((p.position - this.transform.position)*2 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            i.consumable_effect();
            Destroy(this.gameObject);
        }
    }
    

}
