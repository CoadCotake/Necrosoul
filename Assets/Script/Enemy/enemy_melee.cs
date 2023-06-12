using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_melee : MonoBehaviour
{
    public bool hitted;
    public Vector2 hitted_force;
    public int damage;
    private void OnDisable()
    {
        hitted = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!hitted)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerCharacter p = collision.GetComponent<PlayerCharacter>();

                p.hitted_event(this.transform.position);
                p.player_hitted(damage);

                hitted = true;
            }
        }


    }
}
