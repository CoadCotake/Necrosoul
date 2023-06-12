using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_ground_check : MonoBehaviour
{
    PlayerCharacter p_chr;
    // Start is called before the first frame update
    void Start()
    {
        p_chr = this.transform.parent.GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            p_chr.groundcollision(collision.gameObject);
        }
    }*/
}
